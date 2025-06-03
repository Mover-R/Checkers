using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace SaveLoad
{
    public interface IFileManager
    {
        string FolderPath { get; }
        string FilePath { get; }
        void SelectFolder(string path);
        void SelectFile(string name);
    }
}
