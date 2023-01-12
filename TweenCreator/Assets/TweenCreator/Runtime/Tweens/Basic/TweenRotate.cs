using DG.Tweening;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.Basic)]
    public class TweenRotate : TweenCustomPlayable
    {
        [Header("Rotate")] 
        [SerializeField] private Transform tr;
        [SerializeField] private float target;
        [SerializeField] private float origin;

        private Quaternion m_savedState;
        
        public override void PlayForward()
        {
            PlaySingleTween(tr.DOLocalRotate(Vector3.forward * target, duration, RotateMode.FastBeyond360));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(tr.DOLocalRotate(Vector3.forward * origin, duration, RotateMode.FastBeyond360));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            tr.localRotation = Quaternion.Euler(0f, 0f, origin);
        }

        public override void SavePreviewState()
        {
            m_savedState = tr.localRotation;
        }

        public override void RestorePreviewState()
        {
            tr.localRotation = m_savedState;
        }
    }
}