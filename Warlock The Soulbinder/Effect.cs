using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Effect
    {
        private bool targetsSelf = false;
        private bool targetBoth = false;
        private int cooldown = 1;
        private int damage;
        private int heal;
        private int effectlength = 1;
        private float damageReduction;
        private int damageAbs;
        private float speedMod;
        private float accuracyMod;
        private float damageMod;
        private int shield;
        private bool stun = false;
        private bool confuse = false;
        private bool doubleAttack = false;
        private string effectString;

        public Effect(int index, string type, int damage, FilledStone stone, CharacterCombat characterCombat)
        {
            switch (type)
            {
                case "Weapon":
                    switch (index)
                    {
                        case 0: //sheep
                            EffectString = "Has a chance to headbutt the enemy, stunning them";
                            if (GameWorld.Instance.RandomInt(0, 10) == 0)
                            {
                                Stun = true;
                            }
                            break;
                        case 1: //bear
                            EffectString = "Has a chance to maul the enemy, causing them to bleed";
                            if (GameWorld.Instance.RandomInt(0, 8) == 0)
                            {
                                Damage = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.2f));
                                Effectlength = 3;
                            }
                            break;
                        case 2: //wolf
                            EffectString = "Activates your killer instincts, passively giving you more damage";
                            if (characterCombat != null)
                            {
                                TargetsSelf = true;
                                characterCombat.Damage = (int)(characterCombat.Damage * 1.1f);
                            }
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to steal nutrients from the enemy, healing you";
                            if (GameWorld.Instance.RandomInt(0, 5) == 0)
                            {
                                TargetsSelf = true;
                                Heal = (int)(damage * 0.2f);
                            }
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to poison the enemy";
                            if (GameWorld.Instance.RandomInt(0, 6) == 0)
                            {
                                Damage = (int)(2.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.15f));
                                Effectlength = 5;
                            }
                            break;
                        case 5: //slime eater
                            EffectString = "Has a chance to lower the damage of your enemy";
                            if (GameWorld.Instance.RandomInt(0, 9) == 0)
                            {
                                DamageMod = 0.75f;
                            }
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to constrict your enemy with tentacles, crushing them";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                Damage = (int)(2.75 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                                Effectlength = 4;
                            }
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your enemy's speed";
                            if (GameWorld.Instance.RandomInt(0, 5) == 0)
                            {
                                SpeedMod = 0.7f;
                            }
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate your health";
                            if (GameWorld.Instance.RandomInt(0, 10) == 0)
                            {
                                TargetsSelf = true;
                                Heal = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            }
                            break;
                        case 9: //mummy
                            EffectString = "Has a chance to curse your enemy, lowering their accuracy";
                            if (GameWorld.Instance.RandomInt(0, 6) == 0)
                            {
                                AccuracyMod = 0.7f;
                                Effectlength = 2;
                            }
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to steal blood from your enemy, \nhealing a percentage of the damage you dealt";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                TargetsSelf = true;
                                Heal = (int)(damage * 0.3f);
                            }
                            break;
                        case 11: //banshee
                            EffectString = "Has a chance to paralyze your enemy, stunning them";
                            if (GameWorld.Instance.RandomInt(0, 15) == 0)
                            {
                                Stun = true;
                                Effectlength = 2;
                            }
                            break;
                        case 12: //bucket man
                            EffectString = "Has a chance to deal double damage";
                            if (GameWorld.Instance.RandomInt(0, 14) == 0)
                            {
                                TargetsSelf = true;
                                DamageMod = 2;
                            }
                            break;
                        case 13: //defender
                            EffectString = "Has a chance to break your enemy's armor";
                            if (GameWorld.Instance.RandomInt(0, 3) == 0)
                            {
                                DamageReduction = -0.33f;
                                Effectlength = 3;
                            }
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase you hit rating";
                            if (GameWorld.Instance.RandomInt(0, 3) == 0)
                            {
                                TargetsSelf = true;
                                AccuracyMod = 1.25f;
                                Effectlength = 3;
                            }
                            break;
                        case 15: //fire golem
                            EffectString = "Causes you to attack slower, but deal more damage";
                            if (characterCombat != null)
                            {
                                TargetsSelf = true;
                                characterCombat.Damage = (int)(characterCombat.Damage * 1.5f);
                                characterCombat.AttackSpeed *= 0.8f;
                            }
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your enemy on fire";
                            if (GameWorld.Instance.RandomInt(0, 3) == 0)
                            {
                                Damage = (int)(4 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.3f));
                                Effectlength = 2;
                            }
                            break;
                        case 17: //ash zombie
                            EffectString = "Has a chance to quickly deal two smaller blows to your enemy";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                TargetsSelf = true;
                                DoubleAttack = true;
                                DamageMod = 0.75f;
                            }
                            break;
                        case 18: //falcon
                            EffectString = "Has a chance to blind your enemy, reducing their accuracy";
                            if (GameWorld.Instance.RandomInt(0, 13) == 0)
                            {
                                AccuracyMod = 0.1f;
                                Effectlength = 3;
                            }
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse your enemy";
                            if (GameWorld.Instance.RandomInt(0, 15) == 0)
                            {
                                Confuse = true;
                                Effectlength = 2;
                            }
                            break;
                        case 20: //raven
                            EffectString = "Has a chance to increase your speed for a little while";
                            if (GameWorld.Instance.RandomInt(0, 8) == 0)
                            {
                                TargetsSelf = true;
                                SpeedMod = 1.5f;
                                Effectlength = 3;
                            }
                            break;
                    }
                    break;
                case "Armor":
                    switch (index)
                    {
                        case 0: //sheep
                            EffectString = "Passively gives you more defense";
                            if (characterCombat != null)
                            {
                                TargetsSelf = true;
                                characterCombat.Defense += (int)(1.5f * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.1f));
                            }
                            break;
                        case 1: //bear
                            EffectString = "Passively gives you more max health";
                            if (characterCombat != null)
                            {
                                TargetsSelf = true;
                                characterCombat.MaxHealth += (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.1f));
                            }
                            break;
                        case 2: //wolf
                            EffectString = "Has a chance to give you more damage after being hit";
                            if (GameWorld.Instance.RandomInt(0, 8) == 0)
                            {
                                TargetsSelf = true;
                                DamageMod = 1.5f;
                                Effectlength = 2;
                            }
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to retaliate when attacked, \ndealing damage and healing you";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                TargetBoth = true;
                                int localDamage;
                                localDamage = (int)(characterCombat.Damage * 0.25f);
                                Damage = localDamage;
                                Heal = (int)(localDamage * 0.5f);
                            }
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to posion the enemy when attacked";
                            if (GameWorld.Instance.RandomInt(0, 8) == 0)
                            {
                                Damage = (int)(2.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.15f));
                                Effectlength = 4;
                            }
                            break;
                        case 5: //slime eater
                            EffectString = "Has a chance to give you a shield after being hit";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                TargetsSelf = true;
                                Shield = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.3f));
                                Effectlength = 5;
                            }
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to retaliate with a tentacle when hit, \ndealing damage to your attacker";
                            if (GameWorld.Instance.RandomInt(0, 2) == 0)
                            {
                                Damage = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.2f));
                            }
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your enemy down when attacked";
                            if (GameWorld.Instance.RandomInt(0, 10) == 0)
                            {
                                SpeedMod = 0.8f;
                                Effectlength = 2;
                            }
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate some health when attacked";
                            if (GameWorld.Instance.RandomInt(0, 15) == 0)
                            {
                                TargetsSelf = true;
                                Heal = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            }
                            break;
                        case 9: //mummy
                            EffectString = "Gives you immunity to curses";
                            TargetsSelf = true;
                            AccuracyMod = 2;
                            Effectlength = 999;
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to block the next hit after being attacked";
                            if (GameWorld.Instance.RandomInt(0, 5) == 0)
                            {
                                TargetsSelf = true;
                                Shield = damage;
                                Effectlength = 5;
                            }
                            break;
                        case 11: //banshee
                            //help! don't know what to do here!
                            break;
                        case 12: //bucket man
                            EffectString = "Has a chance to give yourself a shield after being hit";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                TargetsSelf = true;
                                Shield = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.3f));
                                Effectlength = 5;
                            }
                            break;
                        case 13: //defender
                            EffectString = "Passively reduces damage taken";
                            TargetsSelf = true;
                            DamageAbs = (int)(1.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            Effectlength = 999;
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase your dodge rating after being hit";
                            if (GameWorld.Instance.RandomInt(0, 7) == 0)
                            {
                                AccuracyMod = 0.6f;
                                Effectlength = 3;
                            }
                            break;
                        case 15: //fire golem
                            EffectString = "Passively reduces damage taken";
                            TargetsSelf = true;
                            DamageReduction = 0.2f;
                            Effectlength = 999;
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your enemy on fire when attacked";
                            if (GameWorld.Instance.RandomInt(0, 6) == 0)
                            {
                                Damage = (int)(3.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.25f));
                                Effectlength = 2;
                            }
                            break;
                        case 17: //ash zombie
                            EffectString = "Passively increases your attack speed";
                            characterCombat.AttackSpeed *= 1.25f;
                            break;
                        case 18: //falcon
                            EffectString = "Has a chance to blind the enemy when hit";
                            if (GameWorld.Instance.RandomInt(0, 20) == 0)
                            {
                                AccuracyMod = 0.1f;
                                Effectlength = 3;
                            }
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse your enemy when attacked";
                            if (GameWorld.Instance.RandomInt(0, 8) == 0)
                            {
                                Confuse = true;
                                Effectlength = 2;
                            }
                            break;
                        case 20: //raven
                            EffectString = "Passively increases your dodge chance";
                            AccuracyMod = 0.7f;
                            Effectlength = 999;
                            break;
                    }
                    break;
                case "Skill":
                    switch (index)
                    {
                        case 0: //sheep
                            EffectString = "Headbutt the enemy stunning them";
                            Stun = true;
                            Cooldown = 3;
                            break;
                        case 1: //bear
                            EffectString = "Maul your enemy causing them to bleed";
                            Damage = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.33f));
                            Effectlength = 3;
                            Cooldown = 5;
                            break;
                        case 2: //wolf
                            EffectString = "Activate you killer instincts, increasing your damage";
                            TargetsSelf = true;
                            DamageMod = 1.5f;
                            Effectlength = 3;
                            Cooldown = 6;
                            break;
                        case 3: //plant eater
                            EffectString = "Steal health from the enemy";
                            TargetBoth = true;
                            int localDamage;
                            localDamage = (int)(characterCombat.Damage * 0.75f);
                            Damage = localDamage;
                            Heal = (int)(localDamage * 0.5f);
                            Cooldown = 5;
                            break;
                        case 4: //insect soldier
                            EffectString = "Poison your enemy";
                            Damage = (int)(2.75 * ((stone.Level + GameWorld.Instance.RandomInt(2, 4)) * 0.25f));
                            Effectlength = 5;
                            Cooldown = 5;
                            break;
                        case 5: //slime eater
                            EffectString = "Cover you self in a thick layer of slime, reducing damage you take";
                            TargetsSelf = true;
                            DamageAbs = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(2, 3)) * 0.4f));
                            Effectlength = 4;
                            Cooldown = 5;
                            break;
                        case 6: //tentacle
                            EffectString = "Wrap tentacles around your enemy crushing them for a few rounds";
                            Damage = (int)(3.25 * ((stone.Level + GameWorld.Instance.RandomInt(2, 6)) * 0.3f));
                            Effectlength = 4;
                            Cooldown = 6;
                            break;
                        case 7: //frog
                            EffectString = "Shoot mucus at you enemy slowing them down";
                            SpeedMod = 0.6f;
                            Effectlength = 3;
                            Cooldown = 6;
                            break;
                        case 8: //fish
                            EffectString = "Regenerate health for a few round";
                            TargetsSelf = true;
                            Heal = (int)(1.25 * ((stone.Level + GameWorld.Instance.RandomInt(3, 6)) * 0.7f));
                            Effectlength = 3;
                            Cooldown = 8;
                            break;
                        case 9: //mummy
                            EffectString = "Curses your enemy, lowering their accuracy";
                            AccuracyMod = 0.6f;
                            Effectlength = 5;
                            Cooldown = 7;
                            break;
                        case 10: //vampire
                            EffectString = "Creates a bloodshield blocking your enemy's next three attacks";
                            TargetsSelf = true;
                            Shield = damage * 3;
                            Effectlength = 5;
                            Cooldown = 10;
                            break;
                        case 11: //banshee
                            EffectString = "Paralyse your enemy";
                            Stun = true;
                            Effectlength = 2;
                            Cooldown = 5;
                            break;
                        case 12: //bucket man
                            EffectString = "A powerful attack that is difficult to land";
                            TargetBoth = true;
                            AccuracyMod = 0.1f;
                            Damage = (characterCombat.Damage + characterCombat.DamageTypes[3]) * 15;
                            Cooldown = 0;
                            break;
                        case 13: //defender
                            EffectString = "Causes you to enter a defensive stance, \nincreasing your defences but lowers your damage";
                            TargetsSelf = true;
                            DamageMod = 0.25f;
                            DamageReduction = 80;
                            Effectlength = 5;
                            Cooldown = 9;
                            break;
                        case 14: //sentry
                            EffectString = "Scans your enemy revealing their stats to you";
                            //scan code goes here!
                            break;
                        case 15: //fire golem
                            EffectString = "Attacks your enemy with a critical attack";
                            Damage = damage * 2;
                            Cooldown = 4;
                            break;
                        case 16: //infernal golem
                            EffectString = "Sets your enemy ablaze";
                            Damage = (int)(4.25 * ((stone.Level + GameWorld.Instance.RandomInt(2, 5)) * 0.35f));
                            Effectlength = 3;
                            Cooldown = 6;
                            break;
                        case 17: //ash zombie
                            EffectString = "Causes your next two attacks to strike twice";
                            DoubleAttack = true;
                            Effectlength = 2;
                            Cooldown = 7;
                            break;
                        case 18: //falcon
                            EffectString = "Pecks out your enemy's eyes, lowering their accuracy for a few rounds, \nuntil they magically regenerate their eyes";
                            AccuracyMod = 0.1f;
                            Effectlength = 3;
                            Cooldown = 10;
                            break;
                        case 19: //bat
                            EffectString = "Confuses your enemy with a super sonic screech";
                            Confuse = true;
                            Effectlength = 2;
                            Cooldown = 5;
                            break;
                        case 20: //raven
                            EffectString = "Increases your speed for a few rounds";
                            TargetsSelf = true;
                            SpeedMod = 1.55f;
                            Effectlength = 3;
                            Cooldown = 6;
                            break;
                    }
                    break;
            }
        }

        public bool TargetsSelf { get => targetsSelf; set => targetsSelf = value; }
        public bool TargetBoth { get => targetBoth; set => targetBoth = value; }
        public int Cooldown { get => cooldown; set => cooldown = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Heal { get => heal; set => heal = value; }
        public int Effectlength { get => effectlength; set => effectlength = value; }
        public float DamageReduction { get => damageReduction; set => damageReduction = value; }
        public int DamageAbs { get => damageAbs; set => damageAbs = value; }
        public float SpeedMod { get => speedMod; set => speedMod = value; }
        public float AccuracyMod { get => accuracyMod; set => accuracyMod = value; }
        public float DamageMod { get => damageMod; set => damageMod = value; }
        public int Shield { get => shield; set => shield = value; }
        public bool Stun { get => stun; set => stun = value; }
        public bool Confuse { get => confuse; set => confuse = value; }
        public bool DoubleAttack { get => doubleAttack; set => doubleAttack = value; }
        public string EffectString { get => effectString; set => effectString = value; }
    }
}
