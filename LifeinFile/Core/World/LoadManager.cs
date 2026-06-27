using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using LifeinFile.Helper;
using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.Json;

namespace LifeinFile.Core.Facade
{
    public enum LoadResult
    {
        Success,
        Cancelled,
        Error
    }

    public static class LoadManager
    {
        public static LoadResult TryLoad()
        {
            string zipPath = AskPath();
            if (zipPath == null) return LoadResult.Cancelled;
            
            var files = GetFiles(zipPath);

            if (files.cageFile == null)
            {
                Console.Error.Write("cage file not found.");
                return LoadResult.Error;
            }

            if (files.petFiles == null)
            {
                Console.Error.Write("pet files not found.");
                return LoadResult.Error;
            }
            
            CageInitData cageInitData = new CageInitData("BeforeLoadName", false);
            CageExternal cageExternal = CageManager.CreateCage(cageInitData);
            cageExternal.ImportFile(files.cageFile);
            cageExternal.EnableWindow(true);

            List<PetExternal> pets = new List<PetExternal>();
            foreach (var petFile in files.petFiles)
            {
                PetInitData petInitData = new PetInitData("BeforeLoadName", PetSpritesDictionary.DEFAULT_PET_ID, false);
                PetExternal petExternal = PetManager.CreatePet(petInitData);
                petExternal.ImportFile(petFile);
                PetCageConnector.MovePetToCage(petExternal, cageExternal);
                pets.Add(petExternal);
            }
            
            foreach (var petExternal in pets)
                petExternal.EnableWindow(true);
            return LoadResult.Success;
        }

        static string AskPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Title = "ロードするCageファイルを選択してください";
            openFileDialog.Filter = "Cageファイル (*.cage)|*.cage|すべてのファイル (*.*)|*.*";

       
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        static (CageFile cageFile, List<PetFile> petFiles) GetFiles(string zipPath)
        {
            CageFile cageFile = null;
            List<PetFile> petFiles = new List<PetFile>();
    
            // ★追加：Petsディレクトリ（またはその中身）が存在したかどうかのフラグ
            bool hasPetsDirectory = false;
    
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                // 1. CageFile の読み込み
                ZipArchiveEntry cageEntry = archive.GetEntry("CageData.json");
                if (cageEntry != null)
                {
                    using (StreamReader reader = new StreamReader(cageEntry.Open()))
                    {
                        string jsonText = reader.ReadToEnd();
                        cageFile = JsonSerializer.Deserialize<CageFile>(jsonText, JsonHelper.Options);
                    }
                }
        
                // 2. PetFiles の読み込み
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    // エントリーが "Pets/" 階層に関連するものかチェック
                    if (entry.FullName.StartsWith("Pets/"))
                    {
                        // ★1つでも見つかればフラグを true にする
                        hasPetsDirectory = true;

                        // さらにそれが .json ファイルなら読み込む
                        if (entry.Name.EndsWith(".json"))
                        {
                            using (StreamReader reader = new StreamReader(entry.Open()))
                            {
                                string jsonText = reader.ReadToEnd();
                                PetFile pet = JsonSerializer.Deserialize<PetFile>(jsonText, JsonHelper.Options);

                                if (pet != null)
                                {
                                    petFiles.Add(pet);
                                }
                            }
                        }
                    }
                }
            }

            // ★追加：もしPetsディレクトリに関連するデータが1つも無かったら null にする
            if (!hasPetsDirectory)
            {
                petFiles = null;
            }

            return (cageFile, petFiles);
        }
    }
}