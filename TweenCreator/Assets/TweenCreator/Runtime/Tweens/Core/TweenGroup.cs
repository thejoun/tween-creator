using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TweenCreator;
using TypeSwitcher;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lichtcore.Tweening
{
    [TypeCategory(TweenCategory.Core)]
    public class TweenGroup : TweenPlayable
    {
        // todo create a TweenPlayer
        // [SerializeField] private bool rewindOnStart;
        // [SerializeField] private bool playOnStart;
        // [SerializeField] private bool rewindOnEnable;
        // [SerializeField] private bool playOnEnable;

        [Header("Group")]
        [SerializeField] private bool dontReverse;
        
        [SerializeField] protected List<TweenPlayable> playables;

        public override bool IsPreviewable => true;

        public override float Duration => GetDuration();
        
        protected List<TweenPlayable> TweenPlayables => playables.Where(p => p).ToList();

        protected override void Reset()
        {
            base.Reset();
            
            playables = new List<TweenPlayable>();
            
            FindPlayablesInHierarchy();
        }
        
        // private void Start()
        // {
        //     if (rewindOnStart)
        //     {
        //         Rewind();
        //     }
        //     
        //     if (playOnStart)
        //     {
        //         DOVirtual.DelayedCall(0f, RewindAndPlayForward).SetUpdate(true);
        //     }
        // }
        
        // private void OnEnable()
        // {
        //     if (rewindOnEnable)
        //     {
        //         Rewind();
        //     }
        //     
        //     if (playOnEnable)
        //     {
        //         DOVirtual.DelayedCall(0f, RewindAndPlayForward).SetUpdate(true);
        //     }
        // }

        public override void PlayForward()
        {
            base.Kill();
            
            PlaySeveral(TweenPlayables, playable => playable.PlayForward());
        }

        public override void PlayBackwards()
        {
            base.Kill();
            
            var playables = dontReverse ? TweenPlayables : GetReversedPlayables();
            
            PlaySeveral(playables, playable => playable.PlayBackwards());
        }

        public override void Rewind()
        {
            base.Kill();
            
            var playables = dontReverse ? TweenPlayables : GetReversedPlayables();

            playables.ForEach(p => p.Rewind());
        }
        
        public override void RewindAndPlayForward()
        {
            base.Kill();
            
            PlaySeveral(TweenPlayables, playable => playable.RewindAndPlayForward());
        }

        public override void Kill()
        {
            TweenPlayables.ForEach(playable => playable.Kill());
            
            base.Kill();
        }

        public override void SavePreviewState()
        {
            TweenPlayables.ForEach(playable => playable.SavePreviewState());
        }

        public override void RestorePreviewState()
        {
            TweenPlayables.ForEach(playable => playable.RestorePreviewState());
        }

        protected virtual float GetDuration()
        {
            var delay = 0f;
            var highestDuration = 0f;
            
            foreach (var playable in TweenPlayables)
            {
                var fullDuration = delay + playable.Duration;
                
                delay += GetDelay(playable);

                highestDuration = Mathf.Max(fullDuration, highestDuration);
            }

            return highestDuration;
        }

        private List<TweenPlayable> GetReversedPlayables()
        {
            var playables = TweenPlayables.ToList();

            playables.Reverse();

            return playables;
        }

        protected virtual void PlaySeveral(IEnumerable<TweenPlayable> playables, Action<TweenPlayable> action)
        {
            var sequence = DOTween.Sequence();

            foreach (var playable in playables)
            {
                sequence.AppendCallback(() => action(playable));

                var delay = GetDelay(playable);

                if (!Mathf.Approximately(delay, 0f))
                {
                    sequence.AppendInterval(delay);
                }
            }
            
            Prepare(sequence);
            
            currentTweens.Add(sequence);
        }

        protected virtual float GetDelay(TweenPlayable playable)
        {
            return playable is TweenWait ? playable.Duration : 0f;
        }
        
        // [Button] [HideIf(nameof(IsPreviewMode))]
        private void FindPlayablesInHierarchy()
        {
            var playables = FindPlayables();

            this.playables = playables;
        }
        
        // [Button] [HideIf(nameof(IsPreviewMode))]
        private void ApplyTimeScaleIgnoreToPlayables()
        {
	        foreach (var playable in playables)
	        {
		        playable.SetIgnoreTimeScale(IgnoreTimescale);

#if UNITY_EDITOR
                EditorUtility.SetDirty(playable);
#endif
	        }
        }
    }
}