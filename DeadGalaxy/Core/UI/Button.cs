using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Numerics;
using DeadGalaxy.Core.Helpers;
using DeadGalaxy.Core.UI;
using Raylib_cs;
using static System.Net.Mime.MediaTypeNames;

namespace DeadGalaxy.Core.UI
{
    internal class Button
    {
        public void SetText(string Text) { _text = Text; }
        public string GetText() { return _text; }

        public int PositionX = 0;
        public int PositionY = 0;
        public int FontSize = 0;
        public Color TextColor = Color.White;

        private string _text = string.Empty;

        private readonly Color Background = new(100, 100, 100, 200);

        private bool _isClick = false;

        public bool IsClick() { return _isClick; }

        public Button(string Text, int PosX, int PosY, int Fontsize, Color Textcolor)
        {
            SetText(Text);
            PositionX = PosX;
            PositionY = PosY;

            FontSize = Fontsize;

            TextColor = Textcolor;
        }

        public void Update()
        {
            var width = Raylib.GetScreenWidth();
            var height = Raylib.GetScreenHeight();
            var mousePosition = Raylib.GetMousePosition();

            Rectangle Field = new Rectangle(PositionX - 5, PositionY - 5, 180, 50);
            if (Raylib.CheckCollisionPointRec(mousePosition, Field))
            {
                Raylib.DrawRectangle((int)Field.X, (int)Field.Y, (int)Field.Width, (int)Field.Height, Background);

                if (Raylib.IsMouseButtonDown(MouseButton.Left))
                {
                    _isClick = true;
                }
            }
            else
            {
                _isClick = false;
            }

            Raylib.DrawText(_text, PositionX, PositionY, FontSize, TextColor);
        }
    }
}
