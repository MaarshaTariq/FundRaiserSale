using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    public string apiLink;
    public string apiKey;
    public List<string>textsToConvert;
    public List<AudioClip> downloadedClips;

    // Start is called before the first frame update
    void Start()
    {
        //GetDownlaodedAudioFiles();
        StartCoroutine(GetLocalDownlaodedAudioFiles());
    }

    IEnumerator GetLocalDownlaodedAudioFiles()
    {
        //Working for now
        string pathForAudioClips = "file://"+Application.persistentDataPath + "/DownloadedAudioClips/";

        Debug.Log(pathForAudioClips);
        for (int i =0;i<textsToConvert.Count;i++)
        {

            using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(pathForAudioClips + textsToConvert[i] + ".wav", AudioType.WAV))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else if (www.isHttpError)
                {
                    Debug.Log("Didnt Find :" + textsToConvert[i]);
                    StartCoroutine(TextToSpeechFunc(textsToConvert[i]));
                }
                else
                {
                    AudioClip tempClip = DownloadHandlerAudioClip.GetContent(www);
                    tempClip.name = textsToConvert[i];
                    downloadedClips.Add(tempClip);
                }
            }
        }

        
    }

    IEnumerator TextToSpeechFunc(string tempTextToConvert)
    {
        #region previousLogic
        //WWWForm form = new WWWForm();
        //form.AddField("key", "8e06f32cb078436c9c0c881bf507f647");
        //form.AddField("src", "Helloooooooo is it me you are looking forrrrrr!");
        //form.AddField("hl", "en-au");
        //form.AddField("v", "Evie");
        //form.AddField("c", "MP3");

        //UnityWebRequest www = UnityWebRequest.Get(apiLink);
        //yield return www.SendWebRequest();

        //if (www.isNetworkError || www.isHttpError)
        //{
        //    Debug.Log(www.error);
        //}
        //else
        //{
        //    Debug.Log("Form upload complete!");
        //    Debug.Log(www.responseCode);
        //    Debug.Log(www.);

        //    AudioClip weather_1 = DownloadHandlerAudioClip.GetContent(www);
        //    clipPlayer.clip = weather_1;
        //    clipPlayer.Play();
        //}
        #endregion

        //http://www.voicerss.org/api/// For further understanding of parametres.
        //---------------BaseLink--  ApiKey  ---------TextToConvert--------------Language=Eng-Aust,Voicer=Evie,Codec=OGG,Rate=//
        string finalLink = apiLink + "key=" + apiKey + "&" + "src=" + tempTextToConvert + "&hl=en-us&v=Amy&c=WAV&r=0";
        //Debug.Log("Sending Link : "+finalLink);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(finalLink, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                //Debug.Log(www.downloadHandler.data.ToString());
                AudioClip downloadClip = DownloadHandlerAudioClip.GetContent(www);
                downloadClip.name = tempTextToConvert;
                SavWav.Save(downloadClip.name,downloadClip);
                downloadedClips.Add(downloadClip);
                
            }
        }
    }

}

