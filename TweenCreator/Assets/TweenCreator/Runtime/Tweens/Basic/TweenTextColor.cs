using DG.Tweening;
using TMPro;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.Basic)]
    public class TweenTextColor : TweenCustomPlayable
    {
        [Header("Text Color")] 
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color target = Color.white;
        [SerializeField] private Color origin = Color.white;

        private Color m_savedState;
        
        public override void PlayForward()
        {
            PlaySingleTween(text.DOColor(target, duration));
        }

        public override void PlayBackwards()
        {
            PlaySingleTween(text.DOColor(origin, duration));
        }

        public override void Rewind()
        {
            base.Rewind();
            
            text.color = origin;
        }

        public override void SavePreviewState()
        {
            m_savedState = text.color;
        }

        public override void RestorePreviewState()
        {
            text.color = m_savedState;
        }
    }
}