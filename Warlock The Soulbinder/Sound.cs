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
        SoundEffect sound;

        /// <summary>
        /// Create a new Sound
        /// </summary>
        /// <param name="sound">The name of the sound file</param>
        public Sound(string sound)
        {
            this.sound = GameWorld.ContentManager.Load<SoundEffect>($"sound/{sound}");
        }

        /// <summary>
        /// Plays the sound with GameWorld.Instance.SoundVolume volume level
        /// </summary>
        public void Play()
        {
            sound.Play(GameWorld.Instance.SoundEffectVolume, 0f, 0f);
        }
    }
}
