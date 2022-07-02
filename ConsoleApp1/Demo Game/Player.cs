using EngineTest.Demo_Game.Components;
using EngineTest.Models;
using PoxelEngine.Models;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Demo_Game
{
    public class Player : GameObject
    {
        // Todo: Collision Detection

        protected Player(Transform transform, string tag, float playerSpeed = 3f) : base(tag, transform)
        {
            this.Speed = playerSpeed;
        }

        public static Player Create(Transform transform, string tag, string path, float playerSpeed = 3f)
        {
            var player = new Player(transform, tag, playerSpeed);
            player.Initialize(path);
            return player;
        }

        private void Initialize(string path)
        {
            if (!this.LoadSpriteComponent(path))
                return;
            if (!this.LoadMoveComponent())
                return;

            this.GetComponent(typeof(SpriteComponent));

            this.RegisterSelf();
        }

        public float Speed;
        public Directions Directions = new Directions();

        private bool LoadSpriteComponent(string path)
        {
            var component = new SpriteComponent(this, path);

            if (!this.InitComponent(component))
                return false;

            this.AddComponent(component);
            return true;
        }

        private bool LoadMoveComponent()
        {
            var component = new MoveComponent(this, this.Speed, this.Directions);

            if (!this.InitComponent(component))
                return false;

            this.AddComponent(component);
            return true;
        }
    }
}
