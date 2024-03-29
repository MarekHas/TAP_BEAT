﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TapBeat
{
    public class SoundtrackView : MonoBehaviour
    {
        public enum TapResult { Good, WrongKey, TimeMismatch}

        [SerializeField] private RectTransform _rightArrow;
        [SerializeField] private RectTransform _leftArrow;
        [SerializeField] private RectTransform _upArrow;
        [SerializeField] private RectTransform _downArrow;

        [SerializeField] RectTransform _emptyField;

        RectTransform _rectTransform;
        List<Image> _beatImages;


        Vector2 _soundTrackViewPosition;

        float _beatSizeView;
        float _spaceBetweenBeats;


		public float SoundtrackViewPosition
        {
            get { return _soundTrackViewPosition.y; }
            set
            {
                if (value != _soundTrackViewPosition.y)
                {
                    _soundTrackViewPosition.y = value;
                    _rectTransform.anchoredPosition = _soundTrackViewPosition;
                }
            }
        }

        public void Init(SoundtrackData _soundtrackData)
        {
            _rectTransform = (RectTransform)transform;
            _soundTrackViewPosition = _rectTransform.anchoredPosition;

            _beatSizeView = _emptyField.rect.height;
            _spaceBetweenBeats = GetComponent<VerticalLayoutGroup>().spacing;

            _beatImages = new List<Image>();

            foreach (int arrowNumber in _soundtrackData.beatsList)
            {
                GameObject g;
                switch (arrowNumber)
                {
                    case 0:
                        g = _rightArrow.gameObject;
                        break;
                    case 1:
                        g = _rightArrow.gameObject;
                        break;
                    case 2:
                        g = _upArrow.gameObject;
                        break;
                    case 3:
                        g = _downArrow.gameObject;
                        break;
                    default:
                        g = _emptyField.gameObject;
                        break;
                }
                Image beatImage = GameObject.Instantiate(g, transform).GetComponent<Image>();
                beatImage.transform.SetAsFirstSibling();

                _beatImages.Add(beatImage);

            }
        }

        private void Start()
        {
            Init(GameController.Instance.SoundtrackData);
        }

        void Update()
        {
            SoundtrackViewPosition -=  (_beatSizeView + _spaceBetweenBeats) *Time.deltaTime * GameController.Instance.BeatsPerSecond;
        }

        public void ChangeViewBasedOnTapResult(int index, TapResult tapResult)
        {
            switch (tapResult)
            {
                case TapResult.TimeMismatch:
                    _beatImages[index].color = Color.black;
                    break;
                case TapResult.Good:
                    _beatImages[index].color = Color.green;
                    break;
                case TapResult.WrongKey:
                    _beatImages[index].color = Color.red;
                    break;

            }
        }
    }

}
