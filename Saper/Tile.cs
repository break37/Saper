using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Path { get; set; }

        public Tile (int x, int y, string path)
        {
            X = x*25;
            Y = y*25;
            Path = path;
        }
    }
}
