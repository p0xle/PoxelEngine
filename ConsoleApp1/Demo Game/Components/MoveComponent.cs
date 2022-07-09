using EngineTest.Models;
using PoxelEngine.Components;
using PoxelEngine.Models;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Demo_Game.Components
{
    public class MoveComponent : Component
    {
        public MoveComponent(GameObject parent, float speed, Directions directions) : base(parent)
        {
            this.Speed = speed;
            this.Directions = directions;
        }

        private readonly float Speed;

        private readonly Directions Directions;

        public override bool Init() => true;

        public override void Update()
        {
            var collisionComponent = this.GameObject?.GetComponent<CollisionComponent>();
            if (collisionComponent is null)
            {
                Log.Error($"[{nameof(MoveComponent)}]({this.Tag}) - No {nameof(CollisionComponent)} found");
                return;
            }

            if (this.Directions.Up)
            {
                this.Transform.Translate(new Vector2(0, -this.Speed));
                if (collisionComponent.IsColliding("Ground") != null)
                    this.Transform.Translate(new Vector2(0, this.Speed));
            }
            if (this.Directions.Down)
            {
                this.Transform.Translate(new Vector2(0, this.Speed));
                if (collisionComponent.IsColliding("Ground") != null)
                    this.Transform.Translate(new Vector2(0, -this.Speed));
            }
            if (this.Directions.Left)
            {
                this.Transform.Translate(new Vector2(-this.Speed, 0));
                if (collisionComponent.IsColliding("Ground") != null)
                    this.Transform.Translate(new Vector2(this.Speed, 0));
            }
            if (this.Directions.Right)
            {
                this.Transform.Translate(new Vector2(this.Speed, 0));
                if (collisionComponent.IsColliding("Ground") != null)
                    this.Transform.Translate(new Vector2(-this.Speed, 0));
            }
        }

        protected override void Dispose(bool disposing) { }
    }
}
