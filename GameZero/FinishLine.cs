using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameZero.Collisions;

namespace GameZero
{
    /// <summary>
    /// class representing the finish line
    /// </summary>
    class FinishLine
    {
        private Texture2D texture;

        private Vector2 position = new Vector2(772, 58);

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(772, 0), 50, 500);

        /// <summary>
        /// bouding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        /// <summary>
        /// loads finish line
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("finishLine");
        }

        /// <summary>
        /// update finish line
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            //collision updates
        }

        /// <summary>
        /// draw finish line
        /// </summary>
        /// <param name="gametime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 1.57f, new Vector2(64, 64), 1.25f, SpriteEffects.None, 0);
        }
    }
}
