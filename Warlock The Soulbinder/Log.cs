﻿using Microsoft.Xna.Framework;
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
        private static Log instance;
        private List<int> logList = new List<int>();
        private int sheepLog = 10;
        private int wolfLog = 10;
        private int bearLog = 10;
        private int plantEaterLog = 10;
        private int insectSoldierLog = 10;
        private int slimeSnakeLog = 10;
        private int tentacleLog = 10;
        private int frogLog = 10;
        private int fishLog = 10;
        private int mummyLog = 10;
        private int vampireLog = 10;
        private int bansheeLog = 10;
        private int bucketManLog = 10;
        private int defenderLog = 10;
        private int sentryLog = 10;
        private int fireGolemLog = 10;
        private int infernalDemonLog = 10;
        private int ashZombieLog = 10;
        private int falconLog = 10;
        private int batLog = 10;
        private int ravenLog = 10;
        private float neutralBonus = 10;
        private float earthBonus = 10;
        private float waterBonus = 10;
        private float darkBonus = 10;
        private float metalBonus = 10;
        private float fireBonus = 10;
        private float airBonus = 10;
        private bool logBegun = true;

        /// <summary>
        /// Creates an instance for the singleton
        /// </summary>
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
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int SheepLog { get => sheepLog; set => sheepLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int WolfLog { get => wolfLog; set => wolfLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int BearLog { get => bearLog; set => bearLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int PlantEaterLog { get => plantEaterLog; set => plantEaterLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int InsectSoldierLog { get => insectSoldierLog; set => insectSoldierLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int SlimeSnakeLog { get => slimeSnakeLog; set => slimeSnakeLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int TentacleLog { get => tentacleLog; set => tentacleLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int FrogLog { get => frogLog; set => frogLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int FishLog { get => fishLog; set => fishLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int MummyLog { get => mummyLog; set => mummyLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int VampireLog { get => vampireLog; set => vampireLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int BansheeLog { get => bansheeLog; set => bansheeLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int BucketManLog { get => bucketManLog; set => bucketManLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int DefenderLog { get => defenderLog; set => defenderLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int SentryLog { get => sentryLog; set => sentryLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int FireGolemLog { get => fireGolemLog; set => fireGolemLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int InfernalDemonLog { get => infernalDemonLog; set => infernalDemonLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int AshZombieLog { get => ashZombieLog; set => ashZombieLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int FalconLog { get => falconLog; set => falconLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int BatLog { get => batLog; set => batLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public int RavenLog { get => ravenLog; set => ravenLog = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float NeutralBonus { get => neutralBonus; set => neutralBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float EarthBonus { get => earthBonus; set => earthBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float WaterBonus { get => waterBonus; set => waterBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float DarkBonus { get => darkBonus; set => darkBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float MetalBonus { get => metalBonus; set => metalBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float FireBonus { get => fireBonus; set => fireBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public float AirBonus { get => airBonus; set => airBonus = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public bool LogBegun { get => logBegun; set => logBegun = value; }

        private Log()
        {
        }

        /// <summary>
        /// resets the log
        /// </summary>
        public void ResetForMainMenu()
        {
            sheepLog = 0;
            wolfLog = 0;
            bearLog = 0;
            plantEaterLog = 0;
            insectSoldierLog = 0;
            slimeSnakeLog = 0;
            tentacleLog = 0;
            frogLog = 0;
            fishLog = 0;
            mummyLog = 0;
            vampireLog = 0;
            bansheeLog = 0;
            bucketManLog = 0;
            defenderLog = 0;
            sentryLog = 0;
            fireGolemLog = 0;
            infernalDemonLog = 0;
            ashZombieLog = 0;
            falconLog = 0;
            batLog = 0;
            ravenLog = 0;
        }

        /// <summary>
        /// Generates the list of creatures for the list.
        /// </summary>
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

        /// <summary>
        /// Draws the log
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="monsterIndex">An integer from general menu that will determine what monster log is being drawn</param>
        public void Draw(SpriteBatch spriteBatch, int monsterIndex)
        {
            DrawLog(spriteBatch, monsterIndex);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 1:", new Vector2(175, 310), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 2:", new Vector2(175, 390), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 3:", new Vector2(175, 470), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 4:", new Vector2(175, 550), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 5:", new Vector2(175, 630), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 6:", new Vector2(175, 710), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 7:", new Vector2(175, 790), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 8:", new Vector2(1050, 150), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 9:", new Vector2(1050, 230), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, $"Scan 10:", new Vector2(1050, 310), Color.White);
        }


        /// <summary>
        /// Code to check how many creatures has been scanned fully AKA 10 times
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates all bonuses from the elemental scans, and starts to draw log if log list isnt empty
        /// </summary>
        public void CalculateBonus()
        {
            GenerateLogList();
            NeutralBonus = ((float)(sheepLog + wolfLog + bearLog) / 100);
            EarthBonus = ((float)(insectSoldierLog + plantEaterLog + slimeSnakeLog) / 100);
            WaterBonus = ((float)(fishLog + frogLog + tentacleLog) / 100);
            DarkBonus = ((float)(bansheeLog + mummyLog + vampireLog) / 100);
            FireBonus = ((float)(ashZombieLog + infernalDemonLog + fireGolemLog) / 100);
            MetalBonus = ((float)(defenderLog + sentryLog + bucketManLog) / 100);
            AirBonus = ((float)(ravenLog + falconLog + batLog) / 100);

            foreach (int item in LogList)
            {
                if (item > 0)
                {
                    LogBegun = true;
                }
            }
        }

        /// <summary>
        /// Method to draw the 21 different monstesrs by using a switchcase of monster index,
        /// </summary>
        /// <param name="spriteBatch"> spritebatch </param>
        /// <param name="monsterIndex"> what index number the monster has </param>
        public void DrawLog(SpriteBatch spriteBatch, int monsterIndex)
        {
            int monsterLog = 0;
            string monsterString = "0";
            switch (monsterIndex)
            {
                case 0:
                    monsterLog = sheepLog;
                    monsterString = "Sheep";
                    break;

                case 1:
                    monsterLog = wolfLog;
                    monsterString = "Wolf";
                    break;

                case 2:
                    monsterLog = bearLog;
                    monsterString = "Bear";
                    break;

                case 3:
                    monsterLog = plantEaterLog;
                    monsterString = "PlantEater";
                    break;

                case 4:
                    monsterLog = insectSoldierLog;
                    monsterString = "InsectSoldier";
                    break;

                case 5:
                    monsterLog = slimeSnakeLog;
                    monsterString = "SlimeSnake";
                    break;

                case 6:
                    monsterLog = tentacleLog;
                    monsterString = "Tentacle";
                    break;

                case 7:
                    monsterLog = FrogLog;
                    monsterString = "Frog";
                    break;

                case 8:
                    monsterLog = FishLog;
                    monsterString = "Fish";
                    break;

                case 9:
                    monsterLog = MummyLog;
                    monsterString = "Mummy";
                    break;

                case 10:
                    monsterLog = VampireLog;
                    monsterString = "Vampire";
                    break;

                case 11:
                    monsterLog = BansheeLog;
                    monsterString = "Banshee";
                    break;

                case 12:
                    monsterLog = BucketManLog;
                    monsterString = "BucketMan";
                    break;

                case 13:
                    monsterLog = DefenderLog;
                    monsterString = "Defender";
                    break;

                case 14:
                    monsterLog = SentryLog;
                    monsterString = "Sentry";
                    break;

                case 15:
                    monsterLog = FireGolemLog;
                    monsterString = "FireGolem";
                    break;

                case 16:
                    monsterLog = InfernalDemonLog;
                    monsterString = "InfernalDemon";
                    break;

                case 17:
                    monsterLog = AshZombieLog;
                    monsterString = "AshZombie";
                    break;

                case 18:
                    monsterLog = FalconLog;
                    monsterString = "Falcon";
                    break;

                case 19:
                    monsterLog = BatLog;
                    monsterString = "Bat";
                    break;

                case 20:
                    monsterLog = RavenLog;
                    monsterString = "Raven";
                    break;

            }

            spriteBatch.Draw(GameWorld.Instance.Content.Load<Texture2D>($"monsters/orbs/{monsterString}"), new Vector2(425, 100), Color.White);

            if (monsterLog > 0)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 310), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 310), Color.White);
            }

            if (monsterLog > 1)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 390), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 390), Color.White);
            }

            if (monsterLog > 2)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 470), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 470), Color.White);
            }

            if (monsterLog > 3)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 550), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 550), Color.White);
            }

            if (monsterLog > 4)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 630), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 630), Color.White);
            }

            if (monsterLog > 5)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 710), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 710), Color.White);
            }

            if (monsterLog > 6)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(400, 790), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(400, 790), Color.White);
            }

            if (monsterLog > 7)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(1275, 150), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(1275, 150), Color.White);
            }

            if (monsterLog > 8)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(1275, 230), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(1275, 230), Color.White);
            }

            if (monsterLog > 9)
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Complete", new Vector2(1310, 310), Color.Gold);

            }

            else
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, $"Incomplete", new Vector2(1310, 310), Color.White);
            }
        }

        /// <summary>
        /// Entire code for scalling the target increasing its scan count by 1
        /// </summary>
        /// <param name="target"> target that is being scanned </param>
        public void ScanCreature(Enemy target)
        {
            switch (target.Monster)
            {
                case "sheep":
                    if (sheepLog < 10)
                    {
                        sheepLog++;
                    }

                break;

                case "wolf":
                    if (wolfLog < 10)
                    {
                        wolfLog++;
                    }

                    break;

                case "bear":
                    if (bearLog < 10)
                    {
                        bearLog++;
                    }

                    break;

                case "plantEater":
                    if (plantEaterLog < 10)
                    {
                        plantEaterLog++;
                    }

                    break;

                case "insectSoldier":
                    if (insectSoldierLog < 10)
                    {
                        insectSoldierLog++;
                    }

                    break;

                case "slimeSnake":
                    if (slimeSnakeLog < 10)
                    {
                        slimeSnakeLog++;
                    }

                    break;

                case "fish":
                    if (fishLog < 10)
                    {
                        fishLog++;
                    }

                    break;

                case "tentacle":
                    if (tentacleLog < 10)
                    {
                        tentacleLog++;
                    }

                    break;

                case "frog":
                    if (frogLog < 10)
                    {
                        frogLog++;
                    }

                    break;

                case "mummy":
                    if (mummyLog < 10)
                    {
                        mummyLog++;
                    }

                    break;

                case "banshee":
                    if (bansheeLog < 10)
                    {
                        bansheeLog++;
                    }

                    break;

                case "vampire":
                    if (sheepLog < 10)
                    {
                        sheepLog++;
                    }

                    break;

                case "bucketMan":
                    if (bucketManLog < 10)
                    {
                        bucketManLog++;
                    }
                    break;


                case "defender":
                    if (defenderLog < 10)
                    {
                        defenderLog++;
                    }

                    break;

                case "sentry":
                    if (sentryLog < 10)
                    {
                        sentryLog++;
                    }

                    break;

                case "fireGolem":
                    if (fireGolemLog < 10)
                    {
                        fireGolemLog++;
                    }

                    break;

                case "infernalDemon":
                    if (infernalDemonLog < 10)
                    {
                        infernalDemonLog++;
                    }

                    break;

                case "AshZombie":
                    if (ashZombieLog < 10)
                    {
                        ashZombieLog++;
                    }

                    break;


                case "falcon":
                    if (falconLog < 10)
                    {
                        falconLog++;
                    }

                    break;

                case "bat":
                    if (batLog < 10)
                    {
                        batLog++;
                    }

                    break;

                case "raven":
                    if (ravenLog < 10)
                    {
                        ravenLog++;
                    }

                    break;
            }
        }
    }
}