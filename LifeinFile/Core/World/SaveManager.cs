using LifeinFile.Core.Cage;
using LifeinFile.Core.Pets;
using LifeinFile.Core.Setting;
using LifeinFile.Models.Cages;
using Microsoft.Win32;
using System.IO;
using System.IO.Compression;

namespace LifeinFile.Core.Facade
{
    public enum SaveResult
    {
        Success,
        Cancelled,
        Error
    }
    
    public static class SaveManager
    {
        public static SaveResult TrySave(CageExternal cageExternal)
        {
            var pets = PetCageConnector.GetPetsInCage(cageExternal);
            
            string zipPath = GetSavePath(cageExternal.Model);
            if(zipPath == null) return SaveResult.Cancelled;

            CageFile cageFile = cageExternal.InstanceFile();
            zipPath = Path.Combine(zipPath, cageFile.CageFileName);
            cageExternal.Model.Path = zipPath;

            
            using (FileStream zipToOpen = new FileStream(zipPath, FileMode.Create))
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            { 
                cageFile.Write(archive);

                foreach (var petExternal in pets)
                {
                    petExternal.ExportFile(archive);
                }
            }
            return SaveResult.Success;
        }

        /// <summary>
        /// Cage見つからなかったら新しい場所を聞く
        /// </summary>
        /// <param name="cageExternal"></param>
        /// <returns></returns>
        static string GetSavePath(IExternalModel cageExternal)
        {
            Console.Write(cageExternal.Path);
    
            // フォルダが存在しない場合、ユーザーに新しい場所を聞く
            if (!Directory.Exists(cageExternal.Path))
            {
                string defaultPath = UserSetting.Data.DefaultCreateCagePath;
                string parentDir = Path.GetDirectoryName(defaultPath);
                string targetFolderName = Path.GetFileName(defaultPath.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));

                var dialog = new OpenFolderDialog
                {
                    Title = "ケージデータの保存先フォルダを選択してください",
                    InitialDirectory = parentDir,
                    FolderName = targetFolderName 
                };
                
                if (dialog.ShowDialog() == true)
                {
                    return dialog.FolderName; 
                }
                return null; 
            }        
            return cageExternal.Path;
        }
    }
}