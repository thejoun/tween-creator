using DG.Tweening;
using TweenCreator;
using TypeSwitcher;
using UnityEngine;

namespace Lichtcore.Tweening
{
    [TypeCategory(TweenCategory.Basic)]
    public class TweenScaleUniformRelative : TweenCustomPlayable
    {
        [Header("Scale Uniform Relative")] 
        [SerializeField] private Transform tr;
        [SerializeField] private float delta;

        protected override bool IsRelative => true;

        private Vector3 m_savedState;

        public override void PlayForward()
        {
            PlayTween(tr.DOScale(delta, duration));
        }

        public override void PlayBackwards()
        {
            PlayTween(tr.DOScale(-delta, duration));
        }

        public override void Rewind()
        {
            // do nothing??
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