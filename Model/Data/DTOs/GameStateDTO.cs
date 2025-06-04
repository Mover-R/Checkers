using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.DTOs
{
    public class GameStateDTO
    {
        public int[,] Map { get; set; }
        public List<PieceDTO> Pieces { get; set; }
        public bool WhiteMove { get; set; }
    }
    
    [Serializable]
    public class GameStateDTO_XML
    {
        public GameStateDTO_XML() { }
        public int[][] Map { get; set; }
        public bool WhiteMove { get; set; }
        public List<PieceDTO> Pieces { get; set; } = new List<PieceDTO>();
    }
}
