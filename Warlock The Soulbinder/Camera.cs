using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Camera
    {
        public Matrix viewMatrix;
        private Vector2 halfScreenSize;
        private Vector2 position;

        public Vector2 Position { get => position; set => position = value; }

        public Camera()
        {

        }

        public void UpdateViewMatrix()
        {

        }
    }
}
