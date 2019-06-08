using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Camera
    {
        private Vector2 halfScreenSize;
        private Vector2 position;
        /// <summary>
        /// The matrix used for the camera
        /// </summary>
        public Matrix ViewMatrix { get; private set; }
        /// <summary>
        /// Property for the position
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
                UpdateViewMatrix(); // Updates the camera as you set the new position
            }
        }

        /// <summary>
        /// Camera constructor that sets the halfScreenSize to the center of the screen
        /// </summary>
        public Camera()
        {
            halfScreenSize = new Vector2(GameWorld.Instance.ScreenSize.Width / 2, GameWorld.Instance.ScreenSize.Height / 2);
            UpdateViewMatrix();
        }

        /// <summary>
        /// Method that updates the camera Matrix to the new position
        /// </summary>
        private void UpdateViewMatrix()
        {
            ViewMatrix = Matrix.CreateTranslation
                (MathHelper.Clamp //Clamps the X position of the viewMatrix translation
                (halfScreenSize.X - position.X, -GameWorld.Instance.TileMapBounds.Width + GameWorld.Instance.ScreenSize.Width, 0),
                MathHelper.Clamp //Clamps the Y position of the viewMatrix translation
                (halfScreenSize.Y - position.Y, -GameWorld.Instance.TileMapBounds.Height + GameWorld.Instance.ScreenSize.Height, 0),
                0);       
        }
    }
}
