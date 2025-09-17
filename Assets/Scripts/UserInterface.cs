using Game;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI hour;

    void Update()
    {
        day.text = $"{Language.Instance.GetUIText(UIKeys.day)} {DayCycle.Instance.Day}";
        hour.text = DayCycle.Instance.GetFormattedTime();
    }

    public void OnExitPressed()
    {
        SceneManager.LoadScene("3_good_1");
    }

    public void OnMoveScene()
    {
        var currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "1_computer")
            SceneManager.LoadScene("2_bed");
        else if (currentScene == "2_bed")
            SceneManager.LoadScene("1_computer");

    }
}
