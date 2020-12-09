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



        foreach (string tag in tagName)
        {
            if (other.gameObject.tag == tag)
            {
                Debug.Log("other.gameObject.tag" + other.gameObject.tag);
                Debug.Log("tagName" + tagName);
                Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

                DragAndDrop.instance.flagHit = true;
                imageToBeDragged[index].SetActive(true);
                //   scoreBoardImages[index].SetActive(true);

                if (index > 0 &&  index!=scoreBoardImages.Length)
                {

                    Debug.Log(index - 1);
                    scoreBoardImages[index - 1].SetActive(false);
                    scoreBoardImages[index].SetActive(true);

                }
                else
                {
                    if(index != scoreBoardImages.Length - 1)
                    scoreBoardImages[index].SetActive(true);
                }

                index = index + 1;
                Debug.Log(index);
                draggedObjectsCounter++;

                //  Debug.Log(tagName[index]);
                Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

            }
            //checkMarks[indexForTags].SetActive(true);

        }
        }
        
     
        
    
    
	}
