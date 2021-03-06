﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour {
     public AudioClip initialAudio;
    public AudioClip cupcakesIncorrectSound;
    public AudioClip FRIncorrectSound;
    public AudioClip browniesIncorrectSound;
    public AudioClip orderFormSound;

    public AudioClip correctaudio;
    public AudioClip incorrectaudio;
    public AudioClip correct;
    public AudioClip tryAgain;
    [HideInInspector]
	public List<AudioClip> sounds;
	public List<AudioClip> basicSounds;

    [HideInInspector]
    public List<LevelSounds> levelSoundClips;
    public GameObject inputBlocker;
    public bool accessibilityBlocker;

    [HideInInspector]
    public AudioSource audioPlayer;
  
    // Use this for initialization
    private void Awake()
    {
		audioPlayer = this.GetComponent<AudioSource> ();
        
    }
    public IEnumerator playcorrectAudio()
    {
        audioPlayer.clip = correctaudio;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public IEnumerator playIncorrectAudio()
    {
        audioPlayer.clip = incorrectaudio;
        yield return (StartCoroutine(_playCurrentClip())); 

    }
    public IEnumerator correctAudio()
    {
        audioPlayer.clip = correct;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public IEnumerator tryAgainAudio()
    {
        audioPlayer.clip = tryAgain;
        yield return (StartCoroutine(_playCurrentClip())); 

    }
    public IEnumerator PlaySoundOfSelectedItems(int index, int i){
       audioPlayer.clip = levelSoundClips[index].selectedItemSounds[i];
        yield return (StartCoroutine(_playCurrentClip()));
    }

    public void PlaySound(int index)
	{
        
        audioPlayer.clip = sounds[index];
        audioPlayer.Play();
     //   Debug.Log("In PlaySound");
        //StartCoroutine(_playSound(index));
    }
   public IEnumerator PlayHighlight_1(int index)
	{
        audioPlayer.clip = levelSoundClips[index].highlight_1;
        yield return (StartCoroutine(_playCurrentClip()));
    }
    public IEnumerator PlayHighlight_2(int index)
	{

        audioPlayer.clip = levelSoundClips[index].highlight_2;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public IEnumerator PlayOption_1(int index)
	{
        audioPlayer.clip = levelSoundClips[index].option_1;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public IEnumerator PlayOption_2(int index)
	{
        audioPlayer.clip = levelSoundClips[index].option_2;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public IEnumerator PlayOption_3(int index)
	{
        audioPlayer.clip = levelSoundClips[index].option_3;
        yield return (StartCoroutine(_playCurrentClip())); 
    }
    public AudioClip getCurrentSelection(string tagName){
        if (tagName.Contains("FR"))
        {
            return FRIncorrectSound;

        }
        else if (tagName.Contains("cupcakes"))
        {
            return cupcakesIncorrectSound;
        }
        else if (tagName.Contains("brownies"))
        {
            return browniesIncorrectSound;
        }
        else
        {
            return orderFormSound;

        }
    }
    public void PlaySoundWithAudioClip(AudioClip _clip)
    {
        StartCoroutine(_playSoundWithAudioClip(_clip));
        

    }
    public IEnumerator _playSound(int index)
    {
        inputBlocker.SetActive(true);
        audioPlayer.clip = sounds[index];
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
     public IEnumerator _playCurrentClip()
    {
        inputBlocker.SetActive(true);
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        inputBlocker.SetActive(false);
    }
    
    public IEnumerator playInitialAudio(){
        inputBlocker.SetActive(true);
        audioPlayer.clip=initialAudio;
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        inputBlocker.SetActive(false);

    }

    public IEnumerator _playCurrentTagitem(string tag)
    {
       
        inputBlocker.SetActive(true);
        audioPlayer.Play();
        yield return new WaitForSeconds(audioPlayer.clip.length);
        inputBlocker.SetActive(false);
    }
   
   
}
public class LevelSounds
{
    public List<AudioClip> selectedItemSounds;

    public AudioClip highlight_1;
    public AudioClip highlight_2;
    public AudioClip option_1;
    public AudioClip option_2;
    public AudioClip option_3;
}