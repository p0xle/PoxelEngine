using PoxelEngine.Components;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;

namespace PoxelEngine.Models
{
    public abstract class GameObject : IDisposable
    {
        public GameObject(string tag, Transform transform)
        {
            this.Tag = tag;
            this.Transform = transform;
        }

        public GameObject(string tag, Transform transform, params Component[] components)
        {
            this.Tag = tag;
            this.Transform = transform;

            foreach (var c in components)
                this.AddComponent(c);
        }

        public string Tag { get; set; }
        public Transform Transform { get; set; } = new Transform();

        public string ClassName => this.GetType().Name;

        private List<Component> Components { get; set; } = new List<Component>();

        protected bool disposedValue;

        protected virtual bool Initialize()
        {
            if (!this.LoadCollisionComponent())
                return false;

            this.RegisterSelf();
            return true;
        }

        public Component GetComponent(Type type)
        {
            if (!type.IsSubclassOf(typeof(Component)))
                return null;

            foreach (var component in this.Components)
            {
                if (type == component.GetType())
                {
                    return component;
                }
            }

            return null;
        }
        public T GetComponent<T>() where T : Component
        {
            if (this.Components is null)
                return default;

            foreach (var item in this.Components)
            {
                if (item.GetType() == typeof(T))
                {
                    return (T)item;
                }
            }

            return default;
        }

        /// <summary>Compares the <see cref="GameObject.Tag">tag</see> to another specified tag</summary>
        /// <param name="tag">Tag to compare with</param>
        /// <returns>true, if both tags are equal, otherwise false. If the specified tag is null or empty, the method will also return false.</returns>
        public bool CompareTag(string tag)
        {
            if (string.IsNullOrEmpty(tag))
                return false;

            return this.Tag.Equals(tag);
        }

        /// <summary>Adds a component to the internal list of components</summary>
        /// <param name="component">Component to add</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddComponent(Component component)
        {
            if (component is null)
                throw new ArgumentNullException(nameof(component));

            if (this.Components is null)
                this.Components = new List<Component>();

            if (!this.Components.Contains(component))
                this.Components.Add(component);
        }
        /// <summary>Removes a component from the internal list of components</summary>
        /// <param name="component">Component to remove</param>
        public void RemoveComponent(Component component)
        {
            if (this.Components is null || this.Components.Count == 0)
                return;

            if (this.Components.Contains(component))
                this.Components.Remove(component);
        }

        /// <summary>Should be automatically called in constructor:<br/>Registers itself in the engine</summary>
        public void RegisterSelf()
        {
            Engine.RegisterGameObject(this);
        }
        /// <summary>Unregisters itself in the <see cref="Engine"/> and disposes all <see cref="Component">Components</see></summary>
        public void DestroySelf()
        {
            Engine.UnregisterGameObject(this);
            this.Dispose();
        }

        /// <summary>Calls the <see cref="Component.Update">Update</see> method for every added <see cref="Component"/></summary>
        public void Update()
        {
            foreach (var component in this.Components)
            {
                component.Update();
            }
        }

        public bool LoadComponent(Component component)
        {
            if (!this.InitComponent(component))
                return false;

            this.AddComponent(component);
            return true;
        }

        protected bool InitComponent(Component component)
        {
            if (!component.Init())
            {
                Log.Error($"[{this.ClassName}]({this.Tag}) - Error occured while creating the {component.GetType().Name}");
                return false;
            }

            return true;
        }

        protected abstract void DisposeAC();

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    foreach (var component in this.Components)
                    {
                        component?.Dispose();
                    }

                    this.DisposeAC();
                }

                this.Components = null;
                this.disposedValue = true;
            }
        }

        private bool LoadCollisionComponent()
        {
            var component = new CollisionComponent(this);

            if (!this.InitComponent(component))
                return false;

            this.AddComponent(component);
            return true;
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
