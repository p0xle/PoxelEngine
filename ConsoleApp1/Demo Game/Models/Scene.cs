using EngineTest.Demo_Game.Components;
using PoxelEngine.Models;
using PoxelEngine.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EngineTest.Demo_Game.Models
{
    public class Scene : GameObject
    {
        public Scene(SceneSettings settings, string tag) : this(settings, tag, new Transform()) { }

        public Scene(SceneSettings settings, string tag, Transform transform) : base(tag, transform)
        {
            this.Settings = settings;
        }

        private readonly SceneSettings Settings;

        public Player Player { get; set; }

        private UIObject groundRef;

        public void Load()
        {
            this.Player = Player.Create(new Transform(Vector2.One, Vector2.One, 2), tag: "Player", path: "player", playerSpeed: 5f);

            var coinComponent = this.Player?.GetComponent<CoinComponent>();
            if (coinComponent is null)
                Log.Error($"[{nameof(Scene)}]({nameof(Load)}) - Error loading the Player Coin Component");

            this.groundRef = UIObject.CreateReference("ground");

            int groundCount = 0;

            for (int i = 0; i < this.Settings.Map.GetLength(0); i++)
            {
                for (int j = 0; j < this.Settings.Map.GetLength(1); j++)
                {
                    if (this.Settings.Map[j, i] == "g")
                    {
                        UIObject.Create(tag: "Ground", name: $"Ground_{groundCount}", transform: this.GetMapTransform(i, j, layer: 0), reference: this.groundRef);
                        groundCount++;
                    }

                    if (this.Settings.Map[j, i] == "c")
                    {
                        UIObject.Create(tag: "Coin", name: $"Coin_{coinComponent.Coins}", transform: this.GetMapTransform(i, j, layer: 1), "coin");
                        coinComponent.Coins++;
                    }

                    if (this.Settings.Map[j, i] == "t")
                    {
                        Text.Create(content: "Test", transform: this.GetMapTransform(i, j, 100, 100, layer: 10), "TestText",
                            new FontOptions(family: new FontFamily("Comic Sans MS"), style: FontStyle.Underline));
                    }

                    if (this.Settings.Map[j, i] == "p")
                    {
                        this.Player.Transform = this.GetMapTransform(i, j, 48, 48, 2);
                    }
                }
            }
        }

        private Transform GetMapTransform(int i, int j, float? sizeX = null, float? sizeY = null, int layer = 1)
        {
            return new Transform(
                new Vector2(i * this.Settings.groundSize, j * this.Settings.groundSize),
                new Vector2(sizeX ?? this.Settings.groundSize, sizeY ?? this.Settings.groundSize), layer);
        }
    }

    public class SceneSettings
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
