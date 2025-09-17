using System;
using UnityEngine;

namespace Game
{

    public class DayCycle : MonoBehaviour
    {
        public static DayCycle Instance { get; private set; }

        private const int MIN_TIME_OF_DAY = 6;
        private const int MAX_TIME_OF_DAY = 21;
        private const int MIN_DAY = 1;
        private const int MIN_MAX_DAY = 60;
        private const int MAX_MAX_DAY = 90;

        [SerializeField, Range(6f, 24f)] private float timeOfDay = MIN_TIME_OF_DAY;
        [SerializeField] private int day = MIN_DAY;
        [SerializeField] private float timeSpeed = 1f;
        [SerializeField] private int maxDay = MIN_MAX_DAY;

        public int Day { get { return day; } }
        public static Action<int> OnDayEnded;
        public static Action<int> OnTimeAdded;
        public static Action<int> OnLastDay;

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
            day = Mathf.Max(GameStateManager.Instance.Day, MIN_DAY);
            timeOfDay = Mathf.Max(GameStateManager.Instance.TimeOfDay, MIN_TIME_OF_DAY);

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
                timeOfDay += (Time.deltaTime * timeSpeed) / 3600f;
                GameStateManager.Instance.TimeOfDay = timeOfDay;
                if (timeOfDay >= MAX_TIME_OF_DAY)
                {
                    EndDay();
                    GameStateManager.Instance.Day = day;
                }
            }
        }

        public void AddTime(int minutes)
        {
            timeOfDay += minutes / 60f;
            if (timeOfDay >= MAX_TIME_OF_DAY)
            {
                EndDay();
            }
        }

        private void EndDay()
        {
            OnDayEnded?.Invoke(day);
            day++;
            if (day >= maxDay)
            {
                OnLastDay?.Invoke(day);
            }
            timeOfDay = MIN_TIME_OF_DAY;
        }

        public string GetFormattedTime()
        {
            int hours = Mathf.FloorToInt(timeOfDay);
            float fractional = timeOfDay - hours;
            int minutes = Mathf.FloorToInt(fractional * 60f);
            int seconds = Mathf.FloorToInt((fractional * 60f - minutes) * 60f);

            return $"{hours:00}:{minutes:00}:{seconds:00}";
        }

        private void OnDestroy()
        {
            OnTimeAdded -= AddTime;
        }
    }

}

