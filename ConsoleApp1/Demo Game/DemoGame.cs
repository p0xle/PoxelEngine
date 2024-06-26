﻿using EngineTest.Demo_Game.Components;
using EngineTest.Demo_Game.Models;
using PoxelEngine;
using PoxelEngine.Utility;
using System.Drawing;
using System.Windows.Forms;

namespace EngineTest.Demo_Game
{
    class DemoGame : Engine
    {
        public DemoGame() : base(new Size(windowWidth, windowHeight), title: title, isDebug: true) { }

        private const string title = "Engine Demo Game";

        private const int windowHeight = 480;
        private const int windowWidth = 460;

        private const float zoomSpeed = .1f;

        private Scene CurrentScene { get; set; }

        public override void OnLoad()
        {
            this.BackgroundColor = Color.Black;

            this.CurrentScene = this.CreateMainScene();
            this.CurrentScene.Load();
        }

        private Scene CreateMainScene()
        {
            string[,] map = {
                {"g", "g", "g", "g", "g", "g", "g" },
                {"g", ".", ".", ".", ".", "c", "g" },
                {"g", ".", ".", ".", "g", "c", "g" },
                {"g", ".", "g", "g", "g", "c", "g" },
                {"g", "c", "g", "p", "g", "c", "g" },
                {"g", "c", "g", ".", ".", "c", "g" },
                {"g", "g", "g", "g", "g", "g", "g" },
            };

            var sceneSettings = new SceneSettings(map);
            return new Scene(sceneSettings, "mainScene");
        }

        private Scene CreateSecondScene()
        {
            string[,] map = {
                {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g" },
                {"g", ".", ".", ".", ".", ".", ".", ".", ".", ".", "g" },
                {"g", ".", "g", "c", "c", "g", "g", "g", "g", ".", "g" },
                {"g", ".", "g", "c", "c", "g", "c", "c", "g", ".", "g" },
                {"g", ".", "g", "g", "g", "g", "c", "c", "g", ".", "g" },
                {"g", ".", ".", ".", ".", "g", ".", "g", "g", ".", "g" },
                {"g", ".", "g", "g", "c", "g", ".", ".", "g", ".", "g" },
                {"g", ".", "p", "g", "c", "g", "g", ".", "g", ".", "g" },
                {"g", ".", "g", "g", "g", "g", ".", ".", "g", ".", "g" },
                {"g", ".", ".", "c", "c", "g", ".", ".", ".", ".", "g" },
                {"g", "g", "g", "g", "g", "g", "g", "g", "g", "g", "g" },
            };

            var sceneSettings = new SceneSettings(map);
            return new Scene(sceneSettings, "mainScene");
        }

        public override void OnDraw()
        {

        }

        public override void OnUpdate()
        {
            var coinComponent = this.CurrentScene.Player?.GetComponent<CoinComponent>();
            if (coinComponent != null)
            {
                if (coinComponent.Coins <= 0)
                {
                    Log.Info($"[{nameof(DemoGame)}] - All coins collected.. Congratulations!");
                    
                    this.CurrentScene.DestroySelf();
                    ClearGameObjects();

                    this.CurrentScene = this.CreateSecondScene();
                    this.CurrentScene.Load();
                }
            }
        }

        public override void GetKeyDown(KeyEventArgs e)
        {
            var inputComponent = this.CurrentScene.Player?.GetComponent<InputComponent>();
            if (inputComponent != null)
                inputComponent.HandleKeyDown(e.KeyCode);

            switch (e.KeyCode)
            {
                default:
                    break;
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            var inputComponent = this.CurrentScene.Player?.GetComponent<InputComponent>();
            if (inputComponent != null)
                inputComponent.HandleKeyUp(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.StopGame();
                    break;
                default:
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
