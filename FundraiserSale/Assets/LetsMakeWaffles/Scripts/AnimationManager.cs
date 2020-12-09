using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {


    Animator anim;
    AnimatorClipInfo[] m_CurrentClipInfo;
    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
		Toolbox.SoundManager.PlaySound (19);
        m_CurrentClipInfo = this.anim.GetCurrentAnimatorClipInfo(0);
        Invoke( "DisableScreen", m_CurrentClipInfo[0].clip.length); 
    }

	public void DisableScreen()
	{
       // StartCoroutine(Toolbox.GameManager.LoadNextLevel());

        //this.gameObject.SetActive (false);
	}
}
