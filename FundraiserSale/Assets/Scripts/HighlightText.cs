using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightText : MonoBehaviour {
 public Image highlight_Image;
 public float maxFillAmount =350f;
 public static float fillAmountForImage;
 public void Start()
 {
 highlight_Image= GetComponent<Image>();	 
 fillAmountForImage= 1;
 
 }
 public void Update()
 {
	 
	 if(highlight_Image.fillAmount < maxFillAmount){
	 highlight_Image.fillAmount = fillAmountForImage * Time.time *0.2f;
	 }
 }
 public void IncreaseFillAmount(){
	 fillAmountForImage +=1f  ;
 }

}
