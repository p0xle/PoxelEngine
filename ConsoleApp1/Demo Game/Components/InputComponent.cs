using EngineTest.Models;
using PoxelEngine.Models;
using System.Windows.Forms;

namespace EngineTest.Demo_Game.Components
{
    public class InputComponent : Component
    {
        public InputComponent(GameObject gameObject, Directions directions) : base(gameObject)
        {
            this.Directions = directions;
        }

        private readonly Directions Directions;

        public override bool Init()
        {
            return true;
        }

        public override void Update()
        {

        }

        public void HandleKeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    this.Directions.Up = true;
                    break;
                case Keys.S:
                    this.Directions.Down = true;
                    break;
                case Keys.A:
                    this.Directions.Left = true;
                    break;
                case Keys.D:
                    this.Directions.Right = true;
                    break;
            }
        }

        public void HandleKeyUp(Keys key)
        {
            switch (key)
            {
                case Keys.W:
                    this.Directions.Up = false;
                    break;
                case Keys.S:
                    this.Directions.Down = false;
                    break;
                case Keys.A:
                    this.Directions.Left = false;
                    break;
                case Keys.D:
                    this.Directions.Right = false;
                    break;
            }
        }

        protected override void Dispose(bool disposing)
        {

        }
    }
}
