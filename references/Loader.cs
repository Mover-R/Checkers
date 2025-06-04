using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Сheckers.references
{
    public class Loader
    {
        public static Bitmap LoadImage(string relativePath, int cellSize)
        {
            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            return new Bitmap(new Bitmap(fullPath), new Size(cellSize - 1, cellSize - 1));
        }
        public static Bitmap LoadImage(string relativePath, int sizeX, int sizeY)
        {
            string fullPath = Path.Combine(Application.StartupPath, relativePath);
            return new Bitmap(new Bitmap(fullPath), new Size(sizeX - 1, sizeY - 1));
        }
    }
}
