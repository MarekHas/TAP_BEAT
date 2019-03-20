using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TapBeat
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private KeyCode _rightArrowKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode _leftArrowKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _upArrowKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode _downArrowKey = KeyCode.DownArrow;

        [SerializeField] private SoundtrackData _soundtrackData;
        /// <summary>
        /// Actual soundtrack
        /// </summary>
        public SoundtrackData SoundtrackData { get { return _soundtrackData; } }
        #region MonoBehavior Methods
        private void Start()
        {
            
        }
        private void Update()
        {
            if (Input.GetKeyDown(_rightArrowKey))
                TapBeat(0);
            if (Input.GetKeyDown(_leftArrowKey))
                TapBeat(1);
            if (Input.GetKeyDown(_upArrowKey))
                TapBeat(2);
            if (Input.GetKeyDown(_downArrowKey))
                TapBeat(3);
        }
        #endregion

        #region Game

        public void TapBeat(int _input)
        {
            Debug.Log("tap " + _input);
        }
        #endregion
    }
}

