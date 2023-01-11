using DG.Tweening;
using TweenCreator.Extensions;
using UnityEngine;

namespace TweenCreator
{
    public abstract class TweenCustomPlayable : TweenPlayable
    {
        private enum EaseType
        {
            DOTween,
            AnimationCurve
        }
        
        // todo move to TweenPlayer
        // [SerializeField] private bool rewindOnAwake;
        
        // todo hide fields
        [SerializeField] private EaseType easeType;
        [SerializeField] protected Ease ease = Ease.OutQuad;
        [SerializeField] private TweenCreatorCurve animationCurveEase;
        
        [SerializeField] protected float duration = 0.2f;
        [SerializeField] protected float delay;

        public override float Duration => duration + delay;

        public override bool IsPreviewable => true;

        protected virtual bool IsRelative => false;

        private bool IsDoTweenEaseType => easeType == EaseType.DOTween;
        private bool IsAnimationCurveEaseType => easeType == EaseType.AnimationCurve;
        
        // protected virtual void Awake()
        // {
        //     if (rewindOnAwake)
        //     {
        //         Rewind();
        //     }
        // }

        protected void PlaySingleTween(Tween tween, bool isForward = true)
        {
            Kill();
			
            PlayTween(tween, isForward);
        }

        protected void PlayTween(Tween tween, bool isForward = true)
        {
            Prepare(tween);

            if (IsDoTweenEaseType)
            {
                tween.SetEase(ease);
            }
            else if (IsAnimationCurveEaseType)
            {
                tween.SetEase(animationCurveEase.Curve.EvaluateTween);
            }
            
            tween.SetDelay(delay);
            tween.SetRelative(IsRelative);
            
            currentTweens.Add(tween);
        }

        
    }
}