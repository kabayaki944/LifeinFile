using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LifeinFile.Models;
using LifeinFile.Views.Pets;
using System.Diagnostics;

namespace LifeinFile.Core.Pets
{
    public static class PetSpritesDictionary
    {
        public const int DEFAULT_PET_ID = -1;
        private static Dictionary<int, PetSprites> _dictionary = new Dictionary<int, PetSprites>();

        public static bool TryGet(int id, out PetSprites sprite)
        {
            if(_dictionary.Count == 0) LoadAll();
            return _dictionary.TryGetValue(id, out sprite);
        }

        public static bool TryGetDefault(out PetSprites sprite) => TryGet(DEFAULT_PET_ID, out sprite);

        public static int Count{ get
        {
            if(_dictionary.Count == 0) LoadAll();
            return _dictionary.Count;
        }}
        
        // 検索するフォルダのパス（実行ファイルのある場所から Assets/Graphic/Pets を探す）
        private static readonly string SearchDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Graphic", "Pets");

        /// <summary>
        /// Assets/Graphic/Pets フォルダ内のすべての Sprites.json を読み込み、辞書に登録する
        /// </summary>
        static void LoadAll()
        {
            // 読み込み前に一度辞書を空にする（再読み込み対応）
            _dictionary.Clear();

            // フォルダが存在しない場合は処理を中断
            if (!Directory.Exists(SearchDirectory))
            {
                Console.WriteLine($"error: Not found Pet file path ({SearchDirectory})");
                return;
            }

            // TargetDirectory 以下のすべての "Sprites.json" のパスを取得
            string[] jsonFiles = Directory.GetFiles(SearchDirectory, "Sprites.json", SearchOption.AllDirectories);

            foreach (string filePath in jsonFiles)
            {
                try
                {
                    // JSONを読み込んでクラスに変換
                    string jsonString = File.ReadAllText(filePath);
                    PetSprites spriteData = JsonSerializer.Deserialize<PetSprites>(jsonString);

                    if (spriteData != null)
                    {
                        ConvertAbsPath(spriteData,  filePath);
                        AddToDic(spriteData);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error ({filePath}): {ex.Message}");
                }
            }
        }

        static void ConvertAbsPath(PetSprites sprites, string filePath)
        {
            // JSONが置かれているフォルダのパスを取得
            string directoryPath = Path.GetDirectoryName(filePath);

            // ImagePath にファイル名が入っている場合、絶対パスに変換する
            if (!string.IsNullOrEmpty(sprites.Normal))
                sprites.Normal = Path.Combine(directoryPath, sprites.Normal);
            if (!string.IsNullOrEmpty(sprites.Smile))
                sprites.Smile = Path.Combine(directoryPath, sprites.Smile);
            if (!string.IsNullOrEmpty(sprites.Trouble))
                sprites.Trouble = Path.Combine(directoryPath, sprites.Trouble);
            if (!string.IsNullOrEmpty(sprites.Dizzy))
                sprites.Normal = Path.Combine(directoryPath, sprites.Dizzy);
        }

        static void AddToDic(PetSprites spriteData)
        {
            if (!_dictionary.ContainsKey(spriteData.Id))
            {
                _dictionary.Add(spriteData.Id, spriteData);
                Console.WriteLine($"success: ID={spriteData.Id}, Name={spriteData.Name}");
            }
            else
            {
                Console.WriteLine($"warning: ID {spriteData.Id}({spriteData.Name}),({_dictionary[spriteData.Id].Name})  is override");
            }
        }
    }
}