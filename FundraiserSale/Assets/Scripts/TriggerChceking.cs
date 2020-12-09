using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChceking : MonoBehaviour {

    public string[] tagName ;
    public GameObject[] imageToBeDragged;
    public GameObject[] scoreBoardImages;
    public GameObject[] checkMarks;
    int index = 0;
    int indexForTags = 0;
    public int draggedObjectsCounter;


    public void OnTriggerEnter2D(Collider2D other)
    {
        for(int j=0; j<tagName.Length; j++){
            if (other.gameObject.tag == tagName[j])
            {
                
                Debug.Log("other.gameObject.tag" + other.gameObject.tag);
                Debug.Log("tagName" + tagName[j]);
                Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

                DragAndDrop.instance.flagHit = true;
            if(imageToBeDragged[index].tag==tagName[j]){
                imageToBeDragged[index].SetActive(true);
                //   scoreBoardImages[index].SetActive(true);
                if (index > 0 &&  index!=scoreBoardImages.Length)
                {

                    Debug.Log(index - 1);
                    scoreBoardImages[index - 1].SetActive(false);
                    scoreBoardImages[index].SetActive(true);
                  if(index==scoreBoardImages.Length-1){
                       checkMarks[j].SetActive(true);
                  }
                     
                 
                }
                 
                else
                {
                    if(index != scoreBoardImages.Length )
                    scoreBoardImages[index].SetActive(true);
                } 
                
                 
                 index = index + 1;
                 if(imageToBeDragged[index].name !=imageToBeDragged[index-1].name){checkMarks[j].SetActive(true);}
                 
                Debug.Log(index);
                draggedObjectsCounter++;

                //  Debug.Log(tagName[index]);
                Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);
                 
                
            }
           // checkMarks[j].SetActive(true);
            
        }
            Debug.Log(("checkmark kis ka hai" +j));
           
            
     }
        
    }
}
    
