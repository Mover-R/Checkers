using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Model.Core.Game;
using Model.Data.DTOs;
using Model.Core.Pieces;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Model.Data.SaveLoad
{
    public abstract class Serializer : FileSerializer
    {
        public string Folder;
        public override string Extension => "json";

        public Serializer(string folderPath = @"Saves")
        {
            FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderPath);
            Directory.CreateDirectory(FolderPath);
        }

        public abstract void SerializeGame(GameState game, string file = "game");

        public abstract GameState DeSerializeGame(string file = "game");
    }
}