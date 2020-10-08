﻿using System;
using System.Drawing;
using OpenTK;

using Jack;
using Jack.Graphics;
using Jack.Core;

namespace CS_Jack
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var app = new Application())
            {
                app.Run();
            }
        }

        public class Application : JackApp
        {
            public Application() : base()
            {
                WindowTitle = "Jack Sandbox";
                WindowSize = new Size(1000, 700);
                WindowVsync = VSyncMode.On;
            }

            private Camera _uiCamera;

            SpriteFont _font;

            Scene _testScene;

            protected override void Load()
            {
                _uiCamera = new Camera(this, WindowSize.Width, WindowSize.Height);

                _font = new SpriteFont("Menlo", 37);

                _testScene = new TestScene(this);
                CurrentScene = _testScene;

                DebugLayer.Init(this);
            }

            private float _deltaTime = 0;
            protected override void Update(float deltaTime)
            {
                _deltaTime = deltaTime;
                DebugLayer.Update(deltaTime);
            }

            protected override void Draw()
            {
                DebugLayer.Draw(SpriteBatch);
            }

            protected override void Exit()
            {
            }
        }
    }
}
