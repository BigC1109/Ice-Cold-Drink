﻿using Devcade;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using tainicom.Aether.Physics2D.Dynamics;

namespace DevcadeGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Texture2D MetalBarTexture { get; set; }
        public static Tuple<int, int> Coordinates = Tuple.Create(420, 980); // width, height
        public MetalBar MetalBar { get; set; }
        public Ball Ball { get; set; }
        public World World { get; set; }
        public LinkedList<Hole> holes = new LinkedList<Hole>();
        public bool testingvalue = true;
        public Level Level { get; set; }

        /// <summary>
        /// Game constructor
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Does any setup prior to the first frame that doesn't need loaded content.
        /// </summary>
        protected override void Initialize()
        {
            Input.Initialize(); // Sets up the input library

            World = new World();
            World.Gravity = new Vector2(0, 400f);

            var top = 0;
            var bottom = Coordinates.Item2;
            var left = 0;
            var right = Coordinates.Item1;
            var edges = new Body[] {
                World.CreateEdge(new Vector2(left, top), new Vector2(right, top)),
                World.CreateEdge(new Vector2(left, top), new Vector2(left, bottom)),
                World.CreateEdge(new Vector2(left, bottom), new Vector2(right, bottom)),
                World.CreateEdge(new Vector2(right, top), new Vector2(right, bottom)),
            };

            foreach (var edge in edges)
            {
                edge.BodyType = BodyType.Static;
                edge.SetRestitution(1);
            }

            var radius = 15;
            var position = new Vector2(385, 925);
            var body = World.CreateCircle(radius, 1, position, BodyType.Dynamic);
            body.SetRestitution(0.8f);
            body.SetFriction(1);

            Ball = new Ball(radius, body, this);

            MetalBarTexture = new Texture2D(GraphicsDevice, 1, 1);
            MetalBarTexture.SetData(new Color[] { Color.Gray });

            // Set window size if running debug (in release it will be fullscreen)
            #region
#if DEBUG
            // Actual size, change to this when submit
            // width = 420
            // height = 980
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

            // TODO: Add your initialization logic here

            var metalBarBody = World.CreateRectangle(Coordinates.Item1 + 800, 30, 1, new Vector2(0, Coordinates.Item2 - 30), 0, BodyType.Static);
            MetalBar = new MetalBar(MetalBarTexture, metalBarBody, this);

            // Create all Holes statically
            holes.AddLast(new Hole(25, new Vector2(360, 900), Hole.HoleType.ENTER, 0, this));

            holes.AddLast(new Hole(25, new Vector2(0, 630), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(40, 695), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(105, 745), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(200, 690), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(285, 690), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(360, 635), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(355, 740), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(245, 745), Hole.HoleType.INCORRECT, 1, this)); // Level 1

            holes.AddLast(new Hole(25, new Vector2(150, 580), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(95, 490), Hole.HoleType.INCORRECT, 0, this)); 
            holes.AddLast(new Hole(25, new Vector2(95, 630), Hole.HoleType.INCORRECT, 2, this)); // Level 2

            holes.AddLast(new Hole(25, new Vector2(300, 585), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(235, 595), Hole.HoleType.INCORRECT, 3, this)); // Level 3

            holes.AddLast(new Hole(25, new Vector2(60, 415), Hole.HoleType.INCORRECT, 4, this)); // Level 4

            holes.AddLast(new Hole(25, new Vector2(125, 430), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(165, 475), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(210, 515), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(270, 505), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(180, 395), Hole.HoleType.INCORRECT, 5, this)); // Level 5

            holes.AddLast(new Hole(25, new Vector2(210, 335), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(255, 380), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(300, 420), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(350, 465), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(305, 255), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(350, 300), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(390, 345), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(305, 340), Hole.HoleType.INCORRECT, 6, this)); // Level 6

            holes.AddLast(new Hole(25, new Vector2(95, 215), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(45, 245), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(30, 300), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(55, 350), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(110, 375), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(155, 330), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(100, 290), Hole.HoleType.INCORRECT, 7, this)); // Level 7

            holes.AddLast(new Hole(25, new Vector2(195, 125), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(230, 80), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(265, 35), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(305, 80), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(340, 125), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(305, 170), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(265, 215), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(265, 125), Hole.HoleType.INCORRECT, 8, this)); // Level 8

            holes.AddLast(new Hole(25, new Vector2(60, 10), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(130, 10), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(160, 60), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(125, 110), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(30, 60), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(65, 110), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(95, 160), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(95, 60), Hole.HoleType.INCORRECT, 9, this)); // Level 9

            // Miscellaneous Holes
            holes.AddLast(new Hole(25, new Vector2(-5, 495), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(-5, 205), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(380, 200), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(380, 550), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(-5, 0), Hole.HoleType.INCORRECT, 0, this));
            holes.AddLast(new Hole(25, new Vector2(375, -5), Hole.HoleType.INCORRECT, 0, this));



            Level = new Level(holes, this);
            Level.nextLevel();
            //new Hole(25, new Vector2(40, 900), Hole.HoleType.INCORRECT, this)

            base.Initialize();



        }

        /// <summary>
        /// Does any setup prior to the first frame that needs loaded content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Ball.LoadContent(Content);

            // Load all holes
            foreach (Hole hole in holes)
            {
                hole.LoadContent(Content);
            }

            // BallTexture = Content.Load<Texture2D>("CircleSprite");

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
            MetalBar.Update(gameTime);
            Ball.Update(gameTime);
            Level.Update(gameTime);
            World.Step((float)gameTime.ElapsedGameTime.TotalSeconds);

            // Debug.WriteLine($"H1: {Heights[0]} | H2: {Heights[1]}");



            if (Keyboard.GetState().IsKeyDown(Keys.L) && testingvalue == true)
            {
                Hole newHole = new Hole(25, new Vector2(Mouse.GetState().Position.X - 25, Mouse.GetState().Position.Y - 25), Hole.HoleType.ENTER, 0, this);
                newHole.LoadContent(Content);
                Debug.WriteLine("THASTASHTASHTASHT");
                holes.AddLast(newHole);
                testingvalue = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
            {
                Debug.WriteLine("----------Start----------");
                foreach (Hole hole in holes)
                {
                    Debug.WriteLine($"holes.AddLast(new Hole(25, new Vector2({hole.Position.X}, {hole.Position.Y}), Hole.HoleType.INCORRECT, false, this));");
                }
                Debug.WriteLine("-----------End-----------");
            }

            if (Keyboard.GetState().IsKeyUp(Keys.L))
            {
                testingvalue = true;
            }

            foreach (Hole hole in holes)
            {
                hole.Update(gameTime);
            }

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

            foreach (Hole hole in holes)
            {
                hole.Draw(_spriteBatch);
            }

            Ball.Draw(_spriteBatch);
            MetalBar.Draw(_spriteBatch);

            //var test = new Texture2D(GraphicsDevice, 1, 1);
            //test.SetData(new Color[] { Color.Red });

            //_spriteBatch.Draw(test, new Rectangle((int)Ball.Body.Position.X, (int)Ball.Body.Position.Y, Ball.Radius, Ball.Radius), Color.Red);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}