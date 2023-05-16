using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAS
{
    [System.Serializable]
    public class Ability
    {
        public string Name => _name;
        public string Description => _description;
        public int Cost
        {
            get => _cost;
            set => _cost = Mathf.Max(0,value);
        }

        public float Cooldown
        {
            get => _cooldown;
            set => _cooldown = Mathf.Max(0f, value);
        }

        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private int _cost;
        [SerializeField] private float _cooldown;

        public Ability()
        {
            _name = "New Ability";
            _description = "New Ability Description";
            _cost = 0;
            _cooldown = 0f;
        }
    }
}
