using UnityEditor;
using UnityEngine;

namespace TweenCreator.Editor
{
    [CustomEditor(typeof(TweenPlayable), true)]
    public class TweenPlayableEditor : UnityEditor.Editor
    {
        private TweenPlayable _script;

        private Texture2D _forwardIcon;
        private Texture2D _backwardIcon;
        private Texture2D _rewindIcon;
        private Texture2D _replayIcon;

        // todo settings ??
        private const bool ExitPreviewOnLoseSelection = true;
        private const bool DiscardChangesOnExitPreview = true;
        
        private const string ForwardIconFile = "forward.png";
        private const string BackwardIconFile = "backward.png";
        private const string RewindIconFile = "rewind.png";
        private const string ReplayIconFile = "replay.png";

        private readonly string[] _resourcePaths = new[]
        {
            "Packages/com.thejoun.tween-creator/Icons/",
            "Assets/TweenCreator/Icons/",
            "Assets/Plugins/TweenCreator/Icons/"
        };
        
        private void OnEnable()
        {
            _script = (TweenPlayable)target;

            _forwardIcon = LoadAsset<Texture2D>(ForwardIconFile);
            _backwardIcon = LoadAsset<Texture2D>(BackwardIconFile);
            _rewindIcon = LoadAsset<Texture2D>(RewindIconFile);
            _replayIcon = LoadAsset<Texture2D>(ReplayIconFile);
        }

        private void OnDisable()
        {
            if (ExitPreviewOnLoseSelection)
            {
                if (_script.PreviewMode)
                {
                    ExitPreview();
                }
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_script.CanEnterPreview)
            {
                DrawEnterPreview();
            }

            if (_script.IsPreviewMode)
            {
                DrawPreview();
            }
        }

        private void DrawEnterPreview()
        {
            GUILayout.Space(5);

            if (GUILayout.Button("PREVIEW"))
            {
                _script.EnterPreviewMode();
            }
        }

        private void DrawPreview()
        {
            GUILayout.Space(5);

            if (GUILayout.Button("RESET"))
            {
                ExitPreview();
            }

            var lastRect = GUILayoutUtility.GetLastRect();

            const int width = 30;
            const int height = 30;

            const int buttonCount = 4;

            var fullWidth = lastRect.width;
            var xOffset = (fullWidth - buttonCount * width) / 2f;

            var x = lastRect.x + xOffset;
            var y = lastRect.y + lastRect.height;

            if (GUI.Button(new Rect(x, y, width, height), _rewindIcon)) _script.RewindInEditor();
            if (GUI.Button(new Rect(x + width, y, width, height), _backwardIcon)) _script.PlayBackwardsInEditor();
            if (GUI.Button(new Rect(x + 2 * width, y, width, height), _forwardIcon)) _script.PlayForwardInEditor();
            if (GUI.Button(new Rect(x + 3 * width, y, width, height), _replayIcon)) _script.RewindAndPlayForwardInEditor();

            GUILayoutUtility.GetRect(4 * width, height);
        }

        private void ExitPreview()
        {
            _script.ExitPreviewMode();

            if (DiscardChangesOnExitPreview)
            {
                base.DiscardChanges();
            }
        }
        
        private T LoadAsset<T>(string file)
        {
            return LoadAsset<T>(file, _resourcePaths);
        }
        
        private T LoadAsset<T>(string file, params string[] paths)
        {
            foreach (var path in paths)
            {
                var fullPath = path + file;
                var asset = AssetDatabase.LoadAssetAtPath(fullPath, typeof(T));

                if (asset is T specificAsset)
                {
                    return specificAsset;
                }
            }

            return default;
        }
    }
}