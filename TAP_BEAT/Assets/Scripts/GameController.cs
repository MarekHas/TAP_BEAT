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

        public float SecondsPerBeat { get; private set; }
        public float BeatsPerSecond { get; private set; }

        private bool _soundTrackCompleted; // has the player completed the track
        private bool _played; // has the player played within the current beat
        private SoundtrackView _soundtrackView;
        #region MonoBehavior Methods
        private void Awake()
        {
            SecondsPerBeat = _soundtrackData.bpm / 60f;
            BeatsPerSecond = 60f / _soundtrackData.bpm;
        }

        private void Start()
        {
            InvokeRepeating("PlayNextBeat",0f,BeatsPerSecond);
        }
        private void Update()
        {
            if (_played || _soundTrackCompleted)
                return;

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
        private int _currentBeat;
        public int CurrentBeat
        {
            get { return _currentBeat; }
            private set
            {
                if (value != _currentBeat)
                {
                    _currentBeat = value;

                    if (_currentBeat == _soundtrackData.beatsList.Count)
                    {
                        CancelInvoke("PlayNextBeat");

                        _soundTrackCompleted = true;

                       //StartCoroutine(WaitAndStop());
                    }
                }
            }
        }

        public void TapBeat(int _input)
        {
            Debug.Log("tap " + _input);
            
            if(_soundtrackData.beatsList[CurrentBeat]== -1)
            {
                Debug.Log(string.Format("{0} play to early",_input));
            }
            else if (_soundtrackData.beatsList[CurrentBeat] == _input)
            {
                Debug.Log(string.Format("{0} GOOD !!!", _input));
            }
            else//played wrong keycode
            {
                Debug.Log(string.Format("{0} played wrong key , {1} expected",_input, _soundtrackData.beatsList[CurrentBeat]));
            }
            
        }

        public void PlayNextBeat()
        {
            //-1 is empty beat
            if (!_played && _soundtrackData.beatsList[CurrentBeat] != -1)
            {
                Debug.Log(string.Format("{0} missed", _soundtrackData.beatsList[CurrentBeat]));
            }
            _played = false;

            CurrentBeat++;
        }
        #endregion
    }
}

