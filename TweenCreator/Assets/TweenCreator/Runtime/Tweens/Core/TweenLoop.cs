using System.Linq;
using DG.Tweening;
using TypeSwitcher;
using UnityEngine;

namespace TweenCreator.Tweens
{
    [TypeCategory(TweenCategory.Core)]
    public class TweenLoop : TweenPlayable
    {
        [Header("Tween Loop")]
        [SerializeField] protected TweenPlayable tweenPlayable;
        [SerializeField] private bool playOnEnable;
        [SerializeField] private bool rewindOnDisable;
        [SerializeField] private bool playOnStart;
        [SerializeField] private bool keepPlaying;

        private Sequence m_sequence;

        private bool IsPlaying => m_sequence != null && m_sequence.IsPlaying();

        protected override void Reset()
        {
            base.Reset();
            
            FindPlayablesInHierarchy();
        }

        private void OnEnable()
        {
            if (playOnEnable)
            {
                RewindAndPlayForward();
            }
        }

        private void OnDisable()
        {
            if (rewindOnDisable)
            {
                Rewind();
            }
        }

        private void Start()
        {
            if (playOnStart)
            {
                RewindAndPlayForward();
            }
        }

        public override void RewindAndPlayForward()
        {
            if (keepPlaying && IsPlaying)
            {
                return;
            }
            
            base.RewindAndPlayForward();
        }

        public override void PlayForward()
        {
            if (keepPlaying && IsPlaying)
            {
                return;
            }
            
            m_sequence = DOTween.Sequence();
            m_sequence.AppendCallback(() => tweenPlayable.PlayForward());
            m_sequence.AppendInterval(tweenPlayable.Duration);
            m_sequence.SetLoops(-1);
            m_sequence.Play();
        }

        public override void PlayBackwards()
        {
            Kill();
            
            m_sequence = DOTween.Sequence();
            m_sequence.AppendCallback(() => tweenPlayable.PlayBackwards());
            m_sequence.AppendInterval(tweenPlayable.Duration);
            m_sequence.SetLoops(-1);
            m_sequence.Play();
        }

        public override void Rewind()
        {
            Kill();
            
            // tweenPlayable.Kill();
            tweenPlayable.Rewind();
        }

        public override void Kill()
        {
            base.Kill();
            
            m_sequence?.Kill();
            
            tweenPlayable.Kill();
        }

        // [Button]
        private void FindPlayablesInHierarchy()
        {
            var playables = FindPlayables();
            
            tweenPlayable = playables.FirstOrDefault();
        }
    }
}