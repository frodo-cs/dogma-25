using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game
{
    public class InputHandler : MonoBehaviour, GameInput.IPlayerActions
    {
        private GameInput input;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private Texture2D cursor;
        [SerializeField] private Texture2D hoverCursor;

        private void Awake()
        {
            input = new GameInput();
            input.Player.SetCallbacks(this);

            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            var mousePos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D hit = Physics2D.CircleCast(mousePos, 0.2f, Vector2.zero, 0.1f, layerMask);

            if (hit.transform != null)
                Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
            else
                Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
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
                Debug.Log($"Hit: {hit.collider.gameObject.name}");
                var clickable = hit.collider.GetComponent<ClickableItem>();
                if (clickable != null)
                    clickable.OnClick();
            }
        }
    }
}
