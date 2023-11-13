using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AbilityView : MonoBehaviour
    {
        [SerializeField] private Image _abilityImage;
        
        public void UpdateImage(Sprite abilitySprite)
        {
            _abilityImage.transform.DOShakeScale(0.1f, Vector3.one, 3,50);
            _abilityImage.sprite = abilitySprite;
        }
    }
}