using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class Rewind : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        Coroutine rewindAnimation;

        void Start()
        {
            spriteRenderer.enabled = false;
            InputHandler.OnObjectClick += PlayRewind;
            DayCycle.OnDayEnded += StopRoutine;
        }

        private void StopRoutine(int obj)
        {
            if (rewindAnimation != null)
                StopCoroutine(rewindAnimation);
        }

        private void PlayRewind(string obj)
        {
            rewindAnimation = StartCoroutine(ShowAnimation());
        }

        private IEnumerator ShowAnimation()
        {
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.enabled = false;
        }

        private void OnDestroy()
        {
            InputHandler.OnObjectClick -= PlayRewind;
            DayCycle.OnDayEnded -= StopRoutine;
        }
    }

}
