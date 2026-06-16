using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EmmyLuaSnippetGenerator
{
    [Serializable]
    public sealed class SettingOptions
    {
        public string GeneratePath;
        public string TargetNamespacesStr;
        public string GlobalVariablesStr;
        public string FunctionCompatibleTypesStr;
        public bool GenerateCSAlias = true;
        public int SingleFileMaxLine = 20000;

        private static string saveRootPath;

        public static string SaveRootPath
        {
            get => string.IsNullOrWhiteSpace(saveRootPath)
                ? Path.GetFullPath(Path.Combine(Application.dataPath, ".."))
                : saveRootPath;
            set => saveRootPath = value;
        }

        public static string SavePath => Path.Combine(SaveRootPath, "EmmyLuaSnippetToolData", "config.xml");

        public string[] GetTargetNamespaces()
        {
            return SplitBySpace(TargetNamespacesStr);
        }

        public (string varName, string typeName)[] GetGlobalVariables()
        {
            return SplitBySpace(GlobalVariablesStr)
                .Select(item => item.Split(':'))
                .Where(parts => parts.Length == 2)
                .Select(parts => (parts[0], parts[1]))
                .ToArray();
        }

        public string[] GetFunctionCompatibleTypes()
        {
            return SplitBySpace(FunctionCompatibleTypesStr);
        }

        private static string[] SplitBySpace(string value)
        {
            return string.IsNullOrWhiteSpace(value)
                ? Array.Empty<string>()
                : value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }

    public sealed class SettingsWindow : EditorWindow
    {
        private SettingOptions options;

        [MenuItem("LuaType/设置")]
        public static void ShowWindow()
        {
            GetWindow<SettingsWindow>("Lua类型注解文件设置");
        }

        private void OnEnable()
        {
            options = XmlHelper.TryLoadConfig(SettingOptions.SavePath, out SettingOptions settings)
                ? settings
                : CreateDefaultOptions();
        }

        private void OnGUI()
        {
            GUILayout.Space(20);

            GUILayout.Label("配置文件的存放路径");
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            SettingOptions.SaveRootPath = EditorGUILayout.TextField(SettingOptions.SaveRootPath, GUILayout.MinWidth(200));
            GUI.enabled = true;
            if (GUILayout.Button("...", GUILayout.Width(50)))
            {
                SettingOptions.SaveRootPath = EditorUtility.OpenFolderPanel("选择配置文件存放路径", "", "");
            }
            EditorGUILayout.EndHorizontal();

            DrawPathField("生成类型注解文件的路径", ref options.GeneratePath, "选择生成类型注解文件路径");

            GUILayout.Space(10);
            GUILayout.Label("要生成注解的C#命名空间\n- 多个命名空间用空格分隔\n- 例如: UnityEngine Game.Gameplay XLua");
            options.TargetNamespacesStr = EditorGUILayout.TextField(options.TargetNamespacesStr, GUILayout.MinWidth(200));

            GUILayout.Space(10);
            GUILayout.Label("要生成注解的全局变量\n- 变量名:类型名, 多个组用空格分隔\n- 例如: UNITY_EDITOR:boolean DEBUG_LV:integer");
            options.GlobalVariablesStr = EditorGUILayout.TextField(options.GlobalVariablesStr, GUILayout.MinWidth(200));

            GUILayout.Space(10);
            GUILayout.Label("使以下类型名兼容Lua function类型\n- 多个类型名用空格分隔\n- 例如: System.Action UnityEngine.Events.UnityAction");
            options.FunctionCompatibleTypesStr = EditorGUILayout.TextField(options.FunctionCompatibleTypesStr, GUILayout.MinWidth(200));

            GUILayout.Space(10);
            GUILayout.Label("生成带CS.前缀的兼容alias\n- 启用后, 将为生成的类型名额外添加带CS.前缀的版本");
            options.GenerateCSAlias = EditorGUILayout.Toggle(options.GenerateCSAlias);

            GUILayout.Space(10);
            GUILayout.Label("单个注解文件的最大行数\n- 超过该行数时会自动拆分成多个文件\n- 大幅影响类型分析性能, 请依据电脑配置设置");
            options.SingleFileMaxLine = (int)EditorGUILayout.Slider(options.SingleFileMaxLine, 5000, 40000, GUILayout.MinWidth(200));

            GUILayout.Space(20);
            if (GUILayout.Button("保存配置文件"))
            {
                XmlHelper.SaveConfig(options, SettingOptions.SavePath);
                Close();
            }

            if (GUILayout.Button("打开配置文件"))
            {
                XmlHelper.OpenWithDefaultEditor(SettingOptions.SavePath);
            }

            if (GUILayout.Button("取消"))
            {
                Close();
            }
        }

        private static void DrawPathField(string label, ref string path, string title)
        {
            GUILayout.Space(10);
            GUILayout.Label(label);
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            path = EditorGUILayout.TextField(path, GUILayout.MinWidth(200));
            GUI.enabled = true;
            if (GUILayout.Button("...", GUILayout.Width(50)))
            {
                path = EditorUtility.OpenFolderPanel(title, "", "");
            }
            EditorGUILayout.EndHorizontal();
        }

        private static SettingOptions CreateDefaultOptions()
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "..")).Replace("\\", "/");
            return new SettingOptions
            {
                GeneratePath = $"{projectRoot}/LuaAPI/Unity/",
                TargetNamespacesStr = "UnityEngine Game.Core Game.Gameplay Game.Items Game.ItemEffects Game.Pooling Game.UI Game.Animation Game.Presentation XLua",
                FunctionCompatibleTypesStr = "System.Action UnityEngine.Events.UnityAction",
                GenerateCSAlias = true,
                SingleFileMaxLine = 20000
            };
        }
    }
}
