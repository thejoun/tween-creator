using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

#if UNITY_EDITOR
using TweenCreator.Preview;
#endif

namespace TweenCreator
{
    public abstract class TweenPlayable : SwitchableTween
    {
	    [SerializeField] private bool ignoreTimescale;

        public bool PreviewMode { get; private set; }
        public bool Preview { get; private set; }
        
        protected readonly List<DG.Tweening.Tween> currentTweens = new List<DG.Tweening.Tween>();

        public virtual float Duration => 0f;

        public virtual bool IsPreviewable => false;
        
        public bool CanEnterPreview => IsPreviewable && !IsPreviewMode;
        public bool IsPreviewMode => IsEditor && PreviewMode;
        
        private static bool IsEditor => !Application.isPlaying;
        
        private bool HasNoDuration => Mathf.Abs(Duration) < Mathf.Epsilon;
        private bool IsPreview => IsPreviewMode && Preview;
        private bool AnyCurrentTween => currentTweens?.Any() ?? false;
        
        protected bool IgnoreTimescale => ignoreTimescale;

        public float ClipDuration => Duration;

        private const string RuntimeButtonsGroup = "RuntimeButtons";
        private const string EditorButtonsGroup = "EditorButtons";

        protected virtual void Reset()
        {
            if (TryGetComponent(out RectTransform rectTr))
            {
                rectTr.sizeDelta = Vector2.zero;
            }
        }

        // [Button("Forward")] [HideIf(nameof(IsEditor))] [HorizontalGroup(RuntimeButtonsGroup)]
        public abstract void PlayForward();

        // [Button("Backward")] [HideIf(nameof(IsEditor))] [HorizontalGroup(RuntimeButtonsGroup)]
        public abstract void PlayBackwards();

        // [Button] [HideIf(nameof(IsEditor))]
        public virtual void RewindAndPlayForward()
        {
            Rewind();
            PlayForward();
        }

        public virtual void Play(bool invert)
        {
            if (invert)
            {
                PlayBackwards();
            }
            else
            {
                PlayForward();
            }
        }
        
        public void Play(TweenPlayMode playMode)
        {
            switch (playMode)
            {
                case TweenPlayMode.PlayForward: 
                    PlayForward();
                    break;
                case TweenPlayMode.PlayBackward: 
                    PlayBackwards();
                    break;
                case TweenPlayMode.RewindAndPlayForward: 
                    RewindAndPlayForward();
                    break;
                case TweenPlayMode.Rewind:
                    Rewind();
                    break;
            }
        }
        
        // [Button] [HideIf(nameof(IsEditor))] [HorizontalGroup(RuntimeButtonsGroup)]
        public virtual void Rewind()
        {
            Kill();
        }

        public virtual void Kill()
        {
            foreach (var tween in currentTweens)
            {
                tween.Kill();
            }
            
            currentTweens.Clear();
        }
        
        public virtual void SavePreviewState()
        {
            // to override, nothing by default
        }

        public virtual void RestorePreviewState()
        {
            // to override, nothing by default
        }

        public void SetIgnoreTimeScale(bool ignoreTimescale)
        {
	        this.ignoreTimescale = ignoreTimescale;
        }
        
        protected void Prepare(DG.Tweening.Tween tween)
        {
	        if (ignoreTimescale)
	        {
		        tween.SetUpdate(true);
	        }
	        
#if UNITY_EDITOR
            if (IsPreviewable && IsEditor)
            {
                TweenPreview.PreparePreview(tween);
            }
#endif
        }

#if UNITY_EDITOR  
        private void StartPreview()
        {
            TweenPreview.StartPreview(currentTweens);
            
            Preview = true;
        }

        private void StopPreview()
        {
            TweenPreview.StopPreview();
            
            Preview = false;
        }
#endif
        
#if UNITY_EDITOR
        public void EnterPreviewMode()
        {
            SavePreviewState();
            
            PreviewMode = true;
        }
        
        public void ExitPreviewMode()
        {
            StopPreview();
            RestorePreviewState();
            
            PreviewMode = false;
        }
#endif
        
#if UNITY_EDITOR
        public void PlayForwardInEditor()
        {
            PlayForward();
            StartPreview();
        }
        
        public void PlayBackwardsInEditor()
        {
            PlayBackwards();
            StartPreview();
        }
        
        public void RewindInEditor()
        {
            Rewind();
        }

        public void RewindAndPlayForwardInEditor()
        {
            RewindInEditor();
            PlayForwardInEditor();
        }
#endif
        
        protected List<TweenPlayable> FindPlayables()
        {
            var playables = new List<TweenPlayable>();

            foreach (Transform child in transform)
            {
                if (child.gameObject.activeInHierarchy
                    && child.TryGetComponent<TweenPlayable>(out var playable))
                {
                    playables.Add(playable);
                }
            }

            return playables;
        }

        protected TweenPlayMode Inverted(TweenPlayMode playMode)
        {
            if (playMode == TweenPlayMode.PlayForward
                || playMode == TweenPlayMode.RewindAndPlayForward)
            {
                return TweenPlayMode.PlayBackward;
            }

            if (playMode == TweenPlayMode.PlayBackward)
            {
                return TweenPlayMode.PlayForward;
            }

            return playMode;
        }
    }
}