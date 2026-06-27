using LifeinFile.Core.Pets;
using LifeinFile.Helper;
using LifeinFile.Models.Pets;
using System.IO;
using System.IO.Compression;
using System.Text.Json;
using System.Windows;

namespace LifeinFile.Controller.PetSystem
{
    public class PetFileController
    {

        PetModel _model;
        Window _window;
        public PetFileController(PetModel model, Window window)
        {
            _model = model;
            _window = window;
        }        
        
        public PetFile CreateInstance() => new PetFile(_model,  _window);
        
        public void Export(ZipArchive archive)
        {
            PetFile petFile = CreateInstance();
            
            string newJsonText = JsonSerializer.Serialize(petFile, JsonHelper.Options);
            string entryPath = $"Pets/{petFile.Name}_{petFile.InstanceId.Substring(0, 8)}.json";
            
            ZipArchiveEntry petEntry = archive.CreateEntry(entryPath);

            // そのエントリーの中にJSONのテキストを書き込む
            using (StreamWriter writer = new StreamWriter(petEntry.Open()))
            {
                writer.Write(newJsonText);
            }
        }

        public void Import(PetFile petFile)
        {
            _model.InstanceId = petFile.InstanceId;
            _model.Name.Value =  petFile.Name;
            _model.SpritesId.Value = petFile.SpriteId;
            _model.SetHunger(petFile.Hunger);
            _model.SetAffection(petFile.Affection);
            //todo: LastTimeを参照し、メーターを下げる
            _window.Left = petFile.PositionX;
            _window.Top = petFile.PositionY;
            
            
        }
    }
}