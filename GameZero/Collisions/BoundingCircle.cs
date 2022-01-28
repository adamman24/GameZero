using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameZero.Collisions
{
    class BoundingCircle
    {
        /// <summary>
        /// center of boundingCircle
        /// </summary>
        public Vector2 Center;

        /// <summary>
        /// radius of bounding circle
        /// </summary>
        public float Radius;

        /// <summary>
        /// constructs a new bouncing circle
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public BoundingCircle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// checks for collision
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollidesWith(BoundingCircle other)
        {
            return CollisionHelper.Collides(this, other);
        }

        /// <summary>
        /// checks for collision
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisionHelper.Collides(this, other);
        }
    }
}
