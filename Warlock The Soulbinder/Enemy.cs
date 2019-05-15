using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Enemy : Character
    {
        private string monster;
        private int level;
        enum EMonster
        {
            bear, sheep, wolf, //neutral (0,1,2)
            bucketMan, defender, sentry, //metal (3,4,5)
            plantEater, insectSoldier, slimeEater, //earth (6,7,8)
            falcon, bat, raven, //air (9,10,11)
            fireGolem, infernalGolem, ashZombie, //fire (12,13,14)
            mummy, vampire, banshee, //dark (15,16,17)
            tentacle, frog, fish //water (18,19,20)
        };

        public Enemy(int index) : base(index)
        {
            monster = Enum.GetName(typeof(EMonster), index);
        }
    }
}
