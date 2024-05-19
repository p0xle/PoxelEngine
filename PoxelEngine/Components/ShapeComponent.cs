using PoxelEngine.Models;
using PoxelEngine.Models.Shapes;

namespace PoxelEngine.Components
{
    public class ShapeComponent : Component
    {
        public ShapeComponent(GameObject parent, Shape shape) : base(parent)
        {
            this.shape = shape;
        }

        private readonly Shape shape;

        public override bool Init() => true;

        public override void Update()
        {
            this.shape.Draw();
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
