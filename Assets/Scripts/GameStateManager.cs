using UnityEngine;

namespace Game
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance { get; private set; }

        public float TimeOfDay { get; set; }
        public int Day { get; set; }
        public int MaxDay { get; set; }
        public bool IsGameEnded { get; set; }
        public Languages Language { get; set; } = Languages.SPANISH;

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