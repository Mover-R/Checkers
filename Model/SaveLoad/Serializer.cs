using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Сheckers;
using Model;

namespace SaveLoad
{
    public class GameStateDTO
    {
        public int[,] Map { get; set; }
        public List<PieceDTO> Pieces { get; set; }
        public bool WhiteMove { get; set; }
    }

    public class PieceDTO
    {
        public string Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Color { get; set; }
    }

    public class Serializer : FileSerializer
    {
        private string Folder;
        public override string Extension => "json";

        public Serializer(string FolderPath = @"C:\Users\denis\source\repos\Сheckers\Saves")
        {
            Folder = FolderPath;
            Directory.CreateDirectory(FolderPath);
        }

        public void SerializeGame(GameState game, string file = "game")
        {
            SelectFolder(Folder);
            SelectFile(file);
            var dto = new GameStateDTO
            {
                Map = game.Map,
                WhiteMove = game.WhiteMove,
                Pieces = new List<PieceDTO>()
            };

            foreach (var kv in game.Pieces)
            {
                dto.Pieces.Add(new PieceDTO
                {
                    Type = kv.Value is QueenChecker ? "Queen" : "Man",
                    Row = kv.Key.Item1,
                    Col = kv.Key.Item2,
                    Color = kv.Value.Color
                });
            }
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(dto, settings));
        }

        public GameState DeSerializeGame(string file = "game")
        {
            SelectFolder(Folder);
            SelectFile(file);

            if (!File.Exists(FilePath))
                return null;

            try
            {
                // Десериализуем DTO
                var dto = JsonConvert.DeserializeObject<GameStateDTO>(File.ReadAllText(FilePath));

                // Конвертируем DTO обратно в GameState
                var pieces = new Dictionary<(int, int), Piece>();

                foreach (var pieceDto in dto.Pieces)
                {
                    var pos = (pieceDto.Row, pieceDto.Col);
                    Piece piece;

                    if (pieceDto.Type == "Queen")
                    {
                        piece = new QueenChecker(pieceDto.Color, pos);
                    }
                    else
                    {
                        piece = new ManChecker(pieceDto.Color, pos);
                    }

                    pieces[pos] = piece;
                }

                return new GameState(dto.Map, pieces, dto.WhiteMove);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing game: {ex.Message}");
                return null;
            }
        }
    }
}