using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using LifeinFile.Windows;
using System.IO;
using System.IO.Compression;
using System.Numerics;
using System.Text.Json;
using System.Windows;

namespace LifeinFile.Core.Pets
{
    public class PetFile
    {
        public string InstanceId { get; set; }
        public string Name { get; set; }
        public int SpriteId { get; set; }
        
        public double Hunger { get; set; }
        public double Affection{get;set;}
        
        public DateTime LastTime{get;set;}
        
        public double PositionX{get;set;}
        public double PositionY{get;set;}
        
        public PetFile(){}
        public PetFile(PetModel model, Window window)
        {
            InstanceId = model.InstanceId;
            Name = model.Name.Value;
            SpriteId = model.SpritesId.Value;
            Hunger= model.CurrentHunger.Value;
            Affection = model.CurrentAffection.Value;
            LastTime = DateTime.UtcNow;
            
            PositionX = window.Left;
            PositionY = window.Top;
        }

        /*
        public void CreateFile(string cagePath)
        {
            string newJsonText = JsonSerializer.Serialize(this, JsonHelper.Options);

            // ZIPファイルを開く（Updateモードで開くと、中身の追加や上書きができます）
            using (FileStream zipToOpen = new FileStream(cagePath, FileMode.OpenOrCreate))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
            {
                // ZIP内部のパスを指定（ZIP内は必ずスラッシュ '/' で区切ります）
                string entryPath = $"Pets/{Name}_{InstanceId.Substring(0, 8)}.json";
                // もし既に同じ名前のファイルがZIP内にあれば、一度削除する（上書きのため）
                archive.GetEntry(entryPath)?.Delete();

                // ZIPの中に新しいファイル（エントリー）を作成する
                ZipArchiveEntry petEntry = archive.CreateEntry(entryPath);

                // そのエントリーの中にJSONのテキストを書き込む
                using (StreamWriter writer = new StreamWriter(petEntry.Open()))
                {
                    writer.Write(newJsonText);
                }
            }
        }
        */
    }
}