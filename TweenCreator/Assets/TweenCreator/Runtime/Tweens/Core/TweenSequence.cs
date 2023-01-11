using System.Linq;
using TweenCreator;

namespace Lichtcore.Tweening
{
    public class TweenSequence : TweenGroup
    {
        protected override float GetDelay(TweenPlayable playable)
        {
            return playable.Duration;
        }

        protected override float GetDuration()
        {
            var duration = TweenPlayables.Sum(playable => playable.Duration);

            return duration;
        }
    }
}