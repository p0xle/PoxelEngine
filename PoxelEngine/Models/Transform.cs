using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PoxelEngine.Models
{
    public class Transform
    {
        public Transform()
        {
            this.Position = Vector2.One;
            this.Scale = Vector2.One;
            this.Layer = 1;
        }

        public Transform(Vector2 position, Vector2 scale, int layer = 1)
        {
            this.Position = position;
            this.Scale = scale;
            this.Layer = layer;
        }

        /// <summary>The position of the transform in world space</summary>
        public Vector2 Position;
        /// <summary>The scale of the transform in world space</summary>
        public Vector2 Scale;
        public int Layer;

        public void Translate(Vector2 translation)
        {
            this.Position += translation;
        }
    }
}
