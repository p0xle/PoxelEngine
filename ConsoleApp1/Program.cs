using EngineTest.Demo_Game;
using PoxelEngine;
using PoxelEngine.Utility;
using System;

namespace EngineTest
{
    internal class Program
    {
        static Engine game;
        static bool gameStopped = false;

        static void Main(string[] args)
        {
            WriteIntro();

            while (true)
            {
                if (gameStopped)
                {
                    WriteIntro();
                    gameStopped = false;
                }

                var input = Console.ReadLine();

                if (input != null && input.Length > 0)
                {
                    if (input == "stop" || input == "close" || input == "x")
                        break;

                    switch (input)
                    {
                        case "1":
                            StartDemoGame();
                            break;
                        case "2":
                            StartPongGame();
                            break;
                        default:
                            Log.Write("Falsche Eingabe! Bitte versuchen Sie es erneut");
                            break;
                    }
                }
            }

            if (game != null)
                game.Dispose();
        }

        static void WriteIntro()
        {
            Log.Reset();

            Log.Write("Willkommen zu diesem Engine Test!");
            Log.Write("");
            Log.Write("Bitte wählen Sie aus welches Spiel Sie spielen möchten");

            Log.Write("");
            Log.Write("");

            Log.Write("Zur Auswahl stehen: (1) Demo und (2) Pong");
            Log.Write("Bitte geben Sie die entsprechende Zahl ein:");
        }

        static void StartDemoGame()
        {
            Log.Reset();
            game = new DemoGame();
            gameStopped = true;
        }

        static void StartPongGame()
        {
            Log.Reset();
            //game = new PongGame();
            gameStopped = true;
        }
    }
}
