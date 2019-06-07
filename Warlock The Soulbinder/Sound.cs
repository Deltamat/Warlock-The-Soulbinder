using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Sound 

    {
        SoundEffect sound;

       /// <summary>
       /// base, monster, menu
       /// </summary>
       /// <param name="sound"></param>
       /// <param name="type"></param>
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

        public static void PlaySound(string soundLocation)
        {
            SoundEffect sound = GameWorld.ContentManager.Load<SoundEffect>(soundLocation);
            sound.Play(GameWorld.Instance.SoundEffectVolume, 0f, 0f);
        }
    }
}
