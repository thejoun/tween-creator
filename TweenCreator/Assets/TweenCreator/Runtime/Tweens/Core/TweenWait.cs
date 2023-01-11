using TweenCreator;
using TypeSwitcher;
using UnityEngine;

namespace Lichtcore.Tweening
{
    [TypeCategory(TweenCategory.Core)]
    public class TweenWait : TweenPlayable
    {
        [SerializeField] private float duration;

        public override float Duration => duration;

        public override void PlayForward()
        {
            // do nothing
        }

        public override void PlayBackwards()
        {
            // do nothing
        }

        public override void Rewind()
        {
            // do nothing
        }
    }
}