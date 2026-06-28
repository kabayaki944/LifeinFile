using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace LifeinFile.Helper
{
    public static class JsonHelper
    {
        public static JsonSerializerOptions Options =>_options;
        // シリアライズ設定を共通化（日本語対応と整形出力）
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true,
            // 日本語をエスケープせず、そのまま保存・読み込みできるようにする
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
            Converters = { new JsonStringEnumConverter() },
        };

        // 書き込み時に使うBOMなしUTF-8エンコーディング
        private static readonly Encoding _encoding = new UTF8Encoding(false);

        public static void Save<T>(T data, string filePath)
        {
            string json = JsonSerializer.Serialize(data, _options);
            File.WriteAllText(filePath, json, _encoding);
        }

        public static (T? data, bool success) TryLoad<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return (default(T), false); 
            }

            try
            {
                // BOMなしUTF-8で安全に読み込む
                string json = File.ReadAllText(filePath, _encoding);
                var data = JsonSerializer.Deserialize<T>(json, _options);
                return (data, true);
            }
            catch
            {
                // 読み込みに失敗（破損など）した場合はfalseを返す
                return (default(T), false);
            }
        }
    }
}