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
        private bool statBuff = false;
        private int cooldown = 1;
        private int damage;
        private int heal;
        private int effectlength = 1;
        private float damageReduction = 1;
        private int damageAbs;
        private float speedMod = 1;
        private float accuracyMod = 1;
        private float damageMod = 1;
        private int shield;
        private bool stun = false;
        private bool confuse = false;
        private bool doubleAttack = false;
        private bool retaliate = false;
        private bool stunImmunity = false;
        private string effectString;
        private int upperChanceBounds = 1;

        private int index;
        private string type;
        private FilledStone stone;

        public Effect(int index, string type, FilledStone stone, CharacterCombat characterCombat, int damageDealt)
        {
            Index = index;
            Type = type;
            Stone = stone;
            switch (type)
            {
                case "Weapon":
                    switch (index)
                    {
                        case 0: //sheep
                            EffectString = "Has a chance to headbutt the\nenemy, stunning them";
                            UpperChanceBounds = 10;
                            Stun = true;
                            break;
                        case 1: //wolf
                            EffectString = "Activates your killer instincts,\npassively giving you more damage";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                Combat.Instance.WolfBuff = 0.2f;
                            }
                            break;
                        case 2: //bear
                            EffectString = "Has a chance to maul the enemy,\ncausing them to bleed";
                            UpperChanceBounds = 8;
                            Damage = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.2f));
                            EffectLength = 3;
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to steal nutrients\nfrom the enemy, healing you";
                            UpperChanceBounds = 5;
                            TargetsSelf = true;
                            Heal = (int)(damageDealt * 0.2f);
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to poison the enemy";
                            UpperChanceBounds = 6;
                            Damage = (int)(2.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.15f));
                            EffectLength = 5;
                            break;
                        case 5: //slime snake
                            EffectString = "Has a chance to lower the\ndamage of your enemy";
                            UpperChanceBounds = 9;
                            DamageMod = 0.75f;
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to constrict your\nenemy with tentacles, crushing them";
                            UpperChanceBounds = 7;
                            Damage = (int)(2.75 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            EffectLength = 4;
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your\nenemy's speed";
                            UpperChanceBounds = 5;
                            SpeedMod = 0.7f;
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate\nyour health";
                            UpperChanceBounds = 10;
                            TargetsSelf = true;
                            Heal = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            break;
                        case 9: //mummy
                            EffectString = "Has a chance to curse your\nenemy, lowering their accuracy";
                            UpperChanceBounds = 6;
                            AccuracyMod = 0.7f;
                            EffectLength = 2;
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to steal blood from\nyour enemy, healing a percentage\nof the damage you dealt";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Heal = (int)(damageDealt * 0.3f);
                            break;
                        case 11: //banshee
                            EffectString = "Has a chance to paralyze your\nenemy, stunning them";
                            UpperChanceBounds = 13;
                            Stun = true;
                            EffectLength = 2;
                            break;
                        case 12: //bucket man
                            EffectString = "Has a chance to deal double damage\non your next attack";
                            UpperChanceBounds = 9;
                            TargetsSelf = true;
                            DamageMod = 2;
                            break;
                        case 13: //defender
                            EffectString = "Has a chance to break your\nenemy's armor";
                            UpperChanceBounds = 3;
                            DamageReduction = 1.66f;
                            EffectLength = 3;
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase\nyour hit rating";
                            //UpperChanceBounds = 3;
                            TargetsSelf = true;
                            AccuracyMod = 1.25f;
                            EffectLength = 3;
                            break;
                        case 15: //fire golem
                            EffectString = "Causes you to attack slower,\nbut deal more damage";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.Damage = (int)(characterCombat.Damage * 1.5f);
                                characterCombat.AttackSpeed *= (int)0.8f;
                            }
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your\nenemy on fire";
                            UpperChanceBounds = 3;
                            Damage = (int)(4 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.3f));
                            EffectLength = 2;
                            break;
                        case 17: //ash zombie
                            EffectString = "Has a chance to quickly deal\ntwo smaller blows to your enemy\non your next turn";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            DoubleAttack = true;
                            DamageMod = 0.75f;
                            break;
                        case 18: //falcon
                            EffectString = "Has a chance to increase\nyour speed for a little while";
                            UpperChanceBounds = 8;
                            TargetsSelf = true;
                            SpeedMod = 1.5f;
                            EffectLength = 3;
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse\nyour enemy";
                            UpperChanceBounds = 15;
                            Confuse = true;
                            EffectLength = 2;
                            break;
                        case 20: //raven
                            EffectString = "Has a chance to blind\nyour enemy, reducing \ntheir accuracy";
                            UpperChanceBounds = 13;
                            AccuracyMod = 0.1f;
                            EffectLength = 3;
                            break;
                    }
                    break;
                case "Armor":
                    switch (index)
                    {
                        case 0: //sheep
                            EffectString = "Passively gives you more defense";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.Defense += (int)(1.5f * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.1f));
                            }
                            break;
                        case 1: //wolf
                            EffectString = "Has a chance to give you more \ndamage after being hit";
                            UpperChanceBounds = 8;
                            TargetsSelf = true;
                            DamageMod = 1.5f;
                            EffectLength = 2;
                            break;
                        case 2: //bear
                            EffectString = "Passively gives you more max health";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.MaxHealth += (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.1f));
                            }
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to retaliate \nwhen attacked, dealing damage\nand healing you";
                            UpperChanceBounds = 7;
                            TargetsBoth = true;
                            if (characterCombat != null)
                            {
                                int localDamage;
                                localDamage = (int)(characterCombat.Damage * 0.25f);
                                Damage = localDamage;
                                Heal = (int)(localDamage * 0.5f);
                            }
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to posion the \nenemy when attacked";
                            UpperChanceBounds = 8;
                            Damage = (int)(2.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.15f));
                            EffectLength = 4;
                            break;
                        case 5: //slime eater
                            EffectString = "Has a chance to give you a \nshield after being hit";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Shield = 3 * stone.Level + GameWorld.Instance.RandomInt(1, 3) + 1;
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to retaliate with a \ntentacle when hit, dealing damage \nto your attacker";
                            Retaliate = true;
                            UpperChanceBounds = 2;
                            Damage = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 5)) * 0.2f));
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your enemy \ndown when attacked";
                            UpperChanceBounds = 10;
                            SpeedMod = 0.8f;
                            EffectLength = 2;
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate \nsome health when attacked";
                            UpperChanceBounds = 15;
                            TargetsSelf = true;
                            Heal = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.2f));
                            break;
                        case 9: //mummy
                            EffectString = "Gives you immunity to curses";
                            TargetsSelf = true;
                            AccuracyMod = 1000000;
                            EffectLength = 999;
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to block the \nnext hit after being attacked";
                            UpperChanceBounds = 5;
                            TargetsSelf = true;
                            Shield = damageDealt + 1;
                            break;
                        case 11: //banshee
                            EffectString = "Grants immunity to all stuns";
                            TargetsSelf = true;
                            StunImmunity = true;
                            break;
                        case 12: //bucket man
                            EffectString = "Has a chance to give yourself a shield \nafter being hit";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Shield = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.3f)) + 1;
                            break;
                        case 13: //defender
                            EffectString = "Strengthens your defense after being hit";
                            TargetsSelf = true;
                            DamageAbs = (int)(1.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 3)) * 0.6f));
                            EffectLength = 1;
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase your \ndodge rating after being hit";
                            //UpperChanceBounds = 7;
                            AccuracyMod = 0.6f;
                            EffectLength = 3;
                            break;
                        case 15: //fire golem
                            EffectString = "Passively reduces damage taken";
                            TargetsSelf = true;
                            DamageReduction = 0.8f;
                            EffectLength = 999;
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your enemy \non fire when attacked";
                            UpperChanceBounds = 6;
                            Damage = (int)(3.5 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.25f));
                            EffectLength = 2;
                            break;
                        case 17: //ash zombie
                            EffectString = "Passively increases your attack speed";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.AttackSpeed *= (int)1.25f;
                            }
                            break;
                        case 18: //falcon
                            EffectString = "Passively increases your\ndodge chance";
                            AccuracyMod = 0.7f;
                            EffectLength = 999;
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse \nyour enemy when attacked";
                            UpperChanceBounds = 8;
                            Confuse = true;
                            EffectLength = 2;
                            break;
                        case 20: //raven
                            EffectString = "Has a chance to blind the enemy \nwhen hit";
                            UpperChanceBounds = 20;
                            AccuracyMod = 0.1f;
                            EffectLength = 3;
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
                        case 1: //wolf
                            EffectString = "Activate your killer instincts,\nincreasing your damage";
                            TargetsSelf = true;
                            DamageMod = 1.5f;
                            EffectLength = 3;
                            Cooldown = 6;
                            break;
                        case 2: //bear
                            EffectString = "Maul your enemy causing them\nto bleed";
                            Damage = (int)(3 * ((stone.Level + GameWorld.Instance.RandomInt(1, 4)) * 0.33f));
                            EffectLength = 3;
                            Cooldown = 5;
                            break;
                        case 3: //plant eater
                            EffectString = "Steal health from the enemy";
                            TargetsBoth = true;
                            if (characterCombat != null)
                            {
                                int localDamage;
                                localDamage = (int)(characterCombat.Damage * 0.75f);
                                Damage = localDamage;
                                Heal = (int)(localDamage * 0.5f);
                                Cooldown = 5;
                            }
                            break;
                        case 4: //insect soldier
                            EffectString = "Poison your enemy";
                            Damage = (int)(2.75 * ((stone.Level + GameWorld.Instance.RandomInt(2, 4)) * 0.25f));
                            EffectLength = 5;
                            Cooldown = 5;
                            break;
                        case 5: //slime eater
                            EffectString = "Cover yourself in a thick \nlayer of slime, reducing \ndamage you take";
                            TargetsSelf = true;
                            DamageAbs = (int)(2 * ((stone.Level + GameWorld.Instance.RandomInt(2, 3)) * 0.4f));
                            EffectLength = 4;
                            Cooldown = 5;
                            break;
                        case 6: //tentacle
                            EffectString = "Wrap tentacles around your \nenemy crushing them \nfor a few rounds";
                            Damage = (int)(3.25 * ((stone.Level + GameWorld.Instance.RandomInt(2, 6)) * 0.3f));
                            EffectLength = 4;
                            Cooldown = 6;
                            break;
                        case 7: //frog
                            EffectString = "Shoot mucus at you enemy \nslowing them down";
                            SpeedMod = 0.6f;
                            EffectLength = 3;
                            Cooldown = 6;
                            break;
                        case 8: //fish
                            EffectString = "Regenerate health for a few round";
                            TargetsSelf = true;
                            Heal = (int)(1.25 * ((stone.Level + GameWorld.Instance.RandomInt(3, 6)) * 0.7f));
                            EffectLength = 3;
                            Cooldown = 8;
                            break;
                        case 9: //mummy
                            EffectString = "Curses your enemy, \nlowering their accuracy";
                            AccuracyMod = 0.6f;
                            EffectLength = 5;
                            Cooldown = 7;
                            break;
                        case 10: //vampire
                            EffectString = "Creates a bloodshield blocking \ndamage equal to 1/4 of your missing health";
                            TargetsSelf = true;
                            Shield = 1; 
                            if (characterCombat != null)
                            {
                                Shield = (int)((characterCombat.MaxHealth - characterCombat.CurrentHealth) * 0.25);
                            }
                            Cooldown = 10;
                            break;
                        case 11: //banshee
                            EffectString = "Paralyse your enemy";
                            Stun = true;
                            EffectLength = 2;
                            Cooldown = 5;
                            break;
                        case 12: //bucket man
                            EffectString = "A powerful attack that \nis difficult to land";
                            TargetsBoth = true;
                            UpperChanceBounds = 10;
                            if (characterCombat != null)
                            {
                                Damage = (characterCombat.Damage + characterCombat.DamageTypes[3]) * 15;
                            }
                            Cooldown = 0;
                            break;
                        case 13: //defender
                            EffectString = "Causes you to enter a defensive \nstance, increasing your defences \nbut lowers your damage";
                            TargetsSelf = true;
                            DamageMod = 0.25f;
                            DamageReduction = 0.3f;
                            EffectLength = 5;
                            Cooldown = 9;
                            break;
                        case 14: //sentry
                            EffectString = "Scans your enemy, adding \ntheir info to your Log";
                            //scancode
                            Cooldown = 99;
                            break;
                        case 15: //fire golem
                            EffectString = "Attacks your enemy with \na critical attack";
                            Damage = damageDealt * 2;
                            Cooldown = 4;
                            break;
                        case 16: //infernal golem
                            EffectString = "Sets your enemy ablaze";
                            Damage = (int)(4.25 * ((stone.Level + GameWorld.Instance.RandomInt(2, 5)) * 0.35f));
                            EffectLength = 3;
                            Cooldown = 6;
                            break;
                        case 17: //ash zombie
                            EffectString = "Causes your next two attacks to \nstrike twice";
                            TargetsSelf = true;
                            DoubleAttack = true;
                            EffectLength = 2;
                            Cooldown = 7;
                            break;
                        case 18: //falcon
                            EffectString = "Increases your speed for\na few rounds";
                            TargetsSelf = true;
                            SpeedMod = 1.55f;
                            EffectLength = 3;
                            Cooldown = 6;
                            break;
                        case 19: //bat
                            EffectString = "Confuses your enemy with a \nsuper sonic screech";
                            Confuse = true;
                            EffectLength = 2;
                            Cooldown = 5;
                            break;
                        case 20: //raven
                            EffectString = "Pecks out your enemy's eyes, \nlowering their accuracy for \na few rounds";
                            AccuracyMod = 0.1f;
                            EffectLength = 3;
                            Cooldown = 10;
                            break;
                    }
                    break;
            }
        }

        public bool TargetsSelf { get => targetsSelf; set => targetsSelf = value; }
        public bool TargetsBoth { get => targetBoth; set => targetBoth = value; }
        public int Cooldown { get => cooldown; set => cooldown = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Heal { get => heal; set => heal = value; }
        public int EffectLength { get => effectlength; set => effectlength = value; }
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
        public int UpperChanceBounds { get => upperChanceBounds; set => upperChanceBounds = value; }
        public int Index { get => index; set => index = value; }
        public string Type { get => type; set => type = value; }
        public FilledStone Stone { get => stone; set => stone = value; }
        public bool StatBuff { get => statBuff; set => statBuff = value; }
        public bool StunImmunity { get => stunImmunity; set => stunImmunity = value; }
        public bool Retaliate { get => retaliate; set => retaliate = value; }
    }
}
