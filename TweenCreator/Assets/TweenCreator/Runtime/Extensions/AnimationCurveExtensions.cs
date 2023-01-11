using UnityEngine;

namespace TweenCreator.Extensions
{
    public static class AnimationCurveExtensions
    {
        /// <summary>
        /// Evaluation method meant as a DOTween EaseFunction
        /// </summary>
        public static float EvaluateTween(this AnimationCurve curve, float time, float duration, float overshoot, float period)
        {
            return curve.EvaluateProgress(time / duration);
        }

        public static float EvaluateProgress(this AnimationCurve curve, float progress)
        {
            var lastKey = curve[curve.length - 1];
            var curveDuration = lastKey.time;
            
            return curve.Evaluate(progress * curveDuration);
        }
    }
}