using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAS
{
    public abstract class Ability : ScriptableObject
    {
        public string Name => _name;
        public string Description => _description;
        
        /// <summary>
        /// Skill cost for mana
        /// </summary>
        public float Cost
        {   
            get => _cost;
            set => _cost = Mathf.Max(0,value);
        }

        /// <summary>
        /// skill cooldown
        /// </summary>
        public float Cooldown
        {
            get => _cooldown;
            set => _cooldown = Mathf.Max(0f, value);
        }
        
        /// <summary>
        /// skill range
        /// </summary>
        public float Range
        {
            get => _range;
            set => _range = Mathf.Max(0f, value);
        }

        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected float _cost;
        [SerializeField] protected float _cooldown;
        [SerializeField] protected float _range;

        public Ability()
        {
            _name = "New Ability";
            _description = "New Ability Description";
            _cost = 0;
            _cooldown = 0f;
        }
    }
    
    public abstract class SingleTargetAbility : Ability
    {
        [SerializeField] private GameObject _projectilePrefab;
        
        public virtual void Activate(GameObject caster, GameObject target)
        {
            // activate vfx
        }
    }
    
    public abstract class AoeAbility : Ability
    {
        [SerializeField] private GameObject _projectilePrefab;
        
        public virtual void Activate(GameObject caster, GameObject target)
        {
            //activate vfx
        }
    }
    
    public class HealAndDamage : SingleTargetAbility
    {
        public float Damage
        {
            get => _damage;
            set => _damage = value; // if this is negative this means that this heals
        }
        
        [SerializeField] private float _damage;
        
        public override void Activate(GameObject caster, GameObject target)
        {
            // activate vfx
            // apply damage
        }
    }
}
