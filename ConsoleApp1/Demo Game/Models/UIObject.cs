using PoxelEngine.Components;
using PoxelEngine.Models;

namespace EngineTest.Demo_Game.Models
{
    public class UIObject : GameObject
    {
        public UIObject() : base(string.Empty, new Transform()) { }
        protected UIObject(string tag, string name, Transform transform) : base(tag, transform) 
        { 
            this.Name = name; 
        }

        public static UIObject CreateReference(string path)
        {
            var uiObject  = new UIObject();
            return uiObject.Initialize(path) ? uiObject : null;
        }

        public static UIObject Create(string tag, string name, Transform transform, string path)
        {
            var uiObject = new UIObject(tag, name, transform);
            return uiObject.Initialize(path) ? uiObject : null;
        }

        public static UIObject Create(string tag, string name, Transform transform, UIObject reference)
        {
            var uiObject = new UIObject(tag, name, transform);
            return uiObject.Initialize(reference) ? uiObject : null;
        }

        public string Name { get; set; } = string.Empty;

        protected bool Initialize(string path)
        {
            if (!this.LoadComponent(new SpriteComponent(this, path)))
                return false;

            return base.Initialize();
        }

        protected bool Initialize(UIObject reference)
        {
            if (reference is null)
                return false;

            if (!this.LoadComponent(new SpriteComponent(this, reference.GetComponent<SpriteComponent>()?.Sprite)))
                return false;

            return base.Initialize();
        }
    }
}
