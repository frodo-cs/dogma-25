using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum UIKeys
    {
        start,
        hour,
        day,
        ending1,
        ending2,
        ending3
    }

    public class GameText : MonoBehaviour
    {
        public static GameText Instance { get; private set; }

        #region Text Data
        private readonly Dictionary<ActionKeys, string> actionText = new()
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
                { ActionKeys.exist, "Contemplar existencia" }
            };

        private readonly Dictionary<UIKeys, string> UIText = new()
            {
                { UIKeys.start, "Iniciar" },
                { UIKeys.ending1, "La rutina puede esperar otro día" },
                { UIKeys.ending2, "El tiempo siguió avanzando… y vos también" },
                { UIKeys.ending3, "Al menos fuiste constante..." },
                { UIKeys.day, "Día" },
                { UIKeys.hour, "Hora" }
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


        public string GetActionText(ActionKeys key)
        {
            if (actionText.TryGetValue(key, out var value))
            {
                return value;
            }
            return $"Missing: {key}";
        }

        public string GetUIText(UIKeys key)
        {
            if (UIText.TryGetValue(key, out var value))
            {
                return value;
            }
            return $"Missing: {key}";
        }
    }
}