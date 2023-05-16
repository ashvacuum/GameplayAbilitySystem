using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GAS
{
    [System.Serializable]
    public abstract class Talent
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public abstract void ApplyModifier(Ability ability);
    }
}
