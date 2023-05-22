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
    
    public interface IPrefabSpawner
    {
        GameObject VfxPrefab { get; }
    }
    
    public interface IActivateSkill
    {
        void Activate(Hero caster, Hero target);
    }
    
    public abstract class SingleTargetAbility : Ability, IPrefabSpawner, IActivateSkill
    {
        [SerializeField] private GameObject _vfxPrefab;

        public GameObject VfxPrefab => _vfxPrefab;
        public abstract void Activate(Hero caster, Hero target);
    }

    public abstract class AoeAbility : Ability, IPrefabSpawner, IActivateSkill
    {
        [SerializeField] private GameObject _vfxPrefab;
        public GameObject VfxPrefab => _vfxPrefab;
        public abstract void Activate(Hero caster, Hero target);
    }
}
