using System.Drawing;

namespace PoxelEngine.Models
{
    public class Text : GameObject
    {
        protected Text(string content, Transform transform, string tag, FontOptions fontOptions = null, Color? color = null) : base(tag, transform)
        {
            this.Content = content;

            if (fontOptions is null)
                fontOptions = new FontOptions();

            this.Font = new Font(fontOptions.Family, fontOptions.Size, fontOptions.Style);
            this.Color = color ?? Color.White;

            this.RegisterSelf();
        }

        public static Text Create(string content, Transform transform, string tag, FontOptions fontOptions = null, Color? color = null)
        {
            Text text = new Text(content, transform, tag, fontOptions, color);
            text.Initialize();
            return text;
        }

        private new bool Initialize()
        {
            if (this.LoadComponent(new TextRenderer(this)))
                return false;

            return base.Initialize();
        }

        public string Content { get; set; }
        public Font Font { get; set; }
        public Color Color { get; set; }
    }

    public class FontOptions
    {
        public FontOptions(FontFamily family = null, float size = 16, FontStyle style = FontStyle.Regular)
        {
            this.Family = family ?? FontFamily.GenericSerif;
            this.Size = size;
            this.Style = style;
        }

        public FontFamily Family { get; set; }
        public float Size { get; set; }
        public FontStyle Style { get; set; }
    }

    public class TextRenderer : Component
    {
        public TextRenderer(GameObject parent) : base(parent)
        {

        }

        public override bool Init()
        {
            return true;
        }

        public override void Update()
        {
            if (this.GameObject is Text textObject)
            {
                var graphics = Engine.GetGraphics();
                graphics.DrawString(textObject.Content, textObject.Font, new SolidBrush(textObject.Color),
                    this.Transform.Position.X, this.Transform.Position.Y);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    
                }

                this.disposedValue = true;
            }
        }
    }
}
