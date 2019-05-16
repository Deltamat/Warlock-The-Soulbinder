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
        /// <summary>
        /// The matrix used for the camera
        /// </summary>
        public Matrix viewMatrix;
        private Vector2 halfScreenSize;
        private Vector2 position;

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
            //halfScreenSize = new Vector2(GameWorld.ScreenSize.Width / 2, GameWorld.ScreenSize.Height / 2);
            UpdateViewMatrix();
        }

        /// <summary>
        /// Method that updates the camera position to the new position
        /// </summary>
        private void UpdateViewMatrix()
        {
            viewMatrix = Matrix.CreateTranslation(halfScreenSize.X - position.X, halfScreenSize.Y - position.Y, 0f);
        }
    }
}
