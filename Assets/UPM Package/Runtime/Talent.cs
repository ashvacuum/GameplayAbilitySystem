using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAS
{
    
    public abstract class Talent
    {
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int LevelRequirement
        {
            get { return _levelRequirement; }
            set { _levelRequirement = value; }
        }

        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected int _levelRequirement;

        public abstract void ApplyModifier(Ability ability);
    }
    
    public class RangeTalent : Talent
    {
        public float RangeModifier
        {
            get { return _rangeModifier; }
            set { _rangeModifier = value; }
        }
        
        [SerializeField] private float _rangeModifier;

        public override void ApplyModifier(Ability ability)
        {
            ability.Range += _rangeModifier;
        }
    }
    
    public class CostTalent : Talent
    {
        public float CostModifier
        {
            get { return _costModifier; }
            set { _costModifier = value; }
        }
        
        [SerializeField] private float _costModifier;

        public override void ApplyModifier(Ability ability)
        {
            ability.Cost *= _costModifier;
        }
    }
    
}
