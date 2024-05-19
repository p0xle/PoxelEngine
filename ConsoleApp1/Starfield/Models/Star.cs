using EngineTest.Starfield.Components;
using PoxelEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Starfield.Models
{
    public class Star : GameObject
    {
        protected Star(string tag, Transform transform) : base(tag, transform) { }

        public static Star Create(string tag, Transform transform, int speed = 3)
        {
            var star = new Star(tag, transform);
            return star.Initialize(speed) ? star : default;
        }

        protected bool Initialize(int speed = 3)
        {
            if (!this.LoadComponent(new StarPositionComponent(this, speed)))
                return false;

            return base.Initialize();
        }

        protected override void DisposeAC()
        {

        }
    }
}
