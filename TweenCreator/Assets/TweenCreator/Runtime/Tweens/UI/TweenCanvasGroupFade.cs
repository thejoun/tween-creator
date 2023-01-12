using DG.Tweening;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.UI)]
    public class TweenCanvasGroupFade : TweenCustomPlayable
    {
        [Header("Canvas Group Fade")] 
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] [Range(0f, 1f)] private float target = 1f;
        [SerializeField] [Range(0f, 1f)] private float origin = 0f;
        
        private float m_savedState;
        
        public override void PlayForward()
        {
            PlaySingleTween(canvasGroup.DOFade(target, duration));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(canvasGroup.DOFade(origin, duration));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            canvasGroup.alpha = origin;
        }

        public override void SavePreviewState()
        {
            m_savedState = canvasGroup.alpha;
        }

        public override void RestorePreviewState()
        {
            canvasGroup.alpha = m_savedState;
        }
    }
}