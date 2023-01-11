using UnityEngine;

namespace TweenCreator
{
    [CreateAssetMenu(fileName = "Curve", menuName = "Tween Creator/Curve")]
    public class TweenCreatorCurve : ScriptableObject
    {
        [SerializeField] private AnimationCurve curve;

        public AnimationCurve Curve => curve;

        private void Reset()
        {
            curve ??= new AnimationCurve();
            
            curve.keys = new[]
            {
                new Keyframe(0f, 0f),
                new Keyframe(1f, 1f)
            };
        }
    }
}