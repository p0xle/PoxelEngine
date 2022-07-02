using PoxelEngine;
using PoxelEngine.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EngineTest.Demo_Game
{
    class DemoGame : Engine
    {
        public DemoGame() : base(screenSize: new Vector2(windowWidth, windowHeight), title: title, isDebug: true) { }

        private const string title = "Engine Demo Game";

        private const int windowHeight = 480;
        private const int windowWidth = 460;

        private const float zoomSpeed = .1f;

        private const int groundSize = 64;

        private Player Player;

        private readonly string[,] Map =
        {
            {"g", "g", "g", "g", "g", "g", "g" },
            {"g", ".", ".", ".", ".", "c", "g" },
            {"g", ".", ".", ".", "g", "c", "g" },
            {"g", ".", "g", "t", "g", "c", "g" },
            {"g", "c", "g", "p", "g", "c", "g" },
            {"g", "c", "g", ".", ".", "c", "g" },
            {"g", "g", "g", "g", "g", "g", "g" },
        };

        public override void OnLoad()
        {
            this.BackgroundColor = Color.Black;

            for (int i = 0; i < this.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Map.GetLength(1); j++)
                {
                    if (this.Map[j, i] == "t")
                    {
                        Text.Create("Test", new Transform(new Vector2(i * groundSize, j * groundSize), new Vector2(100, 100), 10), "TestText",
                            new FontOptions(family: new FontFamily("Comic Sans MS"), style: FontStyle.Underline));
                    }

                    if (this.Map[j, i] == "p")
                    {
                        this.Player = Player.Create(transform: new Transform(new Vector2(i * groundSize, j * groundSize), new Vector2(48, 48)),
                            tag: "Player", path: "player", playerSpeed: 5f);
                    }
                }
            }

        }

        public override void OnDraw()
        {

        }

        public override void OnUpdate()
        {

        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    this.Player.Directions.Up = true;
                    break;
                case Keys.S:
                    this.Player.Directions.Down = true;
                    break;
                case Keys.A:
                    this.Player.Directions.Left = true;
                    break;
                case Keys.D:
                    this.Player.Directions.Right = true;
                    break;
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    this.Player.Directions.Up = false;
                    break;
                case Keys.S:
                    this.Player.Directions.Down = false;
                    break;
                case Keys.A:
                    this.Player.Directions.Left = false;
                    break;
                case Keys.D:
                    this.Player.Directions.Right = false;
                    break;
                case Keys.Escape:
                    this.StopGame();
                    break;
            }
        }

        public override void MouseWheelMoving(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                this.CameraZoom.X += zoomSpeed;
                this.CameraZoom.Y += zoomSpeed;
            }
            else
            {
                this.CameraZoom.X -= zoomSpeed;
                this.CameraZoom.Y -= zoomSpeed;
            }
        }
    }
}
