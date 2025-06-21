using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            // StartCoroutine(FadeOutIn());
        }

        public void FadeOutImmediate()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1;
            }
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;
            }
        }
        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f);
            print("fade out");
            yield return FadeIn(3f);
            print("fade in");

        }

        public IEnumerator FadeIn(float time)
        {
            if (canvasGroup == null) yield return null;

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}