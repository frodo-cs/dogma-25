using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    public class InputHandler : MonoBehaviour, GameInput.IPlayerActions
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;

        public static Action<string> OnObjectEnter;
        public static Action OnObjectLeave;
        public static Action<string> OnObjectClick;

        private Transform lastHovered;
        private GameInput input;

        private void Awake()
        {
            input = new GameInput();
            input.Player.SetCallbacks(this);

            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }


        private void Update()
        {
            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.CircleCast(mousePos, 0.2f, Vector2.zero, 0.1f, layerMask);

            Transform current = hit.transform;

            if (current != null && current != lastHovered)
            {
                var clickable = current.GetComponent<ClickableItem>();
                if (clickable != null)
                {
                    OnObjectEnter?.Invoke(GameText.Instance.GetActionText(clickable.Action));
                }
            }

            if (lastHovered != null && current != lastHovered)
            {
                OnObjectLeave?.Invoke();
            }

            lastHovered = current;
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.CircleCast(mousePos, 0.2f, Vector2.zero, 0.1f, layerMask);
            if (hit.transform != null)
            {
                var clickable = hit.collider.GetComponent<ClickableItem>();
                if (clickable != null)
                {
                    clickable.OnClick();
                    OnObjectClick?.Invoke(GameText.Instance.GetActionText(clickable.Action));
                }
            }
        }
    }
}