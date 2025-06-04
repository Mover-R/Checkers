using Model.Core.Game;
using Model.Core.Pieces;
using Model.Data.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Model.Data.SaveLoad
{
    public class SerializeGameXML : Serializer
    {
        public override string Extension => "xml";


        public override void SerializeGame(GameState game, string file = "game")
        {
            SelectFolder(FolderPath);
            SelectFile(file);

            int[,] originalMap = game.Map;
            int rows = originalMap.GetLength(0);
            int cols = originalMap.GetLength(1);

            int[][] jaggedMap = new int[rows][];
            for (int r = 0; r < rows; r++)
            {
                jaggedMap[r] = new int[cols];
                for (int c = 0; c < cols; c++)
                {
                    jaggedMap[r][c] = originalMap[r, c];
                }
            }

            var dtoXml = new GameStateDTO_XML
            {
                Map = jaggedMap,
                WhiteMove = game.WhiteMove,
                Pieces = new List<PieceDTO>()
            };

            foreach (var kv in game.Pieces)
            {
                dtoXml.Pieces.Add(new PieceDTO
                {
                    Type = kv.Value is QueenChecker ? "Queen" : "Man",
                    Row = kv.Key.Item1,
                    Col = kv.Key.Item2,
                    Color = kv.Value.Color
                });
            }

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(GameStateDTO_XML));

                if (Path.GetExtension(FilePath).ToLower() != ".xml")
                {
                    FilePath = Path.ChangeExtension(FilePath, ".xml");
                }

                using (var fs = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
                {
                    xmlSerializer.Serialize(fs, dtoXml);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сериализации в XML: {ex.Message}");
            }
        }


        public override GameState DeSerializeGame(string file = "game")
        {
            SelectFolder(FolderPath);
            SelectFile(file);

            if (!File.Exists(FilePath))
                return null; 

            try
            {
                var xmlSerializer = new XmlSerializer(typeof(GameStateDTO_XML));
                GameStateDTO_XML dtoXml;

                using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    dtoXml = (GameStateDTO_XML)xmlSerializer.Deserialize(fs);
                }

                int rows = dtoXml.Map.Length;
                int cols = dtoXml.Map[0].Length;
                int[,] map2D = new int[rows, cols];
                for (int r = 0; r < rows; r++)
                {
                    for (int c = 0; c < cols; c++)
                    {
                        map2D[r, c] = dtoXml.Map[r][c];
                    }
                }

                var pieces = new Dictionary<(int, int), Piece>();
                foreach (var pieceDto in dtoXml.Pieces)
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

                return new GameState(map2D, pieces, dtoXml.WhiteMove);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при десериализации из XML: {ex.Message}");
                return null;
            }
        }
    }
}
