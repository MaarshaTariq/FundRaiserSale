using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightText : MonoBehaviour
{
    public Image highlightImage;
    public float maxFillAmount = 400f;
    public float fillAmountForImage;
    public float waitTime = 1f;


    public IEnumerator _HighlightText(Image highlightImage)
    {
        yield return new WaitForSeconds(0.5f);

        while (highlightImage.fillAmount <= 1)
        {
            highlightImage.fillAmount += 1f / waitTime * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
