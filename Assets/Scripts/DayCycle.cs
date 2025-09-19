using System;
using UnityEngine;

namespace Game
{

    public class DayCycle : MonoBehaviour
    {
        public static DayCycle Instance { get; private set; }

        private const int MIN_TIME_OF_DAY = 21600; // 6:00
        private const int MAX_TIME_OF_DAY = 75600; // 21:00
        private const int MIN_DAY = 1;
        private const int MIN_MAX_DAY = 20;
        private const int MAX_MAX_DAY = 50;

        [SerializeField, Range(21600, 86400)] private int secondsOfDay;
        [SerializeField] private int day = MIN_DAY;
        [SerializeField] private float timeSpeed = 1f;
        [SerializeField] private int maxDay = MIN_MAX_DAY;
        [SerializeField] private SceneController controller;

        public int Day { get { return day; } }
        public int SecondsOfDay { get { return secondsOfDay; } }
        public static Action<int> OnDayEnded;
        public static Action<int> OnTimeAdded;
        public static Action OnLastDay;

        private AudioSource audioSource;
        private int lastGameSecond = -1;
        private float timeAccumulator = 0f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            day = Mathf.Max(GameStateManager.Instance.Day, MIN_DAY);
            secondsOfDay = Mathf.Max(GameStateManager.Instance.SecondsOfDay, MIN_TIME_OF_DAY);

            if (GameStateManager.Instance.MaxDay <= MIN_MAX_DAY)
            {
                maxDay = UnityEngine.Random.Range(MIN_MAX_DAY, MAX_MAX_DAY);
                GameStateManager.Instance.MaxDay = maxDay;
            } else
            {
                maxDay = GameStateManager.Instance.MaxDay;
            }

            OnTimeAdded += AddTime;
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                float safeDelta = Mathf.Min(Time.deltaTime, 0.05f);
                timeAccumulator += safeDelta * timeSpeed;

                while (timeAccumulator >= 1f)
                {
                    secondsOfDay++;
                    timeAccumulator -= 1f;
                }

                GameStateManager.Instance.SecondsOfDay = secondsOfDay;

                HandleClockTick();

                if (secondsOfDay >= MAX_TIME_OF_DAY)
                {
                    EndDay();
                    GameStateManager.Instance.Day = day;
                }
            }
        }

        public void AddTime(int minutes)
        {
            secondsOfDay += minutes * 60;
            if (secondsOfDay >= MAX_TIME_OF_DAY)
            {
                EndDay();
            }
        }

        private void HandleClockTick()
        {
            if (secondsOfDay != lastGameSecond)
            {
                audioSource?.PlayOneShot(audioSource.clip);
                lastGameSecond = secondsOfDay;
            }
        }

        private void EndDay()
        {
            OnDayEnded?.Invoke(day);
            day++;
            if (day >= maxDay)
            {
                GameStateManager.Instance.isEnding1 = false;
                controller.LoadScene("3_ending");
            }
            secondsOfDay = MIN_TIME_OF_DAY;
        }

        public string GetFormattedTime()
        {
            int hours = secondsOfDay / 3600;
            int minutes = (secondsOfDay % 3600) / 60;
            int seconds = secondsOfDay % 60;

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        private void OnDestroy()
        {
            OnTimeAdded -= AddTime;
        }
    }

}

