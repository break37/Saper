using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Saper.View
{
    public interface IGameBoardPanel
    {
        int[,] clickPosition { get; }

        Graphics gameBoardCanvas { get; }

        event Action<int, int> Dig;
        event Action Loaded;
    }
}
