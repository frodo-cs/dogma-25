using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum Languages
    {
        SPANISH,
        ENGLISH
    }

    public enum UIKeys
    {
        start,
        hour,
        day,
        badEnd1,
        badEnd2,
        goodEnd
    }

    public class Language : MonoBehaviour
    {
        public static Language Instance { get; private set; }

        [SerializeField] private Languages language = Languages.SPANISH;
        public Languages CurrentLanguage => language;

        #region Language Data
        private readonly Dictionary<Languages, Dictionary<ActionKeys, string>> actionText = new()
            {
                {
                    Languages.SPANISH, new Dictionary<ActionKeys, string>
                    {
                        { ActionKeys.work, "Trabajar" },
                        { ActionKeys.scroll, "Scrollear" },
                        { ActionKeys.play, "Jugar" },
                        { ActionKeys.watch_series, "Ver alguna serie" },
                        { ActionKeys.get_food, "Ir por algo de comer" },
                        { ActionKeys.drink_water, "Tomar agua" },
                        { ActionKeys.go_bathroom, "Ir al baño" },
                        { ActionKeys.wander, "Merodear" },
                        { ActionKeys.lift_weights, "Levantar pesas" },
                        { ActionKeys.sleep, "Dormir" },
                        { ActionKeys.exist, "Existir" }
                    }
                },
                {
                    Languages.ENGLISH, new Dictionary<ActionKeys, string>
                    {
                        { ActionKeys.work, "Work" },
                        { ActionKeys.scroll, "Doom scroll" },
                        { ActionKeys.play, "Play" },
                        { ActionKeys.watch_series, "Watch a tv show" },
                        { ActionKeys.get_food, "Get a snack" },
                        { ActionKeys.drink_water, "Drink water" },
                        { ActionKeys.go_bathroom, "Go pee" },
                        { ActionKeys.wander, "Wander around" },
                        { ActionKeys.lift_weights, "Lift weights" },
                        { ActionKeys.sleep, "Sleep" },
                        { ActionKeys.exist, "Exist" }
                    }
                }
            };

        private readonly Dictionary<Languages, Dictionary<UIKeys, string>> UIText = new()
            {
                {
                    Languages.SPANISH, new Dictionary<UIKeys, string>
                    {
                        { UIKeys.start, "Iniciar" },
                        { UIKeys.badEnd1, "El tiempo siguió avanzando… y vos también" },
                        { UIKeys.badEnd2, "Al menos fuiste constante" },
                        { UIKeys.goodEnd, "La rutina puede esperar otro día" },
                        { UIKeys.day, "Día" },
                        { UIKeys.hour, "Hora" }
                    }
                },
                {
                    Languages.ENGLISH, new Dictionary<UIKeys, string>
                    {
                        { UIKeys.start, "Start" },
                        { UIKeys.badEnd1, "Time went on and so did you" },
                        { UIKeys.badEnd2, "At least you were constant" },
                        { UIKeys.goodEnd, "Routine can wait another day" },
                        { UIKeys.day, "Day" },
                        { UIKeys.hour, "Hour" }
                    }
                }
            };

        #endregion

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

        private void Start()
        {
            if (GameStateManager.Instance != null)
                language = GameStateManager.Instance.Language;
        }

        public void SetLanguage(Languages newLanguage)
        {
            language = newLanguage;
            GameStateManager.Instance.Language = language;
        }

        public string GetActionText(ActionKeys key)
        {
            if (actionText.TryGetValue(language, out var langDict) && langDict.TryGetValue(key, out var value))
            {
                return value;
            }
            return $"Missing: {key}";
        }

        public string GetUIText(UIKeys key)
        {
            if (UIText.TryGetValue(language, out var langDict) && langDict.TryGetValue(key, out var value))
            {
                return value;
            }
            return $"Missing: {key}";
        }
    }
}