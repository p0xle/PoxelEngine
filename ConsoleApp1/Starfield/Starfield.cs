using PoxelEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineTest.Starfield
{
    class Starfield : Engine
    {
        public Starfield(Size screenSize, string title, bool isDebug = false) : base(screenSize, title, isDebug)
        {
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.StopGame();
                    break;
            }
        }

        public override void MouseWheelMoving(MouseEventArgs e)
        {
        }

        public override void OnDraw()
        {
        }

        public override void OnLoad()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}
