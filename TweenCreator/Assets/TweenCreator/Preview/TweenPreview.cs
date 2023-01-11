using System.Collections.Generic;
using DG.DOTweenEditor;
using DG.Tweening;

namespace TweenCreator.Preview
{
    public class TweenPreview
    {
        public static void StartPreview(params Tween[] tweens)
        {
            if (tweens == null)
            {
                return;
            }
            
            StartPreview((IEnumerable<Tween>)tweens);
        }

        public static void StartPreview(IEnumerable<Tween> tweens)
        {
            PreparePreview(tweens);

            StartPreview();
        }

        public static void PreparePreview(params Tween[] tweens)
        {
            if (tweens == null)
            {
                return;
            }
            
            PreparePreview((IEnumerable<Tween>)tweens);
        }
        
        public static void PreparePreview(IEnumerable<Tween> tweens)
        {
            foreach (var tween in tweens)
            {
                DOTweenEditorPreview.PrepareTweenForPreview(tween);
            }
        }

        private static void StartPreview()
        {
            DOTweenEditorPreview.Start();
        }

        public static void StopPreview()
        {
            DOTweenEditorPreview.Stop();
        }
    }
}