using System.Collections;
using System.Collections.Generic;
using GAS.Skills;
using UnityEditor;
using UnityEngine;


namespace GAS
{
    [System.Serializable]
    public abstract class Talent
    {
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public int LevelRequirement
        {
            get => _levelRequirement;
            set => _levelRequirement = value;
        }
        
        public Ability ModifiedAbility
        {
            get => _modifiedAbility;
            set => _modifiedAbility = value;
        }

        [SerializeField] protected string _name;
        [SerializeField] protected string _description;
        [SerializeField] protected int _levelRequirement;
        [SerializeField] protected Ability _modifiedAbility;

        public abstract void ApplyModifier(Ability hero);

        public virtual void DrawCustomProperties(Hero hero)
        {
            Name = EditorGUILayout.TextField("Name", _name);
            Description = EditorGUILayout.TextField("Description", _description);
            LevelRequirement = EditorGUILayout.IntField("Level Requirement", _levelRequirement);
            
            int selectedIndex = hero.Abilities.IndexOf(_modifiedAbility);
            selectedIndex = EditorGUILayout.Popup("Modified Ability", selectedIndex, GetAbilityNames(hero));
            _modifiedAbility = selectedIndex >= 0 ? hero.Abilities[selectedIndex] : null;
        }
        
        private string[] GetAbilityNames(Hero hero)
        {
            var abilityNames = new string[hero.Abilities.Count];
            for (var i = 0; i < hero.Abilities.Count; i++)
            {
                if(hero.Abilities[i] != null)
                    abilityNames[i] = hero.Abilities[i].Name;
            }
            return abilityNames;
        }
    }
    
    public abstract class AbilityTalent : Talent
    {
        public Ability Ability
        {
            get { return _ability; }
            set { _ability = value; }
        }
        
        [SerializeField] protected Ability _ability;
    }
    
    public class RangeTalent : Talent
    {
        public float RangeModifier
        {
            get { return _rangeModifier; }
            set { _rangeModifier = value; }
        }
        
        [SerializeField] private float _rangeModifier;

        public override void ApplyModifier(Ability skill)
        {
            if(_modifiedAbility != null)
                _modifiedAbility.Range *= _rangeModifier;
        }

        public override void DrawCustomProperties(Hero hero)
        {
            base.DrawCustomProperties(hero);
            // Display custom properties for RangeTalent
            _rangeModifier = EditorGUILayout.FloatField("Range Modifier", _rangeModifier);
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

        public override void ApplyModifier(Ability skill)
        {
            if(_modifiedAbility != null)
                _modifiedAbility.Cost *= _costModifier;
        }

        public override void DrawCustomProperties(Hero hero)
        {
            base.DrawCustomProperties(hero);
            _costModifier = EditorGUILayout.FloatField("Cost Modifier", _costModifier);
        }
    }

    public class DamageVFXTalent : Talent
    {
        private float _damageModifier;
        
        public float DamageModifier
        {
            get { return _damageModifier; }
            set { _damageModifier = value; }
        }
        
        [SerializeField] private GameObject _damageVFX;
        
        public override void ApplyModifier(Ability skill)
        {
            if (_modifiedAbility == null || _modifiedAbility is not HealAndDamage healDamageSKill) return;
            
            healDamageSKill.Damage *= _damageModifier;
            healDamageSKill.VfxPrefab = _damageVFX;
        }

        public override void DrawCustomProperties(Hero hero)
        {
            base.DrawCustomProperties(hero);
            _damageModifier = EditorGUILayout.FloatField("Damage Modifier", _damageModifier);
            _damageVFX = (GameObject) EditorGUILayout.ObjectField("Custom VFX", _damageVFX, typeof(GameObject), false);
        }
    }
    
}
