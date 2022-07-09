using EngineTest.Demo_Game.Components;
using EngineTest.Models;
using PoxelEngine.Components;
using PoxelEngine.Models;

namespace EngineTest.Demo_Game
{
    public class Player : GameObject
    {
        protected Player(Transform transform, string tag, float playerSpeed = 3f) : base(tag, transform)
        {
            this.Speed = playerSpeed;
        }

        public static Player Create(Transform transform, string tag, string path, float playerSpeed = 3f)
        {
            var player = new Player(transform, tag, playerSpeed);
            return player.Initialize(path) ? player : null;
        }

        protected bool Initialize(string path)
        {
            if (!this.LoadComponent(new SpriteComponent(this, path)))
                return false;
            if (!this.LoadComponent(new MoveComponent(this, this.Speed, this.Directions)))
                return false;
            if (!this.LoadComponent(new InputComponent(this, this.Directions)))
                return false;
            if (!this.LoadComponent(new CoinComponent(this)))
                return false;

            return base.Initialize();
        }

        public float Speed;
        public Directions Directions = new Directions();
    }
}
