using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionFadeScript : MonoBehaviour
{

    private CanvasGroup fade;
    private bool fadeIn = true;

    void Update()
    {
        if (fadeIn == true)
        {
            StartCoroutine(FadeTo(1.0f, 2.0f));
        }
        else
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        fade = GetComponent<CanvasGroup>();
        yield return new WaitForSeconds(1.0f);
        float alpha = fade.alpha;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            fade.alpha = Mathf.Lerp(alpha, aValue, t);

            yield return null;
        }
    }
}