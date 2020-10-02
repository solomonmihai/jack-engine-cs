﻿using System;
using System.Drawing;
using Jack;
using Jack.Graphics;
using OpenTK;
using OpenTK.Input;

namespace CS_Jack
{
    class Program
    {
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
                ClearColor = Color.Black;
                WindowVsync = VSyncMode.Off;
            }

            private SpriteBatch _spriteBatch;
            private Camera _camera;

            Texture _texture;
            Texture _texture2;

            protected override void Load()
            {
                _spriteBatch = new SpriteBatch(this);
                _camera = new Camera(this, WindowSize.Width, WindowSize.Height);
                _texture = new Texture("res/avatar.png");
                _texture2 = new Texture("res/glazing_1.png");
            }
            
            private void MoveCamera()
            {
                KeyboardState state = Keyboard.GetState();
                if (state.IsKeyDown(Key.W))
                {
                    _camera.Position += new Vector2(0, -0.01f);
                }
                if (state.IsKeyDown(Key.S))
                {
                    _camera.Position += new Vector2(0, 0.01f);
                }
                if (state.IsKeyDown(Key.A))
                {
                    _camera.Position += new Vector2(-0.01f, 0);
                }
                if (state.IsKeyDown(Key.D))
                {
                    _camera.Position += new Vector2(0.01f, 0);
                }

                if (state.IsKeyDown(Key.Q))
                {
                    _camera.Scale *= 1.01f;
                }
                else if (state.IsKeyDown(Key.E))
                {
                    _camera.Scale *= 0.99f;
                }
            }

            protected override void Update(float deltaTime)
            {
                WindowTitle = "FPS: " + Math.Ceiling(1 / deltaTime);
            }

            protected override void Draw()
            {
                MoveCamera();

                _spriteBatch.Begin(_camera);

                _spriteBatch.TextBatchTest();
                
                _spriteBatch.DrawQuad(new Vector2(-200, 0), new Vector2(30, 30), MathF.PI / 4, Color.Red);
                // _spriteBatch.DrawQuad(new Vector2(-200, -200), new Vector2(50, 50), MathF.PI / 7, _texture, Color.White);
                Rectangle srcRect = new Rectangle(0, 0, _texture.Size.Width / 2, _texture.Size.Height / 2);
                _spriteBatch.DrawQuad(new Vector2(-200, -200), new Vector2(50, 50), srcRect, MathF.PI / 7, _texture, Color.White);

                // int squareWidth = 5000;

                // int quadCount = 0;
                // for (int i = -squareWidth / 2; i < squareWidth / 2; i += 30)
                // {
                    // for (int j = -squareWidth / 2; j < squareWidth / 2; j += 30)
                    // {
                        // _spriteBatch.DrawQuad(new Vector2(i, j), new Vector2(30, 30), 0, _texture, Color.White);
                        // quadCount++;
                    // }
                // }

                _spriteBatch.End();
            }

            protected override void Exit()
            {
            }
        }
    }
}
