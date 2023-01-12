using System.Linq;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.Core)]
    public class TweenReference : TweenPlayable
    {
        [Header("Tween Reference")]
        [SerializeField] protected TweenPlayable tweenPlayable;
        [SerializeField] protected TweenPlayMode playMode;

        public override bool IsPreviewable => true;

        protected override void Reset()
        {
            base.Reset();
            
            FindPlayableInHierarchy();
        }

        public override void PlayForward()
        {
            tweenPlayable.Play(playMode);
        }

        public override void PlayBackwards()
        {
            tweenPlayable.Play(Inverted(playMode));
        }

        public override void Rewind()
        {
            tweenPlayable.Rewind();
        }

        public override void Kill()
        {
            tweenPlayable.Kill();
        }

        public override void SavePreviewState()
        {
            tweenPlayable.SavePreviewState();
        }

        public override void RestorePreviewState()
        {
            tweenPlayable.RestorePreviewState();
        }

        // [Button]
        private void FindPlayableInHierarchy()
        {
            tweenPlayable = FindPlayables().FirstOrDefault();
        }
    }
}