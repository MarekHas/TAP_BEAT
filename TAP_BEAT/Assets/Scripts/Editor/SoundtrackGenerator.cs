using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace TapBeat
{
    [CustomEditor(typeof(SoundtrackData))]
    public class SoundtrackGenerator : Editor
    {
        private SoundtrackData _soundtrack;

        private Vector2 _attributesScrollViewPosition;
        private bool _displayData;

        private void OnEnable()
        {
            _soundtrack = (SoundtrackData)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (_soundtrack.beatsList.Count == 0)
            {
                EditorGUILayout.HelpBox("Empty soundtrack", MessageType.Info);

                if (GUILayout.Button("Generate soudtrack with randoms", EditorStyles.miniButton))
                    _soundtrack.GenerateRandomBeats();
            }
            else
            {
                if (GUILayout.Button("Update soudtrack attributes", EditorStyles.miniButton))
                    _soundtrack.GenerateRandomBeats();

                _displayData = EditorGUILayout.Foldout(_displayData, "Display Beats");

                if (_displayData)
                {
                    _attributesScrollViewPosition = EditorGUILayout.BeginScrollView(_attributesScrollViewPosition);

                    for (int i = 0; i < _soundtrack.beatsList.Count; i++)
                    {
                        _soundtrack.beatsList[i] = EditorGUILayout.IntSlider(_soundtrack.beatsList[i], -1, SoundtrackData.inputs - 1);
                    }

                    EditorGUILayout.EndScrollView();
                }
            }

            EditorUtility.SetDirty(_soundtrack);
        }
    }
}

