using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using BindData = ComponentAutoBindTool.BindData;
using System.Reflection;
using System.IO;

[InitializeOnLoad]
[CustomEditor(typeof(ComponentAutoBindTool))]
public class ComponentAutoBindToolInspector : Editor
{
    private const string AutoAttachObjectIdKey = "ComponentAutoBindTool.AutoAttachObjectId";
    private const string AutoAttachTypeNameKey = "ComponentAutoBindTool.AutoAttachTypeName";
    private const string AutoAttachWaitingKey = "ComponentAutoBindTool.AutoAttachWaiting";
    private const string AutoAttachRetryCountKey = "ComponentAutoBindTool.AutoAttachRetryCount";
    private const int AutoAttachMaxRetryCount = 120;

    private ComponentAutoBindTool m_Target;

    private SerializedProperty m_BindDatas;
    private SerializedProperty m_BindComs;
    private List<BindData> m_TempList = new List<BindData>();
    private List<string> m_TempFiledNames = new List<string>();
    private List<string> m_TempComponentTypeNames = new List<string>();

    private string[] s_AssemblyNames = { "ComponentAutoBindTool.Runtime", "Assembly-CSharp", "Assembly-CSharp-firstpass" };
    private string[] m_HelperTypeNames;
    private string m_HelperTypeName;
    private int m_HelperTypeNameIndex;

    private AutoBindGlobalSetting m_Setting;

    private SerializedProperty m_Namespace;
    private SerializedProperty m_ClassName;
    private SerializedProperty m_CodePath;
    private SerializedProperty m_BaseClassName;

    static ComponentAutoBindToolInspector()
    {
        EditorApplication.delayCall += TryAttachPendingGeneratedScript;
    }

    private void OnEnable()
    {
        m_Target = (ComponentAutoBindTool)target;
        m_BindDatas = serializedObject.FindProperty("BindDatas");
        m_BindComs = serializedObject.FindProperty("m_BindComs");

        m_HelperTypeNames = GetTypeNames(typeof(IAutoBindRuleHelper), s_AssemblyNames);

        string[] paths = AssetDatabase.FindAssets("t:AutoBindGlobalSetting");
        if (paths.Length == 0)
        {
            Debug.LogError("不存在AutoBindGlobalSetting");
            return;
        }
        if (paths.Length > 1)
        {
            Debug.LogError("AutoBindGlobalSetting数量大于1");
            return;
        }
        string path = AssetDatabase.GUIDToAssetPath(paths[0]);
        m_Setting = AssetDatabase.LoadAssetAtPath<AutoBindGlobalSetting>(path);


        m_Namespace = serializedObject.FindProperty("m_Namespace");
        m_ClassName = serializedObject.FindProperty("m_ClassName");
        m_CodePath = serializedObject.FindProperty("m_CodePath");
        m_BaseClassName = serializedObject.FindProperty("m_BaseClassName");

        m_Namespace.stringValue = string.IsNullOrEmpty(m_Namespace.stringValue) ? m_Setting.Namespace : m_Namespace.stringValue;
        m_ClassName.stringValue = string.IsNullOrEmpty(m_ClassName.stringValue) ? m_Target.gameObject.name : m_ClassName.stringValue;
        m_CodePath.stringValue = string.IsNullOrEmpty(m_CodePath.stringValue) ? m_Setting.CodePath : m_CodePath.stringValue;
        m_BaseClassName.stringValue = string.IsNullOrEmpty(m_BaseClassName.stringValue) ? m_Setting.BaseClassName : m_BaseClassName.stringValue;

        serializedObject.ApplyModifiedProperties();
    }

    public override void OnInspectorGUI()
    {

        DrawTopButton();

        DrawHelperSelect();

        DrawSetting();

        DrawKvData();

        serializedObject.ApplyModifiedProperties();
        serializedObject.UpdateIfRequiredOrScript();

    }

