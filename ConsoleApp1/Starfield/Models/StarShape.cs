using PoxelEngine;
using PoxelEngine.Components;
using PoxelEngine.Models;
using PoxelEngine.Models.Shapes;
using System.Drawing;

namespace EngineTest.Starfield.Models
{
    public class StarShape : Shape
    {
        public StarShape(GameObject transform, Color? color = null) : base(transform, color)
        {
        }

        private readonly GameObject parent;

        public override void Draw()
        {
            var graphics = Engine.GetGraphics();

            var pos = 

        }
    }
}
