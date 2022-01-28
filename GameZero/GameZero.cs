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

            bats = new BatSprite[]
            {
                new BatSprite(){ Position = new Vector2(100,100), Direction = Direction.Down },
                new BatSprite(){ Position = new Vector2(400,400), Direction = Direction.Up },
                new BatSprite(){ Position = new Vector2(200,300), Direction = Direction.Left }
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

            // TODO: Add your update logic here
            ghost.Update(gameTime);
            ghost.Color = Color.Black;
            finishLine.Update(gameTime);
            foreach (var bat in bats) bat.Update(gameTime);

            if(finishLine.Bounds.CollidesWith(ghost.Bounds))
            {
                ghost.Color = Color.White;

                spriteBatch.Begin();
                spriteBatch.DrawString(spriteFont, "You Win!", new Vector2(350, 250), Color.Gold);
                spriteBatch.End();
                base.Draw(gameTime);
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
            foreach (var bat in bats) bat.Draw(gameTime, spriteBatch);
            ghost.Draw(gameTime, spriteBatch);            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