    /// <summary>
    /// 绘制顶部按钮
    /// </summary>
    private void DrawTopButton()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("排序"))
        {
            Sort();
        }

        if (GUILayout.Button("全部删除"))
        {
            RemoveAll();
        }

        if (GUILayout.Button("删除空引用"))
        {
            RemoveNull();
        }

        if (GUILayout.Button("自动绑定组件"))
        {
            AutoBindComponent();
        }

        if (GUILayout.Button("生成绑定代码"))
        {
            GenAutoBindCode();
        }

        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 排序
    /// </summary>
    private void Sort()
    {


        m_TempList.Clear();
        foreach (BindData data in m_Target.BindDatas)
        {
            m_TempList.Add(new BindData(data.Name, data.BindCom));
        }
        m_TempList.Sort((x, y) =>
        {
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        });

        m_BindDatas.ClearArray();
        foreach (BindData data in m_TempList)
        {
            AddBindData(data.Name, data.BindCom);
        }

        SyncBindComs();
    }

    /// <summary>
    /// 全部删除
    /// </summary>
    private void RemoveAll()
    {
        m_BindDatas.ClearArray();

        SyncBindComs();
    }

    /// <summary>
    /// 删除空引用
    /// </summary>
    private void RemoveNull()
    {
        for (int i = m_BindDatas.arraySize - 1; i >= 0; i--)
        {
            SerializedProperty element = m_BindDatas.GetArrayElementAtIndex(i).FindPropertyRelative("BindCom");
            if (element.objectReferenceValue == null)
            {
                m_BindDatas.DeleteArrayElementAtIndex(i);
            }
        }

        SyncBindComs();
    }

    /// <summary>
    /// 自动绑定组件
    /// </summary>
    private void AutoBindComponent()
    {
        if (m_Target.RuleHelper == null)
        {
            Debug.LogError("自动绑定失败, 未找到可用的绑定规则辅助器.");
            return;
        }

        m_BindDatas.ClearArray();
       
        Transform[] childs = m_Target.gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in childs)
        {
            m_TempFiledNames.Clear();
            m_TempComponentTypeNames.Clear();

            if (m_Target.RuleHelper.IsValidBind(child, m_TempFiledNames, m_TempComponentTypeNames))
            {
                for (int i = 0; i < m_TempFiledNames.Count; i++)
                {
                    Component com = child.GetComponent(m_TempComponentTypeNames[i]);
                    if (com == null)
                    {
                        Debug.LogError($"{child.name}上不存在{m_TempComponentTypeNames[i]}的组件");
                    }
                    else
                    {
                        AddBindData(m_TempFiledNames[i], child.GetComponent(m_TempComponentTypeNames[i]));
                    }
                   
                }
            }
        }

        SyncBindComs();
    }

    /// <summary>
    /// 绘制辅助器选择框
    /// </summary>
    private void DrawHelperSelect()
    {
        if (m_HelperTypeNames == null || m_HelperTypeNames.Length == 0)
        {
            EditorGUILayout.HelpBox("未找到 IAutoBindRuleHelper 实现, 请确认 DefaultAutoBindRuleHelper 已正常编译.", MessageType.Error);
            return;
        }

        // 默认选择第一个可用辅助器, 避免空引用状态下无法初始化.
        m_HelperTypeName = m_HelperTypeNames[0];

        if (m_Target.RuleHelper != null)
        {
            m_HelperTypeName = m_Target.RuleHelper.GetType().FullName;

            m_HelperTypeNameIndex = -1;
            for (int i = 0; i < m_HelperTypeNames.Length; i++)
            {
                if (m_HelperTypeName == m_HelperTypeNames[i])
                {
                    m_HelperTypeNameIndex = i;
                }
            }

            if (m_HelperTypeNameIndex < 0)
            {
                // 当前辅助器类型不在可选列表中时, 回退到第一个可用类型.
                m_HelperTypeNameIndex = 0;
                m_HelperTypeName = m_HelperTypeNames[m_HelperTypeNameIndex];
            }
        }
        else
        {
            IAutoBindRuleHelper helper = (IAutoBindRuleHelper)CreateHelperInstance(m_HelperTypeName, s_AssemblyNames);
            m_Target.RuleHelper = helper;
        }

        foreach (GameObject go in Selection.gameObjects)
        {
            ComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();
            if (autoBindTool != null && autoBindTool.RuleHelper == null)
            {
                IAutoBindRuleHelper helper = (IAutoBindRuleHelper)CreateHelperInstance(m_HelperTypeName, s_AssemblyNames);
                autoBindTool.RuleHelper = helper;
            }
        }

        int selectedIndex = EditorGUILayout.Popup("AutoBindRuleHelper", m_HelperTypeNameIndex, m_HelperTypeNames);
        if (selectedIndex != m_HelperTypeNameIndex)
        {
            m_HelperTypeNameIndex = selectedIndex;
            m_HelperTypeName = m_HelperTypeNames[selectedIndex];
            IAutoBindRuleHelper helper = (IAutoBindRuleHelper)CreateHelperInstance(m_HelperTypeName, s_AssemblyNames);
            m_Target.RuleHelper = helper;

        }
    }

    /// <summary>
    /// 绘制设置项
    /// </summary>
    private void DrawSetting()
    {
        EditorGUILayout.BeginHorizontal();
        m_Namespace.stringValue = EditorGUILayout.TextField(new GUIContent("命名空间："), m_Namespace.stringValue);
        if (GUILayout.Button("默认设置"))
        {
            m_Namespace.stringValue = m_Setting.Namespace;
        }
        EditorGUILayout.EndHorizontal();

        //只有选中一个物体时允许设置类名
        EditorGUILayout.BeginHorizontal();
        m_ClassName.stringValue = EditorGUILayout.TextField(new GUIContent("类名："), m_ClassName.stringValue);
        if (GUILayout.Button("物体名"))
        {
            m_ClassName.stringValue = m_Target.gameObject.name;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        m_BaseClassName.stringValue = EditorGUILayout.TextField(new GUIContent("继承类："), m_BaseClassName.stringValue);
        if (GUILayout.Button("默认设置"))
        {
            m_BaseClassName.stringValue = m_Setting.BaseClassName;
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("代码保存路径：");
        EditorGUILayout.LabelField( m_CodePath.stringValue);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("选择路径"))
        {
            string temp = m_CodePath.stringValue;
            m_CodePath.stringValue = EditorUtility.OpenFolderPanel("选择代码保存路径", Application.dataPath, "");
            if (string.IsNullOrEmpty(m_CodePath.stringValue))
            {
                m_CodePath.stringValue = temp;
            }
        }
        if (GUILayout.Button("默认设置"))
        {
            m_CodePath.stringValue = m_Setting.CodePath;
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// 绘制键值对数据
    /// </summary>
    private void DrawKvData()
    {
        //绘制key value数据

        int needDeleteIndex = -1;

        EditorGUILayout.BeginVertical();
        SerializedProperty property;

        for (int i = 0; i < m_BindDatas.arraySize; i++)
        {

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"[{i}]",GUILayout.Width(25));
            property = m_BindDatas.GetArrayElementAtIndex(i).FindPropertyRelative("Name");
            property.stringValue = EditorGUILayout.TextField(property.stringValue, GUILayout.Width(150));
            property = m_BindDatas.GetArrayElementAtIndex(i).FindPropertyRelative("BindCom");
            property.objectReferenceValue = EditorGUILayout.ObjectField(property.objectReferenceValue, typeof(Component), true);

            if (GUILayout.Button("X"))
            {
                //将元素下标添加进删除list
                needDeleteIndex = i;
            }
            EditorGUILayout.EndHorizontal();
        }

        //删除data
        if (needDeleteIndex != -1)
        {
            m_BindDatas.DeleteArrayElementAtIndex(needDeleteIndex);
            SyncBindComs();
        }

        EditorGUILayout.EndVertical();
    }



    /// <summary>
    /// 添加绑定数据
    /// </summary>
    private void AddBindData(string name, Component bindCom)
    {
        int index = m_BindDatas.arraySize;
        m_BindDatas.InsertArrayElementAtIndex(index);
        SerializedProperty element = m_BindDatas.GetArrayElementAtIndex(index);
        element.FindPropertyRelative("Name").stringValue = name;
        element.FindPropertyRelative("BindCom").objectReferenceValue = bindCom;

    }

    /// <summary>
    /// 同步绑定数据
    /// </summary>
    private void SyncBindComs()
    {
        m_BindComs.ClearArray();

        for (int i = 0; i < m_BindDatas.arraySize; i++)
        {
            SerializedProperty property = m_BindDatas.GetArrayElementAtIndex(i).FindPropertyRelative("BindCom");
            m_BindComs.InsertArrayElementAtIndex(i);
            m_BindComs.GetArrayElementAtIndex(i).objectReferenceValue = property.objectReferenceValue;
        }
    }

    /// <summary>
    /// 获取指定基类在指定程序集中的所有子类名称
    /// </summary>
    private string[] GetTypeNames(Type typeBase, string[] assemblyNames)
    {
        List<string> typeNames = new List<string>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assembly == null || Array.IndexOf(assemblyNames, assembly.GetName().Name) < 0)
            {
                continue;
            }

            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                // 忽略加载失败的类型, 保留已经成功加载的辅助器类型.
                types = Array.FindAll(e.Types, type => type != null);
            }

            foreach (Type type in types)
            {
                if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                {
                    typeNames.Add(type.FullName);
                }
            }
        }

        typeNames.Sort();
        return typeNames.ToArray();
    }

    /// <summary>
    /// 创建辅助器实例
    /// </summary>
    private object CreateHelperInstance(string helperTypeName, string[] assemblyNames)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            if (assembly == null || Array.IndexOf(assemblyNames, assembly.GetName().Name) < 0)
            {
                continue;
            }

            object instance = assembly.CreateInstance(helperTypeName);
            if (instance != null)
            {
                return instance;
            }
        }

        return null;
    }


    /// <summary>
    /// 生成自动绑定代码
    /// </summary>
    private void GenAutoBindCode()
    {
        GameObject go = m_Target.gameObject;

        string className = !string.IsNullOrEmpty(m_Target.ClassName) ? m_Target.ClassName : go.name;
        string codePath = !string.IsNullOrEmpty(m_Target.CodePath) ? m_Target.CodePath : m_Setting.CodePath;

        if (!Directory.Exists(codePath))
        {
            Debug.LogError($"{go.name}的代码保存路径{codePath}无效");
            return;
        }

        GenMainCode(codePath, className);
        GenBindComponentCode(codePath, className);

        RegisterPendingGeneratedScriptAttach(go, className);

        AssetDatabase.Refresh();

        if (!EditorApplication.isCompiling)
        {
            TryAttachPendingGeneratedScript();
        }

        EditorUtility.DisplayDialog("提示", "代码生成完毕, 编译完成后会自动挂载主类脚本.", "OK");
    }

    /// <summary>
    /// 记录待挂载脚本信息, 用于编译完成后继续挂载.
    /// </summary>
    private void RegisterPendingGeneratedScriptAttach(GameObject go, string className)
    {
        string namespaceName = m_Target.Namespace;
        string fullTypeName = string.IsNullOrEmpty(namespaceName) ? className : $"{namespaceName}.{className}";
        GlobalObjectId objectId = GlobalObjectId.GetGlobalObjectIdSlow(go);

        EditorPrefs.SetString(AutoAttachObjectIdKey, objectId.ToString());
        EditorPrefs.SetString(AutoAttachTypeNameKey, fullTypeName);
        EditorPrefs.SetBool(AutoAttachWaitingKey, true);
        EditorPrefs.SetInt(AutoAttachRetryCountKey, AutoAttachMaxRetryCount);
    }

    /// <summary>
    /// 尝试挂载生成的主类脚本, 编译未完成时延后执行.
    /// </summary>
    private static void TryAttachPendingGeneratedScript()
    {
        if (!EditorPrefs.GetBool(AutoAttachWaitingKey, false))
        {
            return;
        }

        if (EditorApplication.isCompiling || EditorApplication.isUpdating)
        {
            EditorApplication.delayCall += TryAttachPendingGeneratedScript;
            return;
        }

        string objectIdText = EditorPrefs.GetString(AutoAttachObjectIdKey, string.Empty);
        string typeName = EditorPrefs.GetString(AutoAttachTypeNameKey, string.Empty);
        if (string.IsNullOrEmpty(objectIdText) || string.IsNullOrEmpty(typeName))
        {
            ClearPendingGeneratedScriptAttach();
            return;
        }

        if (!GlobalObjectId.TryParse(objectIdText, out GlobalObjectId objectId))
        {
            Debug.LogError($"自动挂载失败, 无法解析目标对象ID: {objectIdText}.");
            ClearPendingGeneratedScriptAttach();
            return;
        }

        GameObject go = GlobalObjectId.GlobalObjectIdentifierToObjectSlow(objectId) as GameObject;
        if (go == null)
        {
            Debug.LogError("自动挂载失败, 目标对象不存在或已被删除.");
            ClearPendingGeneratedScriptAttach();
            return;
        }

        Type componentType = FindType(typeName);
        if (componentType == null)
        {
            int retryCount = EditorPrefs.GetInt(AutoAttachRetryCountKey, 0);
            if (retryCount > 0)
            {
                // 脚本类型可能仍在编译或载入, 下一帧继续尝试.
                EditorPrefs.SetInt(AutoAttachRetryCountKey, retryCount - 1);
                EditorApplication.delayCall += TryAttachPendingGeneratedScript;
                return;
            }

            Debug.LogError($"自动挂载失败, 未找到生成的脚本类型: {typeName}.");
            ClearPendingGeneratedScriptAttach();
            return;
        }

        if (!typeof(MonoBehaviour).IsAssignableFrom(componentType))
        {
            Debug.LogError($"自动挂载失败, 类型不是MonoBehaviour: {typeName}.");
            ClearPendingGeneratedScriptAttach();
            return;
        }

        if (go.GetComponent(componentType) == null)
        {
            Undo.AddComponent(go, componentType);
            EditorUtility.SetDirty(go);
            if (go.scene.IsValid())
            {
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(go.scene);
            }
            Debug.Log($"自动挂载成功: {typeName} -> {go.name}.");
        }

        ClearPendingGeneratedScriptAttach();
    }

    /// <summary>
    /// 清理待挂载脚本信息, 避免重复执行.
    /// </summary>
    private static void ClearPendingGeneratedScriptAttach()
    {
        EditorPrefs.DeleteKey(AutoAttachObjectIdKey);
        EditorPrefs.DeleteKey(AutoAttachTypeNameKey);
        EditorPrefs.DeleteKey(AutoAttachWaitingKey);
        EditorPrefs.DeleteKey(AutoAttachRetryCountKey);
    }

    /// <summary>
    /// 在所有已加载程序集中查找类型, 支持asmdef程序集.
    /// </summary>
    private static Type FindType(string fullTypeName)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type type = assembly.GetType(fullTypeName);
            if (type != null)
            {
                return type;
            }
        }

        return null;
    }

    /// <summary>
    /// 生成主逻辑代码文件
    /// </summary>
    private void GenMainCode(string codePath, string className)
    {
        string mainCodeFilePath = $"{codePath}/{className}.cs";
        if (File.Exists(mainCodeFilePath))
        {
            return;
        }

        using (StreamWriter sw = new StreamWriter(mainCodeFilePath))
        {
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("");

            if (!string.IsNullOrEmpty(m_Target.Namespace))
            {
                //命名空间.
                sw.WriteLine("namespace " + m_Target.Namespace);
                sw.WriteLine("{");
                sw.WriteLine("");
            }

            //主逻辑类.
            sw.WriteLine($"\tpublic partial class {className} : {GetMainBaseClassName()}");
            sw.WriteLine("\t{");
            sw.WriteLine("\t\tprivate void Awake()");
            sw.WriteLine("\t\t{");
            sw.WriteLine("\t\t\tGetBindComponents(gameObject);");
            sw.WriteLine("\t\t}");
            sw.WriteLine("\t}");

            if (!string.IsNullOrEmpty(m_Target.Namespace))
            {
                sw.WriteLine("}");
            }
        }
    }

    /// <summary>
    /// 获取主类继承类型名称.
    /// </summary>
    private string GetMainBaseClassName()
    {
        if (!string.IsNullOrEmpty(m_Target.BaseClassName))
        {
            return m_Target.BaseClassName;
        }

        return m_Setting.BaseClassName;
    }

    /// <summary>
    /// 生成组件绑定代码文件
    /// </summary>
    private void GenBindComponentCode(string codePath, string className)
    {
        using (StreamWriter sw = new StreamWriter($"{codePath}/{className}.BindComponent.cs"))
        {
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine("using UnityEngine.UI;");
            sw.WriteLine("");

            sw.WriteLine("// 自动生成于: " + DateTime.Now);

            if (!string.IsNullOrEmpty(m_Target.Namespace))
            {
                //命名空间.
                sw.WriteLine("namespace " + m_Target.Namespace);
                sw.WriteLine("{");
                sw.WriteLine("");
            }

            //绑定代码类.
            sw.WriteLine($"\tpublic partial class {className}");
            sw.WriteLine("\t{");
            sw.WriteLine("");

            //组件字段.
            foreach (BindData data in m_Target.BindDatas)
            {
                sw.WriteLine($"\t\tprivate {data.BindCom.GetType().Name} m_{data.Name};");
            }
            sw.WriteLine("");

            sw.WriteLine("\t\tprivate void GetBindComponents(GameObject go)");
            sw.WriteLine("\t\t{");

            //获取autoBindTool上的Component.
            sw.WriteLine($"\t\t\tComponentAutoBindTool autoBindTool = go.GetComponent<ComponentAutoBindTool>();");
            sw.WriteLine("");

            //根据索引获取.

            for (int i = 0; i < m_Target.BindDatas.Count; i++)
            {
                BindData data = m_Target.BindDatas[i];
                string filedName = $"m_{data.Name}";
                sw.WriteLine($"\t\t\t{filedName} = autoBindTool.GetBindComponent<{data.BindCom.GetType().Name}>({i});");
            }

            sw.WriteLine("\t\t}");

            sw.WriteLine("\t}");

            if (!string.IsNullOrEmpty(m_Target.Namespace))
            {
                sw.WriteLine("}");
            }
        }
    }
}
 
