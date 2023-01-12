using DG.Tweening;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.Basic)]
    public class TweenMove : TweenCustomPlayable
    {
        [Header("Move")]
        [SerializeField] private Transform tr;
        [SerializeField] private Vector3 target;
        [SerializeField] private Vector3 origin;

        private Vector3 m_savedState;
        
        public override void PlayForward()
        {
            PlaySingleTween(tr.DOLocalMove(target, duration), true);
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(tr.DOLocalMove(origin, duration), false);
        }

        public override void Rewind()
        {
            base.Rewind();
            
            tr.localPosition = origin;
        }
        
        public override void SavePreviewState()
        {
            m_savedState = tr.localPosition;
        }

        public override void RestorePreviewState()
        {
            tr.localPosition = m_savedState;
        }
    }
}