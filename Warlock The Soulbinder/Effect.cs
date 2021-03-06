﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// class for effects of filledStones
    /// </summary>
    public class Effect
    {
        #region VARIABLES
        private bool targetsSelf;
        private bool targetBoth;
        private bool statBuff;
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
        private bool stun;
        private bool confuse;
        private bool doubleAttack;
        private bool retaliate;
        private bool stunImmunity;
        private bool accuracyImmunity;
        private string effectString;
        private int upperChanceBounds = 1;
        private Texture2D skillIcon = null;

        private int index;
        private string type;
        private FilledStone stone;
        #endregion

        /// <summary>
        /// Constructor for stone effect
        /// </summary>
        /// <param name="index">Monster index</param>
        /// <param name="type">Weapon, armor or skill</param>
        /// <param name="stone">Generates a stone with it</param>
        /// <param name="characterCombat">,stats from character combat</param>
        /// <param name="damageDealt">how much damage dealt</param>
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
                            UpperChanceBounds = 8;
                            Stun = true;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/stun");
                            break;
                        case 1: //wolf
                            EffectString = "Activates your killer instincts,\npassively giving you more damage";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.Damage = (int)Math.Round(characterCombat.Damage * 1.2f);
                            }
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/attackUp");
                            break;
                        case 2: //bear
                            EffectString = "Has a chance to maul the enemy,\ncausing them to bleed";
                            UpperChanceBounds = 8;
                            Damage = (int)Math.Round(3d * (stone.Level + 4));
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to steal nutrients\nfrom the enemy, healing you";
                            UpperChanceBounds = 5;
                            TargetsSelf = true;
                            Heal = (int)Math.Round(damageDealt * 0.25f);
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to poison the enemy";
                            UpperChanceBounds = 6;
                            Damage = (int)Math.Round(1.5 * (stone.Level + 2));
                            EffectLength = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 5: //slime snake
                            EffectString = "Has a chance to lower the\ndamage of your enemy";
                            UpperChanceBounds = 9;
                            DamageMod = 0.75f;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/attackDown");
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to constrict your\nenemy with tentacles, crushing them";
                            UpperChanceBounds = 7;
                            Damage = (int)Math.Round(1.75 * (stone.Level + 4));
                            EffectLength = 4;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your\nenemy's speed";
                            UpperChanceBounds = 5;
                            SpeedMod = 0.7f;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedDown");
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate\nyour health";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Heal = (int)Math.Round(2d * (stone.Level + 3));
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/healing");
                            break;
                        case 9: //mummy
                            EffectString = "Has a chance to curse your\nenemy, lowering their accuracy";
                            UpperChanceBounds = 6;
                            AccuracyMod = 0.5f;
                            EffectLength = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyDown");
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to steal blood from\nyour enemy, healing a percentage\nof the damage you dealt";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Heal = (int)Math.Round(damageDealt * 0.3f);
                            break;
                        case 11: //banshee
                            EffectString = "Has a chance to paralyze your\nenemy, stunning them";
                            UpperChanceBounds = 13;
                            Stun = true;
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/stun");
                            break;
                        case 12: //bucket man
                            EffectString = "Has a chance to deal double damage\non your next attack";
                            UpperChanceBounds = 9;
                            TargetsSelf = true;
                            DamageMod = 2;
                            break;
                        case 13: //defender
                            EffectString = "Has a chance to break your\nenemy's armor";
                            UpperChanceBounds = 6;
                            DamageReduction = 1.5f;
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseDown");
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase\nyour hit rating";
                            UpperChanceBounds = 2;
                            TargetsSelf = true;
                            AccuracyMod = 1.25f;
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyUp");
                            break;
                        case 15: //fire golem
                            EffectString = "Causes you to attack slower,\nbut deal more damage";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.Damage = (int)Math.Round(characterCombat.Damage * 1.5f);
                                characterCombat.AttackSpeed = (int)Math.Round(characterCombat.AttackSpeed * 0.8f);
                            }
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your\nenemy on fire";
                            UpperChanceBounds = 6;
                            Damage = (int)Math.Round(2.5 * (stone.Level + 4));
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
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
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedUp");
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse\nyour enemy";
                            UpperChanceBounds = 10;
                            Confuse = true;
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedDown");
                            break;
                        case 20: //raven
                            EffectString = "Has a chance to blind\nyour enemy, reducing \ntheir accuracy";
                            UpperChanceBounds = 11;
                            AccuracyMod = 0.25f;
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyDown");
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
                                characterCombat.Defense += (int)Math.Round(0.5f * stone.Level + 3);
                            }
                            break;
                        case 1: //wolf
                            EffectString = "Has a chance to give you more \ndamage after being hit";
                            UpperChanceBounds = 8;
                            TargetsSelf = true;
                            DamageMod = 1.5f;
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/attackUp");
                            break;
                        case 2: //bear
                            EffectString = "Passively gives you more max health";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.MaxHealth += (int)Math.Round(3d * (stone.Level + 4));
                            }
                            break;
                        case 3: //plant eater
                            EffectString = "Has a chance to retaliate \nwhen attacked, dealing damage\nand healing you";
                            UpperChanceBounds = 7;
                            TargetsBoth = true;
                            if (characterCombat != null)
                            {
                                int localDamage;
                                localDamage = (int)Math.Round(characterCombat.Damage * 0.25f);
                                Damage = localDamage;
                                Heal = (int)Math.Round(localDamage * 0.5f);
                            }
                            break;
                        case 4: //insect soldier
                            EffectString = "Has a chance to poison the \nenemy when attacked";
                            UpperChanceBounds = 8;
                            Damage = (int)Math.Round(1.75 * (stone.Level + 3));
                            EffectLength = 4;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 5: //slime eater
                            EffectString = "Has a chance to give you a \nshield after being hit";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Shield = 3 * stone.Level + 4;
                            break;
                        case 6: //tentacle
                            EffectString = "Has a chance to retaliate with a \ntentacle when hit, dealing damage \nto your attacker";
                            Retaliate = true;
                            UpperChanceBounds = 2;
                            Damage = (int)Math.Round(2.1 * (stone.Level + 5));
                            break;
                        case 7: //frog
                            EffectString = "Has a chance to slow your enemy \ndown when attacked";
                            UpperChanceBounds = 10;
                            SpeedMod = 0.8f;
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedDown");
                            break;
                        case 8: //fish
                            EffectString = "Has a chance to regenerate \nsome health when attacked";
                            UpperChanceBounds = 15;
                            TargetsSelf = true;
                            Heal = (int)Math.Round(2d * (stone.Level + 3));
                            break;
                        case 9: //mummy
                            EffectString = "Gives you immunity to reduced accuracy";
                            TargetsSelf = true;
                            AccuracyImmunity = true;
                            EffectLength = 3;
                            break;
                        case 10: //vampire
                            EffectString = "Has a chance to block the \nnext hit after being attacked";
                            UpperChanceBounds = 7;
                            TargetsSelf = true;
                            Shield = damageDealt + 1;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseUp");
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
                            Shield = (int)Math.Round(3d * (stone.Level + 3)) + 1;
                            break;
                        case 13: //defender
                            EffectString = "Strengthens your defense after being \nhit";
                            TargetsSelf = true;
                            DamageAbs = (int)Math.Round(1.5 * (stone.Level + 3));
                            EffectLength = 1;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseUp");
                            break;
                        case 14: //sentry
                            EffectString = "Has a chance to increase your \ndodge rating after being hit";
                            UpperChanceBounds = 7;
                            AccuracyMod = 0.6f;
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedUp");
                            break;
                        case 15: //fire golem
                            EffectString = "Reduces damage taken after being hit";
                            TargetsSelf = true;
                            DamageReduction = 0.75f;
                            EffectLength = 1;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseUp");
                            break;
                        case 16: //infernal golem
                            EffectString = "Has a chance to set your enemy \non fire when attacked";
                            UpperChanceBounds = 7;
                            Damage = (int)Math.Round(2.5 * (stone.Level + 4));
                            EffectLength = 1;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 17: //ash zombie
                            EffectString = "Passively increases your attack speed";
                            StatBuff = true;
                            if (characterCombat != null)
                            {
                                characterCombat.AttackSpeed = (int)Math.Round(characterCombat.AttackSpeed * 1.25f);
                            }
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedUp");
                            break;
                        case 18: //falcon
                            EffectString = "Passively increases your\ndodge chance";
                            AccuracyMod = 0.7f;
                            EffectLength = 1;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedDown");
                            break;
                        case 19: //bat
                            EffectString = "Has a chance to confuse \nyour enemy when attacked";
                            UpperChanceBounds = 8;
                            Confuse = true;
                            EffectLength = 2;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/confusion");
                            break;
                        case 20: //raven
                            EffectString = "Has a chance to blind the enemy \nwhen hit";
                            UpperChanceBounds = 20;
                            AccuracyMod = 0.25f;
                            EffectLength = 3;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyDown");
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
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/stun");
                            break;
                        case 1: //wolf
                            EffectString = "Activate your killer instincts,\nincreasing your damage";
                            TargetsSelf = true;
                            DamageMod = 1.5f;
                            EffectLength = 3;
                            Cooldown = 6;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/attackUp");
                            break;
                        case 2: //bear
                            EffectString = "Maul your enemy causing them\nto bleed";
                            Damage = (int)Math.Round(3d * (stone.Level + 4));
                            EffectLength = 3;
                            Cooldown = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 3: //plant eater
                            EffectString = "Steal health from the enemy";
                            TargetsBoth = true;
                            if (characterCombat != null)
                            {
                                int localDamage;
                                localDamage = (int)Math.Round(characterCombat.Damage * 0.75f);
                                Damage = localDamage;
                                Heal = (int)Math.Round(localDamage * 0.5f);
                                Cooldown = 5;
                            }
                            Cooldown = 3;
                            break;
                        case 4: //insect soldier
                            EffectString = "Poison your enemy";
                            Damage = (int)Math.Round(2d * (stone.Level + 4));
                            EffectLength = 5;
                            Cooldown = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 5: //slime snake
                            EffectString = "Cover yourself in a thick \nlayer of slime, reducing \ndamage you take";
                            TargetsSelf = true;
                            DamageAbs = (int)Math.Round(2d * (stone.Level + 3));
                            EffectLength = 4;
                            Cooldown = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseUp");
                            break;
                        case 6: //tentacle
                            EffectString = "Wrap tentacles around your \nenemy crushing them \nfor a few rounds";
                            Damage = (int)Math.Round(2.5 * (stone.Level + 6));
                            EffectLength = 4;
                            Cooldown = 6;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 7: //frog
                            EffectString = "Shoot mucus at you enemy \nslowing them down";
                            SpeedMod = 0.6f;
                            EffectLength = 2;
                            Cooldown = 8;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedDown");
                            break;
                        case 8: //fish
                            EffectString = "Regenerate health for a few round";
                            TargetsSelf = true;
                            Heal = (int)Math.Round(1.25 * (stone.Level + 6));
                            EffectLength = 3;
                            Cooldown = 8;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/healing");
                            break;
                        case 9: //mummy
                            EffectString = "Curses your enemy, \nlowering their accuracy";
                            AccuracyMod = 0.5f;
                            EffectLength = 5;
                            Cooldown = 7;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyDown");
                            break;
                        case 10: //vampire
                            EffectString = "Creates a bloodshield blocking \ndamage equal to 1/4 of your missing health";
                            TargetsSelf = true;
                            Shield = 1; 
                            if (characterCombat != null)
                            {
                                Shield = (int)Math.Round((characterCombat.MaxHealth - characterCombat.CurrentHealth) * 0.25);
                            }
                            Cooldown = 10;
                            break;
                        case 11: //bansheex
                            EffectString = "Paralyse your enemy";
                            Stun = true;
                            EffectLength = 2;
                            Cooldown = 7;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/stun");
                            break;
                        case 12: //bucket man
                            EffectString = "A powerful attack that \nis difficult to land";
                            TargetsBoth = true;
                            UpperChanceBounds = 9;
                            Damage = 1;
                            if (characterCombat != null)
                            {
                                Damage = (characterCombat.Damage + characterCombat.DamageTypes[3]) * 20;
                            }
                            Cooldown = 0;
                            break;
                        case 13: //defender
                            EffectString = "Causes you to enter a defensive \nstance, increasing your defences \nbut lowers your damage";
                            TargetsSelf = true;
                            DamageMod = 0.25f;
                            DamageReduction = 0.4f;
                            EffectLength = 5;
                            Cooldown = 11;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/defenseUp");
                            break;
                        case 14: //sentry
                            EffectString = "Scans your enemy, adding \ntheir info to your Log";
                            Cooldown = 99;
                            break;
                        case 15: //fire golem
                            EffectString = "Attacks your enemy with \na critical attack";
                            Damage = 1;
                            if (characterCombat != null)
                            {
                                int tempTotalDamage = characterCombat.Damage;                                
                                for (int i = 0; i < characterCombat.DamageTypes.Count; i++)
                                {
                                    tempTotalDamage += characterCombat.DamageTypes[i];
                                }
                                Damage = tempTotalDamage * 2;
                            }
                            Cooldown = 4;
                            break;
                        case 16: //infernal golem
                            EffectString = "Sets your enemy ablaze";
                            Damage = (int)Math.Round(3.25 * (stone.Level + 5));
                            EffectLength = 3;
                            Cooldown = 6;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/DOT");
                            break;
                        case 17: //ash zombie
                            EffectString = "Causes your attack on the \nnext round to strike twice";
                            TargetsSelf = true;
                            DoubleAttack = true;
                            EffectLength = 2;
                            Cooldown = 7;
                            break;
                        case 18: //falcon
                            EffectString = "Increases your speed for\na few rounds";
                            TargetsSelf = true;
                            SpeedMod = 1.75f;
                            EffectLength = 4;
                            Cooldown = 4;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/speedUp");
                            break;
                        case 19: //bat
                            EffectString = "Confuses your enemy with a \nsuper sonic screech";
                            Confuse = true;
                            EffectLength = 2;
                            Cooldown = 5;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/confusion");
                            break;
                        case 20: //raven
                            EffectString = "Pecks out your enemy's eyes, \nlowering their accuracy for \na few rounds";
                            AccuracyMod = 0.25f;
                            EffectLength = 3;
                            Cooldown = 10;
                            SkillIcon = GameWorld.Instance.Content.Load<Texture2D>("Status/accuracyDown");
                            break;
                    }
                    break;
            }
        }

        #region PROPERTIES
        /// <summary>
        /// Whether the effect targets the caster or their enemy
        /// </summary>
        public bool TargetsSelf { get => targetsSelf; set => targetsSelf = value; }
        /// <summary>
        /// Whether the effect targets both characters or not
        /// </summary>
        public bool TargetsBoth { get => targetBoth; set => targetBoth = value; }
        /// <summary>
        /// An integer value that depicts whether or not the effect can be used. 
        /// Needs to be 0 or less to be activatable
        /// </summary>
        public int Cooldown { get => cooldown; set => cooldown = value; }
        /// <summary>
        /// The amount of damage the effect does
        /// </summary>
        public int Damage { get => damage; set => damage = value; }
        /// <summary>
        /// How much health the effect restores
        /// </summary>
        public int Heal { get => heal; set => heal = value; }
        /// <summary>
        /// How many turns the effect is active
        /// </summary>
        public int EffectLength { get => effectlength; set => effectlength = value; }
        /// <summary>
        /// Percentile damage reduction. 
        /// Acts as a damage modifier: 1 is 0% reduction, 0 is 100% reduction
        /// </summary>
        public float DamageReduction { get => damageReduction; set => damageReduction = value; }
        /// <summary>
        /// Reduces damage by a flat amount
        /// </summary>
        public int DamageAbs { get => damageAbs; set => damageAbs = value; }
        /// <summary>
        /// Modifies the character's speed. 
        /// Less than 1 is slower, more than 1 is faster
        /// </summary>
        public float SpeedMod { get => speedMod; set => speedMod = value; }
        /// <summary>
        /// Modifies the character's chance to hit. 
        /// Base hit chance is 100% at 1
        /// </summary>
        public float AccuracyMod { get => accuracyMod; set => accuracyMod = value; }
        /// <summary>
        /// Modifies the character's damage. 
        /// Less than 1 is less damage, more than 1 is more damage
        /// </summary>
        public float DamageMod { get => damageMod; set => damageMod = value; }
        /// <summary>
        /// Damage taken by the character is first subtracted from shield, 
        /// before it affects the character's health
        /// </summary>
        public int Shield { get => shield; set => shield = value; }
        /// <summary>
        /// If a character is stunned, they miss a turn
        /// </summary>
        public bool Stun { get => stun; set => stun = value; }
        /// <summary>
        /// Confused characters has a 50% to damage themselves,
        /// 25% chance to miss, and 25% chance to hit (before AccuracyMod)
        /// </summary>
        public bool Confuse { get => confuse; set => confuse = value; }
        /// <summary>
        /// Whether the effect causes the character to attack once more on their next attack
        /// </summary>
        public bool DoubleAttack { get => doubleAttack; set => doubleAttack = value; }
        /// <summary>
        /// A string describing the effect's gameplay effect
        /// </summary>
        public string EffectString { get => effectString; set => effectString = value; }
        /// <summary>
        /// The upperbounds for the effect to take effect
        /// </summary>
        public int UpperChanceBounds { get => upperChanceBounds; set => upperChanceBounds = value; }
        /// <summary>
        /// Integer to indetify effects
        /// </summary>
        public int Index { get => index; set => index = value; }
        /// <summary>
        /// Whether the effect is Weapon, Armor or skill
        /// </summary>
        public string Type { get => type; set => type = value; }
        /// <summary>
        /// The FilledStone the effect is attached to
        /// </summary>
        public FilledStone Stone { get => stone; set => stone = value; }
        /// <summary>
        /// Effects with StatBuff == true applies their effect when equipped
        /// </summary>
        public bool StatBuff { get => statBuff; set => statBuff = value; }
        /// <summary>
        /// Effects with StunImmunity == true sets the Stun property to false
        /// </summary>
        public bool StunImmunity { get => stunImmunity; set => stunImmunity = value; }
        /// <summary>
        /// Whether the effect should take effect immediatly after the character is hit
        /// </summary>
        public bool Retaliate { get => retaliate; set => retaliate = value; }
        /// <summary>
        /// Set the character's accuracyMod to 1
        /// </summary>
        public bool AccuracyImmunity { get => accuracyImmunity; set => accuracyImmunity = value; }
        public Texture2D SkillIcon { get => skillIcon; set => skillIcon = value; }
        #endregion
    }
}