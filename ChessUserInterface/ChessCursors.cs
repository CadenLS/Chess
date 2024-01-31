using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChessUserInterface
{
    public static class ChessCursors
    {
        public static readonly Cursor WhiteCursor = LoadCursor("Assets/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursor("Assets/CursorB.cur");
        private static Cursor LoadCursor(string filepath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filepath, UriKind.Relative)).Stream;
            return new Cursor(stream,true);

        }
    }
}
