using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Sound 
    {
        /// <summary>
        /// Play a SoundEffect
        /// </summary>
        /// <param name="soundLocation">The path and name of the sound file</param>
        public static void PlaySound(string soundLocation)
        {
            SoundEffect sound = GameWorld.ContentManager.Load<SoundEffect>(soundLocation);
            sound.Play(GameWorld.Instance.SoundEffectVolume, 0f, 0f);
        }
    }
}
