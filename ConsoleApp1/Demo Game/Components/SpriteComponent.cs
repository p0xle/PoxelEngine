﻿using PoxelEngine;
using PoxelEngine.Models;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Demo_Game.Components
{
    public class SpriteComponent : Component
    {
        public SpriteComponent(GameObject parent, string path) : base(parent, parent.Transform)
        {
            this.Path = path;
        }

        public SpriteComponent(GameObject parent, SpriteComponent reference) : base(parent, parent.Transform)
        {
            this.Sprite = reference.Sprite;
        }

        public static (bool success, SpriteComponent component) CreateReference(string path)
        {
            var component = new SpriteComponent(default, path);
            var result = component.Init();
            return (result, component);
        }

        public Image Sprite { get; set; }
        public string Path { get; set; }

        public override void Update()
        {
            var graphics = Engine.GetGraphics();

            graphics.DrawImage(this.Sprite, this.Transform.Position.X, this.Transform.Position.Y,
                this.Transform.Scale.X, this.Transform.Scale.Y);
        }

        public override bool Init()
        {
            if (this.Sprite != null)
                return true;

            if (!this.LoadSprite(this.Path))
            {
                Log.Error($"[{nameof(SpriteComponent)}]({this.Tag}) - Sprite not initialized correctly");
                return false;
            }

            return true;
        }

        public override void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.Sprite.Dispose();
                }

                this.Sprite = null;

                this.disposedValue = true;
            }
        }

        private bool LoadSprite(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                Log.Error($"[{nameof(SpriteComponent)}]({this.Tag}) - No path specified");
                return false;
            }

            string filePath = $"{Engine.root}/{path}.png";
            if (!File.Exists(filePath))
            {
                Log.Error($"[{nameof(SpriteComponent)}]({this.Tag}) - File not found");
                return false;
            }

            this.Sprite = Image.FromFile(filePath);
            Log.Info($"[{nameof(SpriteComponent)}]({this.Tag}) - Sprite successfully loaded");
            return true;
        }
    }
}