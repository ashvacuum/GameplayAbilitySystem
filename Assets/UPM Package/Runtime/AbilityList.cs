using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAS.Skills
{
    [CreateAssetMenu(fileName = "New Heal and Damage", menuName = "GAS/Abilities/Heal or Damage")]
    public class HealAndDamage : SingleTargetAbility
    {
        /// <summary>
        /// if negative deals damage, if positive heals
        /// </summary>
        public float Damage
        {
            get => _damage;
            set => _damage = value; 
        }
        
        [SerializeField] private float _damage;
        
        public override void Activate(Hero hero, Hero target)
        {
            // activate vfx
            // apply damage
        }
    }
}
