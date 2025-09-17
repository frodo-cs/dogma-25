using UnityEngine;

namespace Game
{
    public class ClickableItem : MonoBehaviour
    {
        [SerializeField] private ItemDuration duration = ItemDuration.Thirty;
        [SerializeField] private ActionKeys[] actions;
        private ActionKeys oldAction;
        private ActionKeys action;

        private void Start()
        {
            PickAction();
        }

        private void PickAction()
        {
            if (actions == null || actions.Length == 0)
            {
                return;
            }

            if (actions.Length == 1)
            {
                action = actions[0];
            } else
            {
                int attempts = 0;
                do
                {
                    int index = Random.Range(0, actions.Length);
                    action = actions[index];
                    attempts++;
                } while (action == oldAction && attempts < 10);

            }

            duration = Actions.Instance.GetActionDuration(action);
            oldAction = action;
        }

        public void OnClick()
        {
            DayCycle.OnTimeAdded?.Invoke((int)duration);
            PickAction();
            Debug.Log($"Action picked: {action}");
        }
    }
}