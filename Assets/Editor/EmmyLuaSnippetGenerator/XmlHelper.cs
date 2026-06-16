using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;

namespace EmmyLuaSnippetGenerator
{
    public static class XmlHelper
    {
        public static void SaveConfig<T>(T config, string filePath)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using StreamWriter writer = new StreamWriter(filePath);

            serializer.Serialize(writer, config);
        }

        public static bool TryLoadConfig<T>(string filePath, out T config)
        {
            if (!File.Exists(filePath))
            {
                config = default;
                return false;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using StreamReader reader = new StreamReader(filePath);

            config = (T)serializer.Deserialize(reader);
            return true;
        }

        public static void OpenWithDefaultEditor(string filePath)
        {
            // 使用系统默认程序打开文件, 方便从编辑器菜单检查配置.
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }
    }
}
