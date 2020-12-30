using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class GetAudio
{
    public string mp3FileUrl { get; set; }
    public object timingFileUrl { get; set; }
    public List<int> timings { get; set; }
}

public class TextToSpeech : MonoBehaviour
{
    public bool LocalTTS;//false for main n2y build , true for local build 	
    public bool N2YTTS;

    public string apiLink;
    public string apiKey;
    public List<string> textsToConvert;
    public List<AudioClip> downloadedClips;

    [HideInInspector]
    public int AudioDownloaded;
    [HideInInspector]
    public int TotalAudioToDownload;

    // Start is called before the first frame update
    void Start()
    {
        TotalAudioToDownload = textsToConvert.Count-1;
        if (LocalTTS)
        {
            StartCoroutine(GetLocalDownlaodedAudioFiles());
        }
        else if (N2YTTS)//Else get it from the N2Y server
        {
            StartCoroutine(DownloadSoundClipsFromN2Y());
        }
    }

    IEnumerator DownloadSoundClipsFromN2Y()
    {
        yield return StartCoroutine(Download());
    }

    IEnumerator Download()
    {
        for (int i = 0; i < textsToConvert.Count; i++)
        {
            Debug.Log("Request for \"" + textsToConvert[i] + "\" is sent");
            Regex rg = new Regex("\\s+");
            string result = rg.Replace(textsToConvert[i], "+");

            print("TextToSpeech->ExternalRef.BaseUrl: " + Toolbox.ExternalHandler.baseURL);
            string url = Toolbox.ExternalHandler.baseURL + "api/speechapi/GetDynamicSpeechData?text=" + result + "&speed=30&volume=90&speechLanguage=en";//n2y server
            print(url);

            WWW ww = new WWW(url);
            yield return ww;
            Debug.Log("#Audio downloaded : " + ww.text);
            GetAudio Audio = JsonConvert.DeserializeObject<GetAudio>(ww.text);
            string NewUrl = Audio.mp3FileUrl;
            WWW wwNew = new WWW(NewUrl);
            yield return wwNew;
            Debug.Log("Done waiting now storing audio " + NewUrl + " " + wwNew.text);
            Toolbox.SoundManager.sounds.Add(wwNew.GetAudioClip(false, false, AudioType.MPEG));
            AudioDownloaded++;
        }
    }

    IEnumerator GetLocalDownlaodedAudioFiles()
    {
        string pathForAudioClips = "file://" + Application.persistentDataPath + "/DownloadedAudioClips/";
        Debug.Log(pathForAudioClips);
        for (int i = 0; i < textsToConvert.Count; i++)
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
                    Debug.Log("Didnt Find :" + textsToConvert[i]+". Downloading it right now");
                    StartCoroutine(TextToSpeechFunc(textsToConvert[i]));
                }
                else
                {
                    AudioClip tempClip = DownloadHandlerAudioClip.GetContent(www);
                    tempClip.name = textsToConvert[i];
                    Toolbox.SoundManager.sounds.Add(tempClip);
                }
            }
        }
    }

    IEnumerator TextToSpeechFunc(string tempTextToConvert)
    {
        //http://www.voicerss.org/api/// For further understanding of parametres.
        //---------------BaseLink--  ApiKey  ---------TextToConvert--------------Language=Eng-Aust,Voicer=Evie,Codec=OGG,Rate=//
        string finalLink = apiLink + "key=" + apiKey + "&" + "src=" + tempTextToConvert + "&hl=en-us&v=Amy&c=WAV&r=0";
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(finalLink, AudioType.WAV))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                AudioClip downloadClip = DownloadHandlerAudioClip.GetContent(www);
                downloadClip.name = tempTextToConvert;
                SavWav.Save(downloadClip.name, downloadClip);
                Toolbox.SoundManager.sounds.Add(downloadClip);

            }
        }
    }

}

