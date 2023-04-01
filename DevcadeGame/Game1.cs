﻿using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DevcadeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D metalBar;
        private Tuple<int, int> Coordinates; // width, height
        private Dictionary<int, int[]> metalBarPosition = new Dictionary<int, int[]>();

        /// <summary>
        /// Game constructor
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        /// <summary>
        /// Does any setup prior to the first frame that doesn't need loaded content.
        /// </summary>
        protected override void Initialize()
        {
            Input.Initialize(); // Sets up the input library

            // Set window size if running debug (in release it will be fullscreen)
            #region
#if DEBUG
            // Actual size, change to this when submit
            // width = 420
            // height = 980
            Coordinates = Tuple.Create(420, 980);
            _graphics.PreferredBackBufferWidth = Coordinates.Item1;
            _graphics.PreferredBackBufferHeight = Coordinates.Item2;
            //_graphics.ApplyChanges();

            // Connor's massive monitor size requires this
            // Coordinates = Tuple.Create(750, 1750);
            _graphics.PreferredBackBufferWidth = Coordinates.Item1;
            _graphics.PreferredBackBufferHeight = Coordinates.Item2;
            _graphics.ApplyChanges();
#else
			_graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
			_graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
			_graphics.ApplyChanges();
#endif
            #endregion

            metalBarPosition[0] = new int[] { 0, Coordinates.Item2 - 30 };
            metalBarPosition[1] = new int[] { Coordinates.Item1, Coordinates.Item2 - 30};

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// Does any setup prior to the first frame that needs loaded content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            metalBar = Content.Load<Texture2D>("download");



            // TODO: use this.Content to load your game content here
            // ex.
            // texture = Content.Load<Texture2D>("fileNameWithoutExtention");
        }

        /// <summary>
        /// Your main update loop. This runs once every frame, over and over.
        /// </summary>
        /// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update(); // Updates the state of the input library

            // Exit when both menu buttons are pressed (or escape for keyboard debuging)
            // You can change this but it is suggested to keep the keybind of both menu
            // buttons at once for gracefull exit.
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                (Input.GetButton(1, Input.ArcadeButtons.Menu) &&
                Input.GetButton(2, Input.ArcadeButtons.Menu)))
            {
                Exit();
            }

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {                
                Debug.WriteLine("Up (Left)");
                metalBarPosition[0] = new int[] { 0, metalBarPosition[0][1] + 10 };
                if (metalBarPosition[0][1] + 30 > Coordinates.Item2)
                {
                    metalBarPosition[0] = new int[] { 0, Coordinates.Item2 - 30};
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Debug.WriteLine("Down (Left)");
                metalBarPosition[0] = new int[] { 0, metalBarPosition[0][1] - 10 };
                if (metalBarPosition[0][1] < 0)
                {
                    metalBarPosition[0] = new int[] { 0, 0 };
                }

            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Debug.WriteLine("Up (Right)");
                metalBarPosition[1] = new int[] { Coordinates.Item1, metalBarPosition[1][1] + 10 };
                if (metalBarPosition[1][1] + 30 > Coordinates.Item2)
                {
                    metalBarPosition[1] = new int[] { Coordinates.Item1, Coordinates.Item2 - 30};
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Debug.WriteLine("Down (Right)");
                metalBarPosition[1] = new int[] { Coordinates.Item1, metalBarPosition[1][1] - 10 };
                if (metalBarPosition[1][1] < 0)
                {
                    metalBarPosition[1] = new int[] { Coordinates.Item1, 0 };
                }

            }

            // Debug.WriteLine($"H1: {Heights[0]} | H2: {Heights[1]}");

            base.Update(gameTime);
        }

        /// <summary>
        /// Your main draw loop. This runs once every frame, over and over.
        /// </summary>
        /// <param name="gameTime">This is the gameTime object you can use to get the time since last frame.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(227, 208, 36));

            _spriteBatch.Begin();
            // TODO: Add your drawing code here
            Debug.WriteLine("RightBar: " + metalBarPosition[1][0] + " " + metalBarPosition[1][1]);
            Debug.WriteLine("LeftBar: " + metalBarPosition[0][0] + " " + metalBarPosition[0][1]);
            int x1 = metalBarPosition[0][0];
            int y1 = metalBarPosition[0][1];
            int x2 = metalBarPosition[1][0];
            int y2 = metalBarPosition[1][1];
            int y = y2 - y1;
            int x = x2 - x1;
            Debug.WriteLine(x + " " + y);
            double rotation = Math.Atan2(y, x);
            Debug.WriteLine((float)rotation);
            int[] midpoint = new int[] {(x1 + x2) / 2, (y1 + y2) / 2};
            _spriteBatch.Draw(metalBar, new Rectangle(0, metalBarPosition[0][1], Coordinates.Item1 + 800, 30), null, new Color(255, 255, 255), (float)rotation, new Vector2(0, 0), SpriteEffects.None, 0);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}