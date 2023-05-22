using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAS
{
    /// <summary>
    /// Base class for all abilities
    /// </summary>
    public abstract class Ability : ScriptableObject
    {
        /// <summary>
        /// just refers to the name of the ability, used by talent system to identify which skill to modify
        /// </summary>
        public string Name => _name;
        
        /// <summary>
        /// used byu designers to describe the ability
        /// </summary>
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
        [Header("Base Ability")]
        [Tooltip("Name of the ability")]
        [SerializeField] protected string _name;
        [Tooltip("Description of the ability")]
        [SerializeField] protected string _description;
        [Tooltip("Mana cost of the ability")]
        [SerializeField] protected float _cost;
        [Tooltip("Cooldown of the ability")]
        [SerializeField] protected float _cooldown;
        [Tooltip("Range of the ability")]
        [SerializeField] protected float _range;


        public Ability()
        {
            _name = "New Ability";
            _description = "New Ability Description";
            _cost = 0;
            _cooldown = 0f;
        }
    }
    
    /// <summary>
    /// means that his specific ability will spawn a prefab can be modified by talents
    /// </summary>
    public interface IPrefabSpawner
    {
        GameObject VfxPrefab { get; set; }
    }
    
    
    /// <summary>
    /// Means that this skill can have a target and hero and is activated by the hero, i.e. for when heroes have passive skills
    /// </summary>
    public interface IActivateSkill
    {
        event Action OnSkillActivated;
        void Activate(Hero caster, Hero target);
    }
    
}
