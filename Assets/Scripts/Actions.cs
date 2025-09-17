using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum ActionKeys
    {
        work,
        scroll,
        play,
        watch_series,
        get_food,
        drink_water,
        go_bathroom,
        wander,
        lift_weights,
        sleep,
        exist
    }

    public enum ItemDuration
    {
        Double = 120,
        Sixty = 60,
        Thirty = 30,
        Fifteen = 15,
        Five = 5
    }

    public class Actions : MonoBehaviour
    {
        public static Actions Instance { get; private set; }

        #region Action Data
        private static readonly Dictionary<ActionKeys, ItemDuration> actionDuration = new() {
            { ActionKeys.work, ItemDuration.Double },
            { ActionKeys.scroll,  ItemDuration.Double },
            { ActionKeys.play, ItemDuration.Sixty },
            { ActionKeys.watch_series,  ItemDuration.Sixty },
            { ActionKeys.get_food, ItemDuration.Fifteen },
            { ActionKeys.drink_water, ItemDuration.Five },
            { ActionKeys.go_bathroom ,ItemDuration.Five },
            { ActionKeys.wander, ItemDuration.Five },
            { ActionKeys.lift_weights, ItemDuration.Thirty },
            { ActionKeys.sleep,  ItemDuration.Double },
            { ActionKeys.exist,  ItemDuration.Five },
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
        }


        public ItemDuration GetActionDuration(ActionKeys key)
        {
            if (actionDuration.TryGetValue(key, out var value))
            {
                return value;
            }
            return 0;
        }


    }
}