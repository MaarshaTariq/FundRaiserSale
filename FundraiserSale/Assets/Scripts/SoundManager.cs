using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [HideInInspector]
	public AudioClip[] sounds;
	public AudioClip correctAudio ;
	public AudioClip incorrectAudio ;
    public GameObject inputBlocker;
	private AudioSource audioPlayer;
  
    // Use this for initialization
    private void Awake()
    {
		audioPlayer = this.GetComponent<AudioSource> ();
        
    }
	
	public void PlaySound(int index)
	{
     //   Debug.Log("In PlaySound");
        StartCoroutine(_playSound(index));
    }
   
    public void PlaySoundWithAudioClip(AudioClip _clip)
    {
        StartCoroutine(_playSoundWithAudioClip(_clip));
    }
    public IEnumerator _playSound(int index)
    {
        inputBlocker.SetActive(true);
        audioPlayer.clip = Toolbox.TextToSpeech.downloadedClips[index];
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        inputBlocker.SetActive(false);
    }
    public IEnumerator _playSoundWithAudioClip(AudioClip _clip)
    {
        inputBlocker.SetActive(true);
        audioPlayer.clip = _clip;
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        inputBlocker.SetActive(false);
    }
    

}
