using LifeinFile.Helper;
using LifeinFile.Models.Cages;
using LifeinFile.Windows;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;

namespace LifeinFile.Core.Cage
{
    public class CageFile
    {
        public string CageInstanceId{get;set;}
        public string CageName{get;set;}
        public string Type{get;set;}
        
        public double PositionX{get;set;}
        public double PositionY{get;set;}
        public double Width{get;set;}
        public double Height{get;set;}

        public CageFile(CageModel model, IReadOnlyWindowModel window)
        {
            CageInstanceId = model.InstanceId;
            CageName = model.Name.Value;
            
            PositionX = window.CurrentLeft;
            PositionY = window.CurrentTop;
            Width = window.CurrentWidth;
            Height = window.CurrentHeight;
        }

        public string CreateFile(string directoryPath)
        {
            string savePath = Path.Combine(directoryPath, $"{CageName}_{CageInstanceId.Substring(0, 8)}.cage");
            
            // 自分自身のデータを読みやすいJSON文字列に変換する
            string jsonText = JsonSerializer.Serialize(this, JsonHelper.Options);

            // FileMode.Create は「無ければ新規作成、あれば中身を空にして完全上書き」という最強のモードです
            using (FileStream zipToOpen = new FileStream(savePath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                // 1. ZIPの直下に CageData.json を作成してデータを書き込む
                ZipArchiveEntry cageEntry = archive.CreateEntry("CageData.json");
                using (StreamWriter writer = new StreamWriter(cageEntry.Open()))
                {
                    writer.Write(jsonText);
                }

                // 2. 空の Pets ディレクトリを作成する
                archive.CreateEntry("Pets/");
                return Path.GetFullPath(savePath);
            }
        }
    }
}