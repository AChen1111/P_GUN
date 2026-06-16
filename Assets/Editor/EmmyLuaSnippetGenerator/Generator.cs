using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EmmyLuaSnippetGenerator
{
    /// <summary>
    /// 该文件只用来给 IDE 生成 Lua 类型提示, 不要在运行时 require 或打包到版本中.
    /// </summary>
    public static class LuaTypeGenerator
    {
        private static readonly HashSet<Type> LuaNumberTypes = new HashSet<Type>
        {
            typeof(byte),
            typeof(sbyte),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(float),
            typeof(double)
        };

        private static readonly HashSet<string> LuaKeywords = new HashSet<string>
        {
            "and", "break", "do", "else", "elseif", "end", "false", "for", "function", "if",
            "in", "local", "nil", "not", "or", "repeat", "return", "then", "true", "until", "while"
        };

        private static readonly StringBuilder Output = new StringBuilder(1024);
        private static readonly StringBuilder Temp = new StringBuilder(256);
        private static readonly Dictionary<Type, List<MethodInfo>> ExtensionMethods = new Dictionary<Type, List<MethodInfo>>();

        private static SettingOptions options;
        private static string[] functionCompatibleTypes = Array.Empty<string>();

        [MenuItem("LuaType/生成EmmyLua类型注解")]
        public static void GenerateEmmyTypeFiles()
        {
            if (!TryLoadOptions())
            {
                return;
            }

            Directory.CreateDirectory(options.GeneratePath);

            try
            {
                List<Type> exportTypes = CollectAllExportTypes();
                functionCompatibleTypes = options.GetFunctionCompatibleTypes();
                BuildExtensionMethodIndex(exportTypes);
                GenerateTypeDefines(exportTypes);
                ClearEmmyTypeFiles();
                WriteToFiles();
                AssetDatabase.Refresh();
                Debug.Log("生成注解文件完毕.");
            }
            catch (Exception exception)
            {
                Debug.LogError("错误: " + exception);
            }
            finally
            {
                Output.Clear();
                Temp.Clear();
                ExtensionMethods.Clear();
                functionCompatibleTypes = Array.Empty<string>();
            }
        }

        [MenuItem("LuaType/清除EmmyLua类型注解")]
        public static void ClearEmmyTypeFiles()
        {
            if (!TryLoadOptions())
            {
                return;
            }

            if (!Directory.Exists(options.GeneratePath))
            {
                return;
            }

            int count = 0;
            foreach (string file in Directory.GetFiles(options.GeneratePath, "TypeHint_*.lua"))
            {
                File.Delete(file);
                count++;
            }

            Debug.Log($"清除完毕, 删除了 {count} 份注解文件. (生成时会自动执行该清理)");
        }

        private static bool TryLoadOptions()
        {
            if (XmlHelper.TryLoadConfig(SettingOptions.SavePath, out SettingOptions loaded))
            {
                options = loaded;
                return true;
            }

            Debug.LogError("错误: 需要配置文件才能执行操作. 请先通过 LuaType/设置 保存配置.");
            return false;
        }

        private static List<Type> CollectAllExportTypes()
        {
            HashSet<Type> set = new HashSet<Type>();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (IsExportType(type))
                    {
                        set.Add(type);
                    }
                }
            }

            return set
                .Where(type => !type.FullName.Contains("<"))
                .OrderBy(type => type.FullName)
                .ToList();
        }

        private static bool IsExportType(Type type)
        {
            if (type == null || string.IsNullOrEmpty(type.FullName))
            {
                return false;
            }

            string typeNamespace = type.Namespace;
            if (string.IsNullOrEmpty(typeNamespace))
            {
                return false;
            }

            if (typeNamespace.StartsWith("UnityEditor", StringComparison.Ordinal) || typeNamespace.Contains("Burst"))
            {
                return false;
            }

            return options.GetTargetNamespaces().Any(namespaceName =>
                typeNamespace.StartsWith(namespaceName, StringComparison.Ordinal));
        }

        private static void BuildExtensionMethodIndex(IReadOnlyList<Type> exportTypes)
        {
            foreach (Type type in exportTypes)
            {
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
                foreach (MethodInfo method in methods)
                {
                    if (!method.IsDefined(typeof(ExtensionAttribute), false))
                    {
                        continue;
                    }

                    ParameterInfo[] parameters = method.GetParameters();
                    if (parameters.Length == 0)
                    {
                        continue;
                    }

                    Type extensionTarget = parameters[0].ParameterType;
                    if (!ExtensionMethods.TryGetValue(extensionTarget, out List<MethodInfo> extensionMethodList))
                    {
                        extensionMethodList = new List<MethodInfo>();
                        ExtensionMethods.Add(extensionTarget, extensionMethodList);
                    }

                    extensionMethodList.Add(method);
                }
            }
        }

        private static void GenerateTypeDefines(IReadOnlyList<Type> exportTypes)
        {
            Output.AppendLine("---@meta CSharp");
            Output.AppendLine();
            Output.AppendLine("---@class NotExportType @表明该类型未导出");
            Output.AppendLine("---@class NotExportEnum @表明该枚举未导出");
            Output.AppendLine();

            WriteGlobalVariablesDefine();
            WriteXLuaDefine();
            WriteNamespaceDefines(exportTypes);

            foreach (Type type in exportTypes)
            {
                WriteClassDefine(type);
                WriteClassFieldDefine(type);
                Output.AppendLine($"{type.ToLuaTypeName().ToLuaVariableName()} = {{}}");

                if (options.GenerateCSAlias)
                {
                    WriteClassAliasDefine(type);
                }

                WriteClassConstructorDefine(type);
                WriteClassMethodDefine(type);
                Output.AppendLine();
            }
        }

        private static void WriteNamespaceDefines(IReadOnlyList<Type> exportTypes)
        {
            HashSet<string> namespaces = new HashSet<string>();
            foreach (string targetNamespace in options.GetTargetNamespaces())
            {
                AddNamespaceChain(namespaces, targetNamespace);
            }

            foreach (Type type in exportTypes)
            {
                AddNamespaceChain(namespaces, type.Namespace);
            }

            foreach (string namespaceName in namespaces.OrderBy(item => item.Count(ch => ch == '.')).ThenBy(item => item))
            {
                Output.AppendLine($"---@class {namespaceName}");
                Output.AppendLine($"{namespaceName.ToLuaVariableName()} = {namespaceName.ToLuaVariableName()} or {{}}");

                if (options.GenerateCSAlias)
                {
                    Output.AppendLine($"---@alias CS.{namespaceName} {namespaceName}");
                    Output.AppendLine($"CS.{namespaceName.ToLuaVariableName()} = CS.{namespaceName.ToLuaVariableName()} or {{}}");
                }

                Output.AppendLine();
            }
        }

        private static void AddNamespaceChain(ISet<string> namespaces, string namespaceName)
        {
            if (string.IsNullOrWhiteSpace(namespaceName))
            {
                return;
            }

            string[] parts = namespaceName.Split('.');
            for (int i = 1; i <= parts.Length; i++)
            {
                namespaces.Add(string.Join(".", parts.Take(i)));
            }
        }

        private static void WriteToFiles()
        {
            string[] lines = Output.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            int fileCount = 0;
            int lineCount = 0;
            StreamWriter writer = null;

            foreach (string line in lines)
            {
                if (writer == null)
                {
                    string fileName = Path.Combine(options.GeneratePath, $"TypeHint_{fileCount}.lua");
                    writer = new StreamWriter(fileName);
                    writer.WriteLine("---@meta");
                    writer.WriteLine("---@diagnostic disable");
                    writer.WriteLine();
                }

                if (string.IsNullOrWhiteSpace(line) && options.SingleFileMaxLine > 0 && lineCount >= options.SingleFileMaxLine)
                {
                    writer.Close();
                    writer = null;
                    fileCount++;
                    lineCount = 0;
                    continue;
                }

                writer.WriteLine(line);
                lineCount++;
            }

            writer?.Close();
        }

        private static void WriteGlobalVariablesDefine()
        {
            foreach ((string varName, string typeName) in options.GetGlobalVariables())
            {
                Output.AppendLine($"---@type {typeName}");
                Output.AppendLine($"{varName} = nil");
                Output.AppendLine();
            }
        }

        private static void WriteXLuaDefine()
        {
            Output.AppendLine("---@class CS");
            Output.AppendLine("CS = CS or {}");
            Output.AppendLine();
            Output.AppendLine("---@class xlua");
            Output.AppendLine("xlua = xlua or {}");
            Output.AppendLine();
            Output.AppendLine("---@param type table");
            Output.AppendLine("---@param method string");
            Output.AppendLine("---@param func function");
            Output.AppendLine("function xlua.hotfix(type, method, func) end");
            Output.AppendLine();
            Output.AppendLine("---@param obj any");
            Output.AppendLine("---@return System.Type");
            Output.AppendLine("function typeof(obj) end");
            Output.AppendLine();
        }

        private static void WriteClassDefine(Type type)
        {
            if (type.BaseType != null && !type.IsEnum)
            {
                Output.AppendLine($"---@class {type.ToLuaTypeName()} : {type.BaseType.ToLuaTypeName()}");
            }
            else
            {
                Output.AppendLine($"---@class {type.ToLuaTypeName()}");
            }
        }

        private static void WriteClassFieldDefine(Type classType)
        {
            IEnumerable<FieldInfo> fields = classType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (!classType.IsEnum)
            {
                fields = fields.Concat(classType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            }

            foreach (FieldInfo field in fields)
            {
                if (!field.IsMemberObsolete())
                {
                    Output.AppendLine($"---@field {field.Name.ToLuaIdentifier()} {field.FieldType.ToLuaTypeName().MakeLuaFunctionCompatible()}");
                }
            }

            IEnumerable<PropertyInfo> properties = classType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (!classType.IsEnum)
            {
                properties = properties.Concat(classType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            }

            foreach (PropertyInfo property in properties)
            {
                if (!property.IsMemberObsolete())
                {
                    Output.AppendLine($"---@field {property.Name.ToLuaIdentifier()} {property.PropertyType.ToLuaTypeName().MakeLuaFunctionCompatible()}");
                }
            }
        }

        private static void WriteClassAliasDefine(Type type)
        {
            string typeName = type.ToLuaTypeName();
            string typeAlias = type.ToLuaTypeName(addCSPrefix: true);

            Output.AppendLine($"---@alias {typeAlias} {typeName}");
            Output.AppendLine($"{typeAlias.ToLuaVariableName()} = {typeName.ToLuaVariableName()}");
            Output.AppendLine();
        }

        private static void WriteClassConstructorDefine(Type type)
        {
            if (type == typeof(MonoBehaviour) || type.IsSubclassOf(typeof(MonoBehaviour)) || type.IsAbstract)
            {
                return;
            }

            ConstructorInfo[] constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                return;
            }

            for (int i = 0; i < constructors.Length - 1; i++)
            {
                WriteOverloadMethodCommentDeclare(constructors[i].GetParameters(), type, null);
            }

            WriteMethodFunctionDeclare(constructors[^1].GetParameters(), type, "New", type.ToLuaTypeName().ToLuaVariableName(), true);
        }

        private static void WriteClassMethodDefine(Type type)
        {
            string className = type.ToLuaTypeName().ToLuaVariableName();
            Dictionary<string, List<MethodInfo>> methodGroups = new Dictionary<string, List<MethodInfo>>();

            IEnumerable<MethodInfo> methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            if (!type.IsEnum)
            {
                methods = methods.Concat(type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly));
            }

            foreach (MethodInfo method in methods)
            {
                if (method.IsGenericMethod || method.IsMemberObsolete() || IsIgnoredMethodName(method.Name))
                {
                    continue;
                }

                if (!methodGroups.TryGetValue(method.Name, out List<MethodInfo> group))
                {
                    group = new List<MethodInfo>();
                    methodGroups.Add(method.Name, group);
                }

                group.Add(method);
            }

            foreach (List<MethodInfo> group in methodGroups.Values)
            {
                for (int i = 0; i < group.Count - 1; i++)
                {
                    MethodInfo method = group[i];
                    WriteOverloadMethodCommentDeclare(method.GetParameters(), method.ReturnType, method.IsStatic ? null : type);
                }

                MethodInfo lastMethod = group[^1];
                WriteMethodFunctionDeclare(lastMethod.GetParameters(), lastMethod.ReturnType, lastMethod.Name, className, lastMethod.IsStatic);
            }

            WriteExtensionMethodFunctionDeclare(type);
        }

        private static bool IsIgnoredMethodName(string methodName)
        {
            return methodName.StartsWith("get_", StringComparison.Ordinal)
                   || methodName.StartsWith("set_", StringComparison.Ordinal)
                   || methodName.StartsWith("op_", StringComparison.Ordinal)
                   || methodName.StartsWith("add_", StringComparison.Ordinal)
                   || methodName.StartsWith("remove_", StringComparison.Ordinal);
        }

        private static void WriteOverloadMethodCommentDeclare(ParameterInfo[] parameters, Type returnType, Type classType)
        {
            List<Type> returnTypes = new List<Type>();
            Temp.Clear();

            if (classType != null)
            {
                Temp.Append($"self: {classType.ToLuaTypeName()}");
                if (parameters.Length > 0)
                {
                    Temp.Append(", ");
                }
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                Type parameterType = UnwrapRefType(parameter, out bool isRefOrOut);
                if (isRefOrOut)
                {
                    returnTypes.Add(parameterType);
                }

                string parameterName = GetLuaParameterName(parameter, i);
                Temp.Append($"{parameterName}: {parameterType.ToLuaTypeName().MakeLuaFunctionCompatible()}");
                if (i < parameters.Length - 1)
                {
                    Temp.Append(", ");
                }
            }

            if (returnType != null && returnType != typeof(void))
            {
                returnTypes.Insert(0, returnType);
            }

            string returnText = returnTypes.Count == 0
                ? string.Empty
                : " : " + string.Join(", ", returnTypes.Select(type => type.ToLuaTypeName().MakeLuaFunctionCompatible()));
            Output.AppendLine($"---@overload fun({Temp}){returnText}");
        }

        private static void WriteMethodFunctionDeclare(ParameterInfo[] parameters, Type returnType, string methodName, string className, bool isStatic)
        {
            List<Type> extraReturnTypes = new List<Type>();
            Temp.Clear();

            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                string parameterName = GetLuaParameterName(parameter, i);
                Type parameterType = UnwrapRefType(parameter, out bool isRefOrOut);
                if (isRefOrOut)
                {
                    extraReturnTypes.Add(parameterType);
                }

                Output.AppendLine($"---@param {parameterName} {parameterType.ToLuaTypeName().MakeLuaFunctionCompatible()}");
                Temp.Append(parameterName);
                if (i < parameters.Length - 1)
                {
                    Temp.Append(", ");
                }
            }

            WriteReturnDeclare(returnType, extraReturnTypes);

            string separator = isStatic ? "." : ":";
            Output.AppendLine($"function {className}{separator}{methodName.ToLuaIdentifier()}({Temp}) end");
        }

        private static void WriteReturnDeclare(Type returnType, List<Type> extraReturnTypes)
        {
            List<Type> returnTypes = new List<Type>();
            if (returnType != null && returnType != typeof(void))
            {
                returnTypes.Add(returnType);
            }

            returnTypes.AddRange(extraReturnTypes);
            if (returnTypes.Count > 0)
            {
                Output.AppendLine($"---@return {string.Join(", ", returnTypes.Select(type => type.ToLuaTypeName().MakeLuaFunctionCompatible()))}");
            }
        }

        private static void WriteExtensionMethodFunctionDeclare(Type type)
        {
            if (!ExtensionMethods.TryGetValue(type, out List<MethodInfo> extensionMethodList))
            {
                return;
            }

            foreach (MethodInfo method in extensionMethodList)
            {
                ParameterInfo[] parameters = method.GetParameters().Skip(1).ToArray();
                WriteMethodFunctionDeclare(parameters, method.ReturnType, method.Name, type.ToLuaTypeName().ToLuaVariableName(), false);
            }
        }

        private static Type UnwrapRefType(ParameterInfo parameter, out bool isRefOrOut)
        {
            isRefOrOut = parameter.IsOut || parameter.ParameterType.IsByRef;
            return isRefOrOut ? parameter.ParameterType.GetElementType() : parameter.ParameterType;
        }

        private static string GetLuaParameterName(ParameterInfo parameter, int parameterIndex)
        {
            string prefix = parameter.IsOut ? "out_" : parameter.ParameterType.IsByRef ? "ref_" : string.Empty;
            string name = string.IsNullOrWhiteSpace(parameter.Name) ? $"arg{parameterIndex + 1}" : parameter.Name;
            return (prefix + name).ToLuaIdentifier();
        }

        private static string MakeLuaFunctionCompatible(this string typeName)
        {
            return functionCompatibleTypes.Contains(typeName) ? $"{typeName} | function" : typeName;
        }

        private static string ToLuaTypeName(this Type type, bool addCSPrefix = false)
        {
            string prefix = addCSPrefix ? "CS." : string.Empty;
            if (type == null)
            {
                return "NotExportType";
            }

            if (LuaNumberTypes.Contains(type))
            {
                return "number";
            }

            if (type == typeof(string))
            {
                return "string";
            }

            if (type == typeof(bool))
            {
                return "boolean";
            }

            if (type.IsArray)
            {
                return $"{type.GetElementType().ToLuaTypeName(addCSPrefix)}[]";
            }

            string typeName = type.FullName ?? type.ToString();
            int genericArgumentIndex = typeName.IndexOf("[[", StringComparison.Ordinal);
            if (genericArgumentIndex > 0)
            {
                typeName = typeName.Substring(0, genericArgumentIndex);
            }

            return prefix + typeName.EscapeGenericTypeSuffix();
        }

        private static string ToLuaVariableName(this string value)
        {
            return string.Join(".", value.Split('.').Select(part => part.ToLuaIdentifier()));
        }

        private static string ToLuaIdentifier(this string value)
        {
            // C# 内部成员名可能包含 $, <, > 等 Lua 不支持的字符.
            string result = Regex.Replace(value ?? string.Empty, @"[^A-Za-z0-9_]", "_");
            if (string.IsNullOrWhiteSpace(result) || char.IsDigit(result[0]))
            {
                result = "_" + result;
            }

            return LuaKeywords.Contains(result) ? "_" + result : result;
        }

        private static string EscapeGenericTypeSuffix(this string value)
        {
            return Regex.Replace(value, @"`[0-9]+", string.Empty).Replace("+", ".");
        }

        private static bool IsMemberObsolete(this MemberInfo member)
        {
            return member.GetCustomAttributes(typeof(ObsoleteAttribute), false).Length > 0;
        }
    }
}
