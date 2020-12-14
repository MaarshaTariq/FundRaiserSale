using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionRandomization : MonoBehaviour {
    public Transform[] transformOptions;
    public Transform[] transformQuantityOptions;
    public GameObject[] options;
    public Vector2 positioninop;
    public GameObject[] quantityOfItems;
    public static OptionRandomization op;


	// Use this for initialization
	void Start () {
        RandomizeOptions();
        op=this;
	}
	
	public void RandomizeOptions()
    {
        for(int i=0; i<transformOptions.Length; i++)
        {
            int random = Random.Range(0, transformOptions.Length);
            Transform temp = transformOptions[random];
            Transform tempquantity = transformQuantityOptions[random];
            transformOptions[random] = transformOptions[i];
            transformQuantityOptions[random] = transformQuantityOptions[i];
            transformOptions[i] = temp;
            transformQuantityOptions[i] = tempquantity;

        }

        if(options.Length == transformOptions.Length && quantityOfItems.Length == transformOptions.Length)
        {
            for(int i=0; i<options.Length; i++)
            {
                options[i].gameObject.transform.localPosition = transformOptions[i].gameObject.transform.localPosition;
                positioninop=transformOptions[i].gameObject.transform.localPosition;
                quantityOfItems[i].gameObject.transform.localPosition = transformQuantityOptions[i].gameObject.transform.localPosition;

            }
        }
        else
        {
            Debug.Log("It doesn't match");
        }
    }
}
