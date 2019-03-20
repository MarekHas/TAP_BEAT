using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TapBeat
{
    [CreateAssetMenu(menuName = "TAP_BEAT/New Soundtrack", fileName = "New Soundtrack.asset")]
    public class SoundtrackData : ScriptableObject
    {

        [Range(40, 200)] public int bpm = 120;

        [HideInInspector] public List<int> beatsList = new List<int>();

#if UNITY_EDITOR 
       
        static public int inputs = 4;
        [Range(0f, 10f)] [SerializeField] private int _entry = 10;
        [Range(1, 20)] [SerializeField] private int _minSectionSize= 2;
        [Range(1, 20)] [SerializeField] private int _maxSectionSize = 5;
        [Range(1, 20)] [SerializeField] private int _minBreakSize = 1;
        [Range(1, 20)] [SerializeField] private int _maxBreakSize = 2;
        [Range(1, 20)] [SerializeField] private int _sectionsNumber = 10;

        public void GenerateRandomBeats()
        {
            beatsList = new List<int>();
           
            for (int i = 0; i < _entry; i++)
            {
                beatsList.Add(-1);
            }

            for (int i = 0; i < _sectionsNumber; i++)
            {
                int blockLength = Random.Range(_minSectionSize, _maxSectionSize + 1);
                for (int b = 0; b < blockLength; b++)
                {
                    int beat = Random.Range(0, inputs);
                    beatsList.Add(beat);
                }
                //dont add empty blocks at the end of all sections
                if (i == _sectionsNumber - 1)
                    break;

                int breakLength = Random.Range(_minBreakSize, _maxBreakSize + 1);
                for (int b = 0; b < breakLength; b++)
                {
                    beatsList.Add(-1);
                }
            }
        }
#endif
    }
}

