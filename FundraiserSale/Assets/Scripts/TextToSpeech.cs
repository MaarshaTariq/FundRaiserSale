using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TextToSpeech : MonoBehaviour
{
    public bool LocalTTS;//false for main n2y build , true for local build 	

    public string localTestApiLink;
    public string localTestApiKey;
    public List<string>textsToConvert;
    public List<AudioClip> downloadedClips;

    [HideInInspector]
    public int AudioDownloaded;
    [HideInInspector]
    public int TotalAudioToDownload;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetLocalDownlaodedAudioFiles());
        StartCoroutine(tempPlaySound());

       
    }
    IEnumerator  tempPlaySound()
    {
        

        yield return new WaitForEndOfFrame();
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
                else if (www.isHttpError)//Didnt find it in Local Storage on the given path.
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
        //http://www.voicerss.org/api/// For further understanding of parametres.
        //---------------BaseLink--  ApiKey  ---------TextToConvert--------------Language=Eng-Aust,Voicer=Evie,Codec=OGG,Rate=//
        string finalLink = localTestApiLink + "key=" + localTestApiKey + "&" + "src=" + tempTextToConvert + "&hl=en-us&v=Amy&c=WAV&r=0";
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

