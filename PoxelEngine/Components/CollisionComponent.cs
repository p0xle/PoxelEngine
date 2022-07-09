using PoxelEngine.Models;

namespace PoxelEngine.Components
{
    public class CollisionComponent : Component
    {
        public CollisionComponent(GameObject gameObject) : base(gameObject)
        {
        }

        public override bool Init()
        {
            return true;
        }


        public override void Update()
        {

        }

        public GameObject IsColliding(string tag)
        {
            var gameObjects = Engine.GetGameObjects(tag);

            if (gameObjects != null)
            {
                foreach (var gameObject in gameObjects)
                {
                    if (this.Transform.Position.X < gameObject.Transform.Position.X + gameObject.Transform.Scale.X
                        && this.Transform.Position.X + this.Transform.Scale.X > gameObject.Transform.Position.X
                        && this.Transform.Position.Y < gameObject.Transform.Position.Y + gameObject.Transform.Scale.Y
                        && this.Transform.Position.Y + this.Transform.Scale.Y > gameObject.Transform.Position.Y)
                    {
                        return gameObject;
                    }
                }
            }

            return null;
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
