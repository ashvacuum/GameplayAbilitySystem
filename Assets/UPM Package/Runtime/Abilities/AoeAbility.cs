using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAS.Skills
{
    public abstract class AoeAbility : Ability, IPrefabSpawner, IActivateSkill
    {
        [Header("Special")]
        [Tooltip("Radius of the ability")]
        [SerializeField] private float _radius;
        [Tooltip("VFX prefab of the ability")]
        [SerializeField] private GameObject _vfxPrefab;
        
        public GameObject VfxPrefab
        {
            get => _vfxPrefab;
            set => _vfxPrefab = value;
        }
        public float Radius
        {
            get => _radius;
            set => _radius = Mathf.Max(1,value);
        }

        public event Action OnSkillActivated;

        public virtual void Activate(Hero caster, Hero target)
        {
            OnSkillActivated?.Invoke();
        }
    }
}
