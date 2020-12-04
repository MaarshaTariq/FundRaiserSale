using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{

    public GameObject ObjectToDrag;
    public GameObject nextObjectTransformation;
    private Vector3 mousePosition;

    public GameObject[] defaultPosition;

    Vector3 OldPosition;
    public float moveSpeed = 1f;
    bool flag = false;

    public static DragAndDrop ins;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        ins = this;
    }
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnEnable()
    {

        flag = true;
    }




    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (flag)
        {

            if (Input.GetMouseButton(0) && PlayerPrefs.GetInt("Click") == 0)
            {

                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                ObjectToDrag.transform.position = Vector2.Lerp(ObjectToDrag.transform.position, mousePosition, moveSpeed);
            }
            else
            {
                //if (PopUpSaleTrayTrigger.instanc.flag)
                //{

                //}
                //else
                //{
                    print("we pata ni");
                    defaultPosition[int.Parse(nextObjectTransformation.name)].SetActive(true);
                    gameObject.SetActive(false);
                    ObjectToDrag.transform.position = defaultPosition[int.Parse(nextObjectTransformation.name)].transform.position;

                //}


            }

        }
    }
    public void resetPopcorn()
    {
        print("Yeah baby");
        int index = int.Parse(nextObjectTransformation.name) + 1;
        defaultPosition[index].GetComponent<CanvasGroup>().blocksRaycasts = true;
        ObjectToDrag.transform.position = defaultPosition[index].transform.position;
        gameObject.SetActive(false);
        //PopUpSaleTrayTrigger.instanc.flag = false;
    }
    public void resetBlockRaycast()
    {
        for (int i = 1; i < defaultPosition.Length; i++)
        {
            defaultPosition[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        defaultPosition[5].GetComponent<CanvasGroup>().blocksRaycasts = true;
        defaultPosition[0].GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    public void GetNExtTransformation(GameObject obj)
    {
        nextObjectTransformation = obj;
        print("coming object is " + obj.name);
    }
}
