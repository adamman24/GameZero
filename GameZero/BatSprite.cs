using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameZero.Collisions;

namespace GameZero
{
    /// <summary>
    /// bat sprite direction enum
    /// </summary>
    public enum Direction
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3
    }

    class BatSprite
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame = 1;
        private BoundingRectangle bounds;

        public int Level = 1;

        /// <summary>
        /// value to determine if bat appears or not
        /// </summary>
        public bool show = false;

        /// <summary>
        /// if bat is being touched 
        /// </summary>
        public bool Collision = false;

        /// <summary>
        /// direction of bat
        /// </summary>
        public Direction Direction;

        /// <summary>
        /// position of bat
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// bounding volume of sprite
        /// </summary>
        public BoundingRectangle Bounds => bounds;

        public BatSprite(Vector2 position)
        {
            this.Position = position;
            this.bounds = new BoundingRectangle(position + new Vector2(4, 4), 25, 16);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("32x32-bat-sprite");
        }

        public void Update(GameTime gameTime)
        {
            //update direction timer 
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Collision)
            {
                Position = Position;
            }
            else
            {
                //switch directions every 2 seconds
                if (directionTimer > 1.0 * Level)
                {
                    switch (Direction)
                    {
                        case Direction.Up:
                            Direction = Direction.Left;
                            this.bounds = new BoundingRectangle(Position + new Vector2(4, 4), 25, 16);
                            break;
                        case Direction.Down:
                            Direction = Direction.Right;
                            this.bounds = new BoundingRectangle(Position + new Vector2(4, 4), 10, 15);
                            break;
                        case Direction.Right:
                            Direction = Direction.Up;
                            this.bounds = new BoundingRectangle(Position + new Vector2(4, 4), 10, 15);
                            break;
                        case Direction.Left:
                            Direction = Direction.Down;
                            this.bounds = new BoundingRectangle(Position + new Vector2(4, 4), 25, 16);
                            break;
                    }
                    directionTimer -= 1.0 * Level;
                }

                //move bat in the direction its flying
                switch (Direction)
                {
                    case Direction.Up:
                        Position += new Vector2(0, -1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Down:
                        Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Left:
                        Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Direction.Right:
                        Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                }
            }
            //updating bounds
            bounds.X = Position.X;
            bounds.Y = Position.Y;
        }

        /// <summary>
        /// draws the animated sprite
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="spriteBatch">the sprite</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //update animation timer
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //update animation frame
            if (animationTimer > 0.1)
            {
                animationFrame++;
                if (animationFrame > 3)
                {
                    animationFrame = 1;
                }
                animationTimer -= 0.1;
            }

            //draw sprite
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
