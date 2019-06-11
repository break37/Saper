using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saper.View
{
    public interface IGameWindow
    {
        string Time { set; }
        System.Windows.Forms.Timer GameTime { get; set; }

        void DrawTiles(List<Tile> tilesToDraw);

        event Action Loaded;
        event Action TimeStarted;
        event Action<int, int> Dig;
        event Action<int, int> FlagToggled;
        event Action GameEnded;
        event Action Reset;
    }
}
