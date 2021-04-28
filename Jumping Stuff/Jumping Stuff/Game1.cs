using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Jumping_Stuff
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite Character;
        List<Obstacle> Obstacles = new List<Obstacle>();

        SpriteFont font; 

        Line TestLine;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true; 
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            Texture2D basketBall = Content.Load<Texture2D>("ballball");
            Texture2D Platform = Content.Load<Texture2D>("Platform");
            Vector2 scale = new Vector2(0.3f);
            Vector2 rectangleTall = new Vector2(.5f, 2f);
            Character = new Sprite(basketBall, Color.White, new Vector2(300, 0), scale);
            Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new Color[] { Color.White });
            Obstacles.Add(new Obstacle(Platform,Color.White,new Vector2(100,385), new Vector2(.5f,2.2f),pixel));
            Obstacles.Add(new Obstacle(Platform, Color.White, new Vector2(300, 265), new Vector2(.5f, 2.2f),pixel));
            Obstacles.Add(new Obstacle(Platform, Color.White, new Vector2(505, 155), new Vector2(.5f, 2.2f), pixel));
            TestLine = new Line(new Vector2(0, 200), new Vector2(100, 100), pixel);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        int currentDirection = 0;
        float previousY = 0;
        float initialForce = 20;
        float gravity = .5f;
        float force = -20;
        int xSpeed = 5;
        bool onObstacle = false;
        bool isJumping = false;
        bool collidingLeft;
        bool collidingRight;
        bool gravityApplied = false;
        bool overObstacle = false;


        KeyboardState lastKs;
        string whichObstacle = "";
        protected override void Update(GameTime gameTime)
        {

            Window.Title = $"{gravity}: Colliding. Force: {force}          Y: {Character.Y}                            {collidingLeft}";

            if (Character.Hitbox.Bottom >= GraphicsDevice.Viewport.Height)
            {
                gravityApplied = false;
                isJumping = false;
            }
            KeyboardState ks = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (ks.IsKeyDown(Keys.D) && Character.X < GraphicsDevice.Viewport.Width - Character.Width)
            {
                Character.X += xSpeed;

            }
            if (ks.IsKeyDown(Keys.A) && Character.X > 0)
            {
                Character.X -= xSpeed;
            }

            if (ks.IsKeyDown(Keys.Space) && !lastKs.IsKeyDown(Keys.Space) && !isJumping  && collidingLeft == false && collidingRight == false)
            {
                isJumping = true;
                force = initialForce;
            }

            if (isJumping)
            {
                Character.Y -= force;
                force -= gravity;
                //if(Math.Abs(force) >= gravity)
                //{
                //    gravityApplied = true;
                //    isJumping = false;
                //}


            }

            if (Character.Hitbox.Bottom > GraphicsDevice.Viewport.Height)
            {

                isJumping = false;
                gravityApplied = false;
                Character.Y = Math.Min(Character.Y, GraphicsDevice.Viewport.Height - Character.Hitbox.Height);
            }
            if (gravityApplied && Character.Hitbox.Bottom < GraphicsDevice.Viewport.Height)
            {
                currentDirection = 1;
                Window.Title = $"{gravity}: Colliding. Force: {force}          Y: {Character.Y}                            {collidingLeft}";
                Character.Y += gravity * 15;
            }

            if(previousY < Character.Y)
            {
                currentDirection = -1;
            }
            //turning point ^
            for (int i = 0; i < Obstacles.Count; i++)
            {
                
                if (Character.Hitbox.Intersects(Obstacles[i].Hitbox))
                {
                    
                    //TODO: fix character colliding with multiple obstacles 
                    
                    if (Character.Hitbox.Left < Obstacles[i].Right.StartPoint.X && Character.Hitbox.Top > Obstacles[i].Top.StartPoint.Y && Character.Hitbox.Right > Obstacles[i].Right.StartPoint.X)
                    {
                        
                        collidingRight = true;
                        Character.X = Obstacles[i].Right.StartPoint.X;
                    }
                    else if (Character.Hitbox.Right > Obstacles[i].Left.StartPoint.X && Character.Hitbox.Top > Obstacles[i].Top.StartPoint.Y && Character.Hitbox.Left < Obstacles[i].Left.StartPoint.X)
                    {
                        collidingLeft = true;
                        Character.X = Obstacles[i].Left.StartPoint.X - Character.Width;
                    }
                    
                    else if (Character.Hitbox.Bottom > Obstacles[i].Top.StartPoint.Y && currentDirection < 0)
                    {
                        gravityApplied = false;
                        Character.Y = Obstacles[i].Top.StartPoint.Y - Character.Width;
                    }
                    else if (Character.Hitbox.Top < Obstacles[i].Bottom.StartPoint.Y && currentDirection > 1)
                    {
                        Character.Y = Obstacles[i].Bottom.StartPoint.Y;
                    }
 
                    isJumping = false;
                }
                else
                {
                    collidingRight = false;
                    collidingLeft = false;
                    gravityApplied = true;
                }

            }


            previousY = Character.Y;
            whichObstacle = $"{currentDirection}";

            lastKs = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, whichObstacle, Vector2.One, Color.Red);
            for (int i = 0; i < Obstacles.Count; i++)
            {
                Obstacles[i].Draw(spriteBatch);
              //  Obstacles[i].Draw(spriteBatch);
            }
            Character.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
