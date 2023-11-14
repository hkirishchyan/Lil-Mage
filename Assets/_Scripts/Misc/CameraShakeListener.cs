using MoreMountains.Feedbacks;
using Player.Abilities.Offensive;
using UnityEngine;

public class CameraShakeListener : MonoBehaviour
{
    [SerializeField] private MMF_Player _mmfPlayer;
    private void OnEnable()
    {
        ProjectileSpell.SpellHit += Shake;
    }

    private void Shake()
    {
        _mmfPlayer.PlayFeedbacks();
    }
    
    private void OnDisable()
    {
        ProjectileSpell.SpellHit -= Shake;
    }
}
