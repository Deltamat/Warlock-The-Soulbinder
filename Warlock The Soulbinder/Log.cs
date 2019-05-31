using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Log : Menu
    {
        static Log instance;
        private List<int> logList = new List<int>();
        private int sheepLog = 1;
        private int wolfLog = 1;
        private int bearLog = 1;
        private int plantEaterLog = 2;
        private int insectSoldierLog = 0;
        private int slimeSnakeLog = 0;
        private int tentacleLog = 0;
        private int frogLog = 10;
        private int fishLog = 0;
        private int mummyLog = 3;
        private int vampireLog = 4;
        private int bansheeLog = 0;
        private int bucketManLog = 0;
        private int defenderLog = 0;
        private int sentryLog = 0;
        private int fireGolemLog = 0;
        private int infernalDemonLog = 0;
        private int ashZombieLog = 0;
        private int falconLog = 0;
        private int batLog = 8;
        private int ravenLog = 0;
        private float neutralBonus = 0;
        private float earthBonus = 0;
        private float waterBonus = 0;
        private float darkBonus = 0;
        private float metalBonus = 0;
        private float fireBonus = 0;
        private float airBonus = 0;

        public static Log Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Log();
                }
                return instance;
            }
        }

        public List<int> LogList { get => logList; set => logList = value; }
        public int SheepLog { get => sheepLog; set => sheepLog = value; }
        public int WolfLog { get => wolfLog; set => wolfLog = value; }
        public int BearLog { get => bearLog; set => bearLog = value; }
        public int PlantEaterLog { get => plantEaterLog; set => plantEaterLog = value; }
        public int InsectSoldierLog { get => insectSoldierLog; set => insectSoldierLog = value; }
        public int SlimeSnakeLog { get => slimeSnakeLog; set => slimeSnakeLog = value; }
        public int TentacleLog { get => tentacleLog; set => tentacleLog = value; }
        public int FrogLog { get => frogLog; set => frogLog = value; }
        public int FishLog { get => fishLog; set => fishLog = value; }
        public int MummyLog { get => mummyLog; set => mummyLog = value; }
        public int VampireLog { get => vampireLog; set => vampireLog = value; }
        public int BansheeLog { get => bansheeLog; set => bansheeLog = value; }
        public int BucketManLog { get => bucketManLog; set => bucketManLog = value; }
        public int DefenderLog { get => defenderLog; set => defenderLog = value; }
        public int SentryLog { get => sentryLog; set => sentryLog = value; }
        public int FireGolemLog { get => fireGolemLog; set => fireGolemLog = value; }
        public int InfernalDemonLog { get => infernalDemonLog; set => infernalDemonLog = value; }
        public int AshZombieLog { get => ashZombieLog; set => ashZombieLog = value; }
        public int FalconLog { get => falconLog; set => falconLog = value; }
        public int BatLog { get => batLog; set => batLog = value; }
        public int RavenLog { get => ravenLog; set => ravenLog = value; }
        public float NeutralBonus { get => neutralBonus; set => neutralBonus = value; }
        public float EarthBonus { get => earthBonus; set => earthBonus = value; }
        public float WaterBonus { get => waterBonus; set => waterBonus = value; }
        public float DarkBonus { get => darkBonus; set => darkBonus = value; }
        public float MetalBonus { get => metalBonus; set => metalBonus = value; }
        public float FireBonus { get => fireBonus; set => fireBonus = value; }
        public float AirBonus { get => airBonus; set => airBonus = value; }

        private Log()
        {
        }

        public void GenerateLogList()
        {
            LogList.Clear();
            LogList.Add(sheepLog);
            LogList.Add(wolfLog);
            LogList.Add(bearLog);
            LogList.Add(plantEaterLog);
            LogList.Add(insectSoldierLog);
            LogList.Add(slimeSnakeLog);
            LogList.Add(tentacleLog);
            LogList.Add(frogLog);
            LogList.Add(fishLog);
            LogList.Add(mummyLog);
            LogList.Add(vampireLog);
            LogList.Add(bansheeLog);
            LogList.Add(bucketManLog);
            LogList.Add(defenderLog);
            LogList.Add(sentryLog);
            LogList.Add(fireGolemLog);
            LogList.Add(infernalDemonLog);
            LogList.Add(ashZombieLog);
            LogList.Add(falconLog);
            LogList.Add(batLog);
            LogList.Add(ravenLog);
        }

        public void Draw(SpriteBatch spriteBatch, int monsterIndex)
        {

            switch(monsterIndex)
            {
                case 0:
                    if (sheepLog > 0)
                    {
                        spriteBatch.Draw(GameWorld.Instance.Content.Load<Texture2D>("monsters/orbs/Sheep"), new Vector2(425, 100), Color.White);

                    }
                    else
                    {
                        spriteBatch.Draw(GameWorld.Instance.Content.Load<Texture2D>("monsters/orbs/Sheep"), new Vector2(425, 100), Color.Black);

                    }
                    break;
                
               
            }

            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Name:", new Vector2(175, 310), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Element:", new Vector2(175, 390), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Levels:", new Vector2(175, 470), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Health:", new Vector2(175, 550), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Resistance:", new Vector2(175, 630), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Defense:", new Vector2(175, 710), Color.White);

        }

        public int FullScans()
        {
            int fullScans = 0;
            for (int i = 0; i < LogList.Count; i++)
            {
                if (LogList[i] == 10)
                {
                    fullScans++;
                }
            }

            return fullScans;
        }

        public void CalculateBonus()
        {
            NeutralBonus = ((float)(sheepLog + wolfLog + bearLog) / 100);
            EarthBonus = ((float)(insectSoldierLog + plantEaterLog + slimeSnakeLog) / 100);
            WaterBonus = ((float)(fishLog + frogLog + tentacleLog) / 100);
            DarkBonus = ((float)(bansheeLog + mummyLog + vampireLog) / 100);
            FireBonus = ((float)(ashZombieLog + infernalDemonLog + fireGolemLog) / 100);
            MetalBonus = ((float)(defenderLog + sentryLog + bucketManLog) / 100);
            AirBonus = ((float)(ravenLog + falconLog + batLog) / 100);
        }
    }
}
