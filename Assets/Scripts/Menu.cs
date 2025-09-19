using Game;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI start;
    [SerializeField] private SceneController controller;

    void Start()
    {
        start.text = GameText.Instance.GetUIText(UIKeys.start);
    }

    public void OnStart()
    {
        controller.LoadScene("1_game");
    }

}
