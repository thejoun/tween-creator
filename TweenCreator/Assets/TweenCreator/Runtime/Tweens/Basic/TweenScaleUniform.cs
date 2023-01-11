using DG.Tweening;
using TweenCreator;
using TypeSwitcher;
using UnityEngine;

namespace Lichtcore.Tweening
{
    [TypeCategory(TweenCategory.Basic)]
    public class TweenScaleUniform : TweenCustomPlayable
    {
        [Header("Scale Uniform")] 
        [SerializeField] private Transform tr;
        [SerializeField] private float target = 1f;
        [SerializeField] private float origin = 1f;
        
        private Vector3 m_savedState;

        public override void PlayForward()
        {
            PlaySingleTween(tr.DOScale(target, duration));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(tr.DOScale(origin, duration));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            tr.localScale = Vector3.one * origin;
        }

        public override void SavePreviewState()
        {
            m_savedState = tr.localScale;
        }

        public override void RestorePreviewState()
        {
            tr.localScale = m_savedState;
        }
    }
}