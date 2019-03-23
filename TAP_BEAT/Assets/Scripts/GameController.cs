using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

        public float BeatsPerSecond { get; private set; }
        public float SecondPerBeat { get; private set; }

        private bool _soundTrackCompleted; // has the player completed the track
        private bool _played; // has the player played within the current beat

        private SoundtrackView _soundtrackView;
        private WaitForSeconds _waitTime;

        #region BeatEvents Interfaces
        List<ITapEventHandler> _tickHandlers;
        #endregion

        #region Singleton Pattern
        private static GameController _instance;
        public static GameController Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = (GameController)GameObject.FindObjectOfType(typeof(GameController));
                }
                return _instance;
            }
     
        }
        #endregion

        #region MonoBehavior Methods
        private void OnDestroy()
        {
            _instance = null;
        }
        private void Awake()
        {
            _instance = this;
            BeatsPerSecond = _soundtrackData.bpm / 60f;
            SecondPerBeat = 60f / _soundtrackData.bpm;
            _waitTime = new WaitForSeconds(SecondPerBeat* 2);

            _soundtrackView = FindObjectOfType<SoundtrackView>();
            if(_soundtrackView == null)
            {
                Debug.Log("no soundtrackView in scene");
            }

            List<Transform> rootTransforms = (from t in FindObjectsOfType<Transform>()
                                              where t.parent == null
                                              select t).ToList();

            _tickHandlers = new List<ITapEventHandler>();
            foreach (Transform t in rootTransforms)
                _tickHandlers.AddRange(t.GetComponentsInChildren<ITapEventHandler>());
        }

        private void Start()
        {
            InvokeRepeating("PlayNextBeat",0f,SecondPerBeat);
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

                       StartCoroutine(StopAndWait());
                    }
                }
            }
        }

        public void TapBeat(int _input)
        {

           

            Debug.Log("tap " + _input);
            _played = true;
            if(_soundtrackData.beatsList[CurrentBeat]== -1)
            {
                Debug.Log(string.Format("{0} play to early",_input));
 
            }
            else if (_soundtrackData.beatsList[CurrentBeat] == _input)
            {
                Debug.Log(string.Format("{0} GOOD !!!", _input));
                _soundtrackView.ChangeViewBasedOnTapResult(CurrentBeat, SoundtrackView.TapResult.Good);
            }
            else//played wrong keycode
            {
                Debug.Log(string.Format("{0} played wrong key , {1} expected",_input, _soundtrackData.beatsList[CurrentBeat]));
                _soundtrackView.ChangeViewBasedOnTapResult(CurrentBeat, SoundtrackView.TapResult.WrongKey);
            }
            
        }

        public void PlayNextBeat()
        {
            //			Debug.Log ("Tick");
            if (_tickHandlers.Count > 0)
                for (int i = 0; i < _tickHandlers.Count; i++)
                    _tickHandlers[i].Tapped(CurrentBeat);

            //-1 is empty beat
            if (!_played && _soundtrackData.beatsList[CurrentBeat] != -1)
            {
                Debug.Log(string.Format("{0} missed", _soundtrackData.beatsList[CurrentBeat]));
                _soundtrackView.ChangeViewBasedOnTapResult(CurrentBeat, SoundtrackView.TapResult.TimeMismatch);
            }
            _played = false;

            CurrentBeat++;
        }

        private IEnumerator StopAndWait()
        {
            yield return _waitTime;
            enabled = false;
        }
        #endregion
    }
}

