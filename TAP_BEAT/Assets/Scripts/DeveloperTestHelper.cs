using UnityEngine;

namespace TapBeat
{
    public class DeveloperTestHelper : MonoBehaviour, ITapEventHandler
    {
        [SerializeField] float speed = 1f;
        [SerializeField] bool debugTap;
        [SerializeField] bool debugOverlooked;
        [SerializeField] bool pauseOnTap;
        [SerializeField] bool pauseOnMissed;

        void Start()
        {
            Time.timeScale = speed;
        }

        public void Tapped(int actual)
        {
            if (debugTap)
                Debug.Log(string.Format("Beat #{0}, time :{1}", actual, Time.time));

            if (pauseOnTap)
                Debug.Break();
        }

        public void BeatOverlooked(int actualBeat)
        {
            if (debugOverlooked)
                Debug.Log(string.Format("{0} missed", actualBeat));

            if (pauseOnMissed)
                Debug.Break();
        }
    }
}
