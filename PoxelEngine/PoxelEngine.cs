using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Windows.Forms;
using PoxelEngine.Models;
using PoxelEngine.Utility;

namespace PoxelEngine
{
    public abstract class Engine : IDisposable
    {
        public Engine(Size screenSize, string title, bool isDebug = false)
        {
            Log.Info("[ENGINE] - Engine is starting...");

            this.ScreenSize = screenSize;
            this.Title = title;

            this.IsDebug = isDebug;

            this.Window = new Canvas
            {
                Size = this.ScreenSize,
                Text = this.Title,
                FormBorderStyle = FormBorderStyle.FixedToolWindow,
            };

            this.Window.Paint += this.Renderer;
            this.Window.KeyDown += this.WindowKeyDown;
            this.Window.KeyUp += this.WindowKeyUp;
            this.Window.MouseWheel += this.WindowMouseWheel;
            this.Window.FormClosing += this.GameClosing;

            this.GameThread = new Thread(this.GameLoop);
            this.GameThread.Start();

            Application.Run(this.Window);
        }

        public const string root = "Assets/Sprites";

        public Color BackgroundColor = Color.Black;

        public Vector2 CameraZoom = Vector2.One;
        public Vector2 CameraPosition = Vector2.Zero;
        public float CameraAngle = 0f;

        private static List<GameObject> GameObjects = new List<GameObject>();
        private static List<int> LayerList = new List<int>();

        private readonly Size ScreenSize = new Size(600, 600);
        private readonly string Title = "Game";
        private readonly Canvas Window = null;
        private readonly Thread GameThread = null;

        private readonly bool IsDebug;

        private DateTime lastFrameCheck = DateTime.Now;
        private long frames = 0;
        private System.Timers.Timer timer;

        private static Graphics graphics;

        public static void RegisterGameObject(GameObject gameObject)
        {
            GameObjects.Add(gameObject);
            Log.Info($"[ENGINE] - ({gameObject.Tag}) has been registered!");

            if (!LayerList.Contains(gameObject.Transform.Layer))
            {
                LayerList.Add(gameObject.Transform.Layer);
                LayerList = LayerList.OrderBy(o => o).ToList();
                Log.Info($"[ENGINE]({gameObject.Tag}) - Layer {gameObject.Transform.Layer} has been registered!");
            }
        }

        public static void UnregisterGameObject(GameObject gameObject)
        {
            GameObjects.Remove(gameObject);
            Log.Info($"[ENGINE] - ({gameObject.Tag}) has been destroyed!");

            if (GameObjects.FirstOrDefault(w => w.Transform.Layer == gameObject.Transform.Layer) is null)
            {
                LayerList.Remove(gameObject.Transform.Layer);
                Log.Info($"[ENGINE]({gameObject.Tag}) - Layer {gameObject.Transform.Layer} has been removed!");
            }
        }

        public static GameObject GetGameObject(string tag)
        {
            return GameObjects.FirstOrDefault(w => w.Tag == tag);
        }

        public static List<GameObject> GetGameObjects(string tag)
        {
            return GameObjects.Where(w => w.Tag == tag).ToList();
        }

        public static Graphics GetGraphics()
        {
            return graphics;
        }

        public double GetFps()
        {
            var secondsElapsed = (DateTime.Now - this.lastFrameCheck).TotalSeconds;
            var count = Interlocked.Exchange(ref this.frames, 0);
            this.lastFrameCheck = DateTime.Now;

            return count / secondsElapsed;
        }

        private void GameLoop()
        {
            this.OnLoad();
            this.SetTimer();

            while (this.GameThread.IsAlive)
            {
                try
                {
                    this.OnDraw();
                    this.Window.BeginInvoke((MethodInvoker)delegate { this.Window.Refresh(); });
                    this.OnUpdate();
                    Thread.Sleep(1);

                    Interlocked.Increment(ref this.frames);
                }
                catch
                {
                    Log.Error("GameThread has not been found...");
                }
            }
        }

        private void Renderer(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            graphics.Clear(this.BackgroundColor);

            graphics.TranslateTransform(this.CameraPosition.X, this.CameraPosition.Y);
            graphics.RotateTransform(this.CameraAngle);
            graphics.ScaleTransform(this.CameraZoom.X, this.CameraZoom.Y);

            // If Collections are changed while they are rendered it throws an Error
            try
            {
                // Don't use order by in here because it create a lot of performance issues
                foreach (var layer in LayerList)
                {
                    // Todo: Where could result in performance issues here, maybe order it earlier so there is a list per layer
                    foreach (var gameObject in GameObjects.Where(w => w.Transform.Layer == layer))
                    {
                        gameObject?.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                if (this.IsDebug) Log.Error("[ENGINE][Renderer] - Exception: " + ex.Message);
            }
        }

        private void WindowKeyUp(object sender, KeyEventArgs e) => this.GetKeyUp(e);
        private void WindowKeyDown(object sender, KeyEventArgs e) => this.GetKeyDown(e);
        private void WindowMouseWheel(object sender, MouseEventArgs e) => this.MouseWheelMoving(e);

        private void SetTimer()
        {
            this.frames = 0;

            this.timer = new System.Timers.Timer(1000);
            this.timer.Elapsed += this.OnTimedEvent;
            this.timer.Enabled = true;

            this.lastFrameCheck = DateTime.Now;
        }

        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.IsDebug)
            {
                Log.Info($"[ENGINE] - {(int)this.GetFps()} frames per second");
            }
        }

        private void GameClosing(object sender, FormClosingEventArgs e)
        {
            Log.Write($"[ENGINE] - Game stopping");
            this.StopGame();
        }

        public void StopGame()
        {
            this.Dispose();
        }

        /// <summary>Handles everything that needs to be loaded before the Game starts</summary>
        public abstract void OnLoad();
        /// <summary>Handles everything like physics after the window is refreshed</summary>
        public abstract void OnUpdate();
        /// <summary>Handles everything that needs to be drawn before the window is refreshed</summary>
        public abstract void OnDraw();
        /// <summary>Handles the KeyDown Event</summary>
        public abstract void GetKeyDown(KeyEventArgs e);
        /// <summary>Handles the KeyUp Event</summary>
        public abstract void GetKeyUp(KeyEventArgs e);
        /// <summary>Handles the Event raised when the mouse wheel is moved</summary>
        public abstract void MouseWheelMoving(MouseEventArgs e);

        public void Dispose()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject?.Dispose();
            }

            GameObjects = new List<GameObject>();

            if (this.GameThread.IsAlive)
                this.GameThread.Abort();

            this.timer.Stop();
            this.timer.Dispose();

            this.Window.Dispose();

            try
            {
                graphics.Dispose();
            }
            catch { }
        }

        public class Canvas : Form
        {
            public Canvas()
            {
                this.DoubleBuffered = true;
            }
        }
    }
}
