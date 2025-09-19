using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private float fadeDuration;
        private Fade sceneFade;

        private void Awake()
        {
            sceneFade = GetComponentInChildren<Fade>();
        }

        private IEnumerator Start()
        {
            yield return sceneFade.FadeInCouroutine(fadeDuration);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            yield return sceneFade.FadeOutCouroutine(fadeDuration);
            yield return SceneManager.LoadSceneAsync(sceneName);
        }
    }
}