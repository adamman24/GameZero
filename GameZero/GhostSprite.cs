using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using GameZero.Collisions;

namespace GameZero
{
    class GhostSprite
    {
        private KeyboardState keyboardState;

        private GamePadState gamePadState;

        private Texture2D texture;

        /// <summary>
        /// possition of bat
        /// </summary>
        public Vector2 position = new Vector2(20,200);

        private bool flipped;

        private BoundingCircle bounds = new BoundingCircle(new Vector2(20, 200), 16);

        /// <summary>
        /// ability to disable keybaord
        /// </summary>
        public bool disable = false;

        /// <summary>
        /// bounding volume of the sprite
        /// </summary>
        public BoundingCircle Bounds => bounds;

        /// <summary>
        /// color blend of ghost
        /// </summary>
        public Color Color { get; set; } = Color.Black;

        /// <summary>
        /// loads sprite texture from conetent manager
        /// </summary>
        /// <param name="content">manager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("slime");
        }

        /// <summary>
        /// update sprite position from user input
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();
            int sprint = 1;

            if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift)) sprint = 2;
            else sprint = 1;

            if (!disable)
            {
                // Apply the gamepad movement with inverted Y axis
                position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
                if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
                if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

                // Apply keyboard movement
                if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1) * sprint;
                if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1) * sprint;
                if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
                {
                    position += new Vector2(-1, 0) * sprint;
                    flipped = true;
                }
                if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
                {
                    position += new Vector2(1, 0) * sprint;
                    flipped = false;
                }
            }else
            { }
            //update the bounds
            bounds.Center = position;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, position, null, Color, 0, new Vector2(64, 64), 0.25f, spriteEffects, 0);
        }
    }
}
