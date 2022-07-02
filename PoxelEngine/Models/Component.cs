using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoxelEngine.Models
{
    public abstract class Component : IDisposable
    {
        public Component(GameObject gameObject, Transform transform)
        {
            this.GameObject = gameObject;
            this.Transform = transform;
        }

        public GameObject GameObject { get; set; }
        public Transform Transform { get; set; }

        public string Tag
        {
            get => this.GameObject.Tag;
            set => this.GameObject.Tag = value;
        }

        protected bool disposedValue;

        public abstract bool Init();
        public abstract void Update();
        public abstract void Dispose(bool disposing);

        public Component GetComponent(Type type)
        {
            return this.GameObject.GetComponent(type);
        }

        /// <summary><inheritdoc cref="GameObject.CompareTag(string)"/></summary>
        /// <param name="tag"><inheritdoc cref="GameObject.CompareTag(string)"/></param>
        /// <returns><inheritdoc cref="GameObject.CompareTag(string)"/></returns>
        public bool CompareTag(string tag)
        {
            return this.GameObject.CompareTag(tag);
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            //GC.SuppressFinalize(this);
        }
    }
}
