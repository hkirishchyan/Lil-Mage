using UnityEngine;

namespace Player.Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Player/Ability", order = 0)]
    public class AbilityScriptable : ScriptableObject
    {
        [SerializeField] private AAbility _ability;
        [SerializeField] private Sprite _visual;

        public AAbility Ability => _ability;
        public Sprite Visual => _visual;
    }
}