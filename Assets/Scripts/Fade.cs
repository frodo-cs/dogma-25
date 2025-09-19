using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Fade : MonoBehaviour
    {
        private Image sceneFade;

        private void Awake()
        {
            sceneFade = GetComponentInChildren<Image>();
        }

        private void Start()
        {
            DayCycle.OnDayEnded += FadeInOut;
        }

        private void FadeInOut(int obj)
        {
            StartCoroutine(FadeOutCouroutine(1f));
            StartCoroutine(FadeInCouroutine(1f));
        }

        public IEnumerator FadeInCouroutine(float duration)
        {
            Color start = new Color(sceneFade.color.r, sceneFade.color.g, sceneFade.color.b, 1);
            Color target = new Color(sceneFade.color.r, sceneFade.color.g, sceneFade.color.b, 0);
            yield return FadeCouroutine(start, target, duration);
            sceneFade.enabled = false;
        }

        public IEnumerator FadeOutCouroutine(float duration)
        {
            Color start = new Color(sceneFade.color.r, sceneFade.color.g, sceneFade.color.b, 0);
            Color target = new Color(sceneFade.color.r, sceneFade.color.g, sceneFade.color.b, 1);
            sceneFade.enabled = true;
            yield return FadeCouroutine(start, target, duration);
        }

        private IEnumerator FadeCouroutine(Color start, Color target, float duration)
        {
            float elapsedTime = 0;
            float elapsedPertage = 0;

            while (elapsedTime < 1)
            {
                elapsedPertage = elapsedTime / duration;
                sceneFade.color = Color.Lerp(start, target, elapsedPertage);

                yield return null;
                elapsedTime += Time.deltaTime;
            }
        }

        private void OnDestroy()
        {
            DayCycle.OnDayEnded -= FadeInOut;
        }
    }

}
