using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameZero
{
    public class GameZero : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private GhostSprite ghost;
        private BatSprite[] bats;
        private FinishLine finishLine;
        private SpriteFont spriteFont;
        private bool win = false;
        private bool lose = false;
        private int numBats = 1;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        /// <summary>
        /// game object
        /// </summary>
        public GameZero()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// initializes game
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ghost = new GhostSprite();
            finishLine = new FinishLine();
            System.Random rand = new System.Random();

            //creates 10 bats 
            bats = new BatSprite[]
            {
                new BatSprite(new Vector2(400,275)) { Direction = Direction.Up },
                new BatSprite(new Vector2(450,300)) { Direction = Direction.Up, Level = 2 },
                new BatSprite(new Vector2(550,325)) { Direction = Direction.Up, Level = 3 },
                new BatSprite(new Vector2(600,400)) { Direction = Direction.Up, Level = 4 },
                new BatSprite(new Vector2(650,500)) { Direction = Direction.Up, Level = 5 },
                new BatSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)) { Direction = Direction.Up, Level = 2 },
                new BatSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)) { Direction = Direction.Up, Level = 2 },
                new BatSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)) { Direction = Direction.Up, Level = 2 },
                new BatSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)) { Direction = Direction.Up, Level = 2 },
                new BatSprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)) { Direction = Direction.Up, Level = 2 },
            };

            base.Initialize();
        }

        /// <summary>
        /// load game content
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ghost.LoadContent(Content);
            finishLine.LoadContent(Content);
            foreach (var bat in bats) bat.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("arial");
            
        }

        /// <summary>
        /// updates game world
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();

            //if space pressed add a bat
            if (currentKeyboardState.IsKeyDown(Keys.Space) &&
                previousKeyboardState.IsKeyUp(Keys.Space))
            {
                numBats++;
                if(numBats > 10)
                {
                    numBats = 10;
                }
                ghost.position = new Vector2(20, 200);
            }

            for(int x = 0; x <= numBats -1; x++)
            {
                bats[x].show = true;
            }

            // TODO: Add your update logic here
            ghost.Update(gameTime);
            ghost.Color = Color.Black;
            finishLine.Update(gameTime);

            //checking for bat collisions
            foreach (var bat in bats)
            {
                bat.Update(gameTime);
                if(bat.show && bat.Bounds.CollidesWith(ghost.Bounds))
                {
                    ghost.Color = Color.Red;
                    lose = true;
                    bat.Collision = true;
                    ghost.disable = true;
                }
            }

            //checking for finish line collisions
            if(finishLine.Bounds.CollidesWith(ghost.Bounds))
            {
                ghost.Color = Color.White;
                win = true;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// draws game world
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            finishLine.Draw(gameTime, spriteBatch);
            //if bats are in use it draws them
            foreach (var bat in bats)
            {
                if(bat.show)
                {
                    bat.Draw(gameTime, spriteBatch);
                }
            }
            ghost.Draw(gameTime, spriteBatch);
            //check for win or lose and respond with apropriate message
            if(win)
            {
                spriteBatch.DrawString(spriteFont, "You Win!", new Vector2(350, 200), Color.Green);
                foreach (var bat in bats)
                {
                    bat.Collision = true;
                }
                ghost.disable = true;
            }
            if(lose)
            {
                spriteBatch.DrawString(spriteFont, "You Lose...", new Vector2(350, 200), Color.Red);
            }

            //show number of bats and how to exit
            spriteBatch.DrawString(spriteFont, $"Bats: {numBats}", new Vector2(5, 5), Color.Black);
            spriteBatch.DrawString(spriteFont, "Press space for more bats :)", new Vector2(5, 445), Color.Black, 0, new Vector2(0, 0), .5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(spriteFont, "Press escape to end game", new Vector2(5, 460), Color.Black, 0, new Vector2(0,0), .5f, SpriteEffects.None, 0);
            
            GraphicsDevice.Clear(Color.BlanchedAlmond);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}