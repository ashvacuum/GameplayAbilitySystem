using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAS.Skills
{
    public abstract class SingleTargetAbility : Ability, IPrefabSpawner, IActivateSkill
    {
        [Header("Special")]
        [SerializeField] private GameObject _vfxPrefab;

        public GameObject VfxPrefab
        {
            get => _vfxPrefab;
            set => _vfxPrefab = value;
        }

        public event Action OnSkillActivated;

        public virtual void Activate(Hero caster, Hero target)
        {
            OnSkillActivated?.Invoke();
        }
    }
}
