using Game;
using System.Collections;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private SceneController controller;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI hour;
    [SerializeField] private GameObject computer;
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject overlay;

    private bool showingComputer = true;
    private Coroutine updateClock;
    private Coroutine timeRandomizer;

    private void Start()
    {
        InputHandler.OnObjectEnter += ShowLabel;
        InputHandler.OnObjectClick += ShowLabel;
        InputHandler.OnObjectLeave += HideLabel;
        DayCycle.OnTimeAdded += TimeRandomizer;
        DayCycle.OnDayEnded -= StopRoutines;
        updateClock = StartCoroutine(UpdateClock());
    }

    private void StopRoutines(int obj)
    {
        if (timeRandomizer != null)
            StopCoroutine(timeRandomizer);
        if (updateClock != null)
            StopCoroutine(updateClock);
    }

    private void TimeRandomizer(int secondsToAdd)
    {
        timeRandomizer = StartCoroutine(AnimateClock(secondsToAdd, 0.5f));
    }

    private IEnumerator AnimateClock(int secondsToAdd, float duration)
    {
        if (DayCycle.Instance == null || secondsToAdd <= 0)
            yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            int random = UnityEngine.Random.Range(0, 24);
            hour.text = $"{random:00}:{random:00}:{random:00}";

            elapsed += Time.deltaTime;
            yield return null;
        }
        hour.text = DayCycle.Instance.GetFormattedTime();
    }

    private void HideLabel()
    {
        overlay.SetActive(false);
    }

    private void ShowLabel(string action)
    {
        overlay.GetComponent<TextMeshProUGUI>().text = action;
        overlay.SetActive(true);
    }

    private IEnumerator UpdateClock()
    {
        while (true)
        {
            if (DayCycle.Instance != null)
            {
                day.text = $"{GameText.Instance.GetUIText(UIKeys.day)} {DayCycle.Instance.Day}";
                hour.text = DayCycle.Instance.GetFormattedTime();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void OnExitPressed()
    {
        GameStateManager.Instance.isEnding1 = true;
        controller.LoadScene("3_ending");
    }

    public void OnMoveScene()
    {
        showingComputer = !showingComputer;
        computer.SetActive(showingComputer);
        bed.SetActive(!showingComputer);
    }

    private void OnDestroy()
    {
        InputHandler.OnObjectEnter -= ShowLabel;
        InputHandler.OnObjectClick -= ShowLabel;
        InputHandler.OnObjectLeave -= HideLabel;
        DayCycle.OnTimeAdded -= TimeRandomizer;
    }
}
