using UnityEngine;

namespace Player.Abilities
{
    public abstract class AAbility : MonoBehaviour
    {
        [SerializeField] private int _value;
        protected int Value => _value;
    }
}