using PoxelEngine;
using PoxelEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Starfield.Components
{
    public class StarPositionComponent : Component
    {
        public StarPositionComponent(GameObject gameObject, int speed) : base(gameObject)
        {
            this.speed = speed;

            gameObject.Transform.Position.X = this.random.Next(-Engine.Width, Engine.Width);
            gameObject.Transform.Position.Y = this.random.Next(-Engine.Height, Engine.Height);

            this.z = this.random.Next(Engine.Width);

            this.pz = this.z;
        }

        public int z;
        public int pz;

        public readonly int speed;

        private readonly Random random = new Random();

        public override bool Init() => true;

        public override void Update()
        {
            this.z -= this.speed;
            if (this.z < 1)
            {
                this.z = Engine.Width;
                this.GameObject.Transform.Position.X = this.random.Next(-Engine.Width, Engine.Width);
                this.GameObject.Transform.Position.Y = this.random.Next(-Engine.Height, Engine.Height);
                this.pz = this.z;
            }
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
