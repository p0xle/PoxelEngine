using System.Drawing;

namespace PoxelEngine.Models.Shapes
{
    public class Elipse : Shape
    {
        public Elipse(Transform transform, Color? color = null) : base(transform, color)
        {
        }

        public override void Draw()
        {
            var graphics = Engine.GetGraphics();
            graphics.FillEllipse(new SolidBrush(this.color), this.transform.Position.X, this.transform.Position.Y, this.transform.Scale.X, this.transform.Scale.Y);
        }
    }
}
