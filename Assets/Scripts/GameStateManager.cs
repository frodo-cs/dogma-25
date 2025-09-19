using UnityEngine;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance { get; private set; }

        public int SecondsOfDay { get; set; }
        public int Day { get; set; }
        public int MaxDay { get; set; }
        public bool isEnding1 { get; set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
}