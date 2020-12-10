using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightText : MonoBehaviour {
 public Image highlightImage;
 public float maxFillAmount =400f;
 public  float fillAmountForImage;
    public float waitTime = 30.0f;
    int j = 0;
    bool ht;
    TriggerChceking tc;

 public void Start()
 {
     
     fillAmountForImage= 1;
        


 
 }
    public void Update()
    {
        increaseFillAmount(highlightImage);
        //increaseFillAmount();
    }
    IEnumerator waitAfterLoad()
    {
        yield return new WaitForSeconds(2f);
    }
    public void Highlight(Image highlight_Image) 
     {

        
            if (highlight_Image.fillAmount < maxFillAmount)
            {
                highlight_Image.fillAmount += fillAmountForImage  ;

            }
            
       
        
    }
    
    public void increaseFillAmount(Image highlight_Image)
    {
        fillAmountForImage += 1f;
        Highlight(highlight_Image);
    }

  //public void IncreaseFillAmount()
    //{
	 //fillAmountForImage +=1f  ;
     //}

}
