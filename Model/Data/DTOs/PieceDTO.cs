using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Data.DTOs
{
    public class PieceDTO
    {
        public string Type { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public bool Color { get; set; }
    }
}
