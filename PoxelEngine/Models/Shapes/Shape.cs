using System.Drawing;

namespace PoxelEngine.Models.Shapes
{
    public abstract class Shape
    {
        protected Shape(Transform transform, Color? color = null)
        {
            this.color = color ?? Color.White;
            this.transform = transform;
        }

        protected readonly Color color;
        protected readonly Transform transform;

        public abstract void Draw();
    }
}
