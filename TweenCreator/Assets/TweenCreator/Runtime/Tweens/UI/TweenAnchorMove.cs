using DG.Tweening;
using TweenCreator;
using TypeSwitcher;
using UnityEngine;

namespace Lichtcore.Tweening
{
    [TypeCategory(TweenCategory.UI)]
    public class TweenAnchorMove : TweenCustomPlayable
    {
        [Header("Anchor Move")] 
        [SerializeField] private RectTransform tr;
        [SerializeField] private Vector2 target;
        [SerializeField] private Vector2 origin;

        private Vector2 m_savedState;
        
        public override void PlayForward()
        {
            PlaySingleTween(tr.DOAnchorPos(target, duration));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(tr.DOAnchorPos(origin, duration));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            tr.anchoredPosition = origin;
        }

        public override void SavePreviewState()
        {
            m_savedState = tr.anchoredPosition;
        }

        public override void RestorePreviewState()
        {
            tr.anchoredPosition = m_savedState;
        }
    }
}