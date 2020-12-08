using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChceking : MonoBehaviour {

public string tagName;
public void OnTriggerEnter2D(Collider2D other)
{
	if(other.gameObject.tag==tagName){
		Debug.Log("other.gameObject.tag" +other.gameObject.tag);
		Debug.Log("tagName" +tagName);
            Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

            DragAndDrop.instance.flagHit = true;
        Debug.Log("DragAndDrop.instance.flagHit" + DragAndDrop.instance.flagHit);

        }

    }
	}
