using DG.Tweening;
using TypeSwitcher;
using UnityEngine;
using UnityEngine.UI;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.UI)]
    public class TweenImageColor : TweenCustomPlayable
    {
        [Header("Image Color")] 
        [SerializeField] private Image image;
        [SerializeField] private Color target = Color.white;
        [SerializeField] private Color origin = Color.white;
    
        private Color m_savedState;
        
        protected override void Reset()
        {
            base.Reset();
            
            if (image)
            {
                target = image.color;
                origin = image.color;
            }
        } 
        
        public override void PlayForward()
        {
            PlaySingleTween(image.DOColor(target, duration));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(image.DOColor(origin, duration));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            image.color = origin;
        }

        public override void SavePreviewState()
        {
            m_savedState = image.color;
        }

        public override void RestorePreviewState()
        {
            image.color = m_savedState;
        }
    }
}
