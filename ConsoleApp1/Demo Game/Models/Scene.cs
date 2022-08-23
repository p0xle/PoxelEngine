using EngineTest.Demo_Game.Components;
using PoxelEngine.Models;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace EngineTest.Demo_Game.Models
{
    // Todo: Make Scene a abstract Engine Class
    public class Scene : GameObject
    {
        public Scene(SceneSettings settings, string tag) : this(settings, tag, new Transform()) { }

        public Scene(SceneSettings settings, string tag, Transform transform) : base(tag, transform)
        {
            this.Settings = settings;
        }

        private readonly SceneSettings Settings;

        public Player Player { get; set; }

        // Todo: Implement IDisposable to Dispose of References for Sprites
        private UIObject groundRef;
        private UIObject coinRef;

        private List<GameObject> objects = new List<GameObject>();

        public void Load()
        {
            this.Player = Player.Create(new Transform(Vector2.One, Vector2.One, 2), tag: "Player", path: "player", playerSpeed: 5f);

            var coinComponent = this.Player?.GetComponent<CoinComponent>();
            if (coinComponent is null)
                Log.Error($"[{nameof(Scene)}]({nameof(Load)}) - Error loading the Player Coin Component");

            this.groundRef = UIObject.CreateReference("ground");
            this.coinRef = UIObject.CreateReference("coin");

            int groundCount = 0;

            for (int i = 0; i < this.Settings.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Settings.Map.GetLength(1); j++)
                {
                    if (this.Settings.Map[j, i] == "g")
                    {
                        this.objects.Add(UIObject.Create(tag: "Ground", name: $"Ground_{groundCount}", transform: this.GetMapTransform(i, j, layer: 0), reference: this.groundRef));
                        groundCount++;
                    }

                    if (this.Settings.Map[j, i] == "c")
                    {
                        this.objects.Add(UIObject.Create(tag: "Coin", name: $"Coin_{coinComponent.Coins}", transform: this.GetMapTransform(i, j, layer: 1), reference: this.coinRef));
                        coinComponent.Coins++;
                    }

                    if (this.Settings.Map[j, i] == "t")
                    {
                        this.objects.Add(Text.Create(content: "Test", transform: this.GetMapTransform(i, j, 100, 100, layer: 10), "TestText",
                            new FontOptions(family: new FontFamily("Comic Sans MS"), style: FontStyle.Underline)));
                    }

                    if (this.Settings.Map[j, i] == "p")
                    {
                        this.Player.Transform = this.GetMapTransform(i, j, 48, 48, 2);
                    }

                    // Todo: Button loading
                    // Todo: Customizable Map Definitions (Dictionary with Key and delegate to call if key is specified in map)
                }
            }
        }

        private Transform GetMapTransform(int i, int j, float? sizeX = null, float? sizeY = null, int layer = 1)
        {
            return new Transform(
                new Vector2(i * this.Settings.groundSize, j * this.Settings.groundSize),
                new Vector2(sizeX ?? this.Settings.groundSize, sizeY ?? this.Settings.groundSize), layer);
        }

        protected override void DisposeAC()
        {
            this.Player.DestroySelf();
            this.groundRef.DestroySelf();
            this.coinRef.DestroySelf();
        }
    }

    public struct SceneSettings
    {
        public int groundSize;
        public string[,] Map;

        public SceneSettings(string[,] map, int groundSize = 64)
        {
            this.groundSize = groundSize;
            this.Map = map;
        }
    }
}
