using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneMnagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        IEnumerator FadeOutIn()
        {
            yield return FadeOut(1f);
            yield return FadeIn(1f);
        }

        public IEnumerator FadeOut(float time)
        {
            float alphaTime = 0.0f;

            while (alphaTime <= 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, alphaTime);
                alphaTime += Time.deltaTime;

                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            float alphaTime = 0.0f;

            while (alphaTime <= 1.0f)
            {
                canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, alphaTime);
                alphaTime += Time.deltaTime;

                yield return null;
            }
        }
    }
}