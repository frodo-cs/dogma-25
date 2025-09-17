using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI start;

    void Start()
    {
        start.text = Language.Instance.GetUIText(UIKeys.start);
    }

    public void OnStart()
    {
        SceneManager.LoadScene("1_computer");
    }


}
