using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Ending : MonoBehaviour
    {
        [SerializeField] private GameObject ending1;
        [SerializeField] private GameObject ending2;
        [SerializeField] private GameObject ending3;
        [SerializeField] private Image background;
        [SerializeField] private Texture2D ending1BG;
        [SerializeField] private Texture2D ending2BG;

        private void Start()
        {
            if (GameStateManager.Instance.isEnding1)
            {
                ending1.SetActive(true);
                ending2.SetActive(false);
                ending3.SetActive(false);
                ending1.GetComponent<TextMeshProUGUI>().text = GameText.Instance.GetUIText(UIKeys.ending1);
                background.sprite = GetSprite(ending1BG);
            } else
            {
                ending1.SetActive(false);
                ending2.SetActive(true);
                ending3.SetActive(true);
                ending2.GetComponent<TextMeshProUGUI>().text = GameText.Instance.GetUIText(UIKeys.ending2);
                ending3.GetComponent<TextMeshProUGUI>().text = GameText.Instance.GetUIText(UIKeys.ending3);
                background.sprite = GetSprite(ending2BG);
            }
        }

        private Sprite GetSprite(Texture2D texture)
        {
            if (texture == null)
                return null;
            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}
