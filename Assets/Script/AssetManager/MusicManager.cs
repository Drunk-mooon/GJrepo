using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Audio;

public static class MusicManager
{
    private static GameObject _musicManagerObject;
    private static bool _isInit = false;
    private static Dictionary<string, AudioSource> _msDic = new Dictionary<string, AudioSource>();

    private static void Init()
    {
        if (_musicManagerObject == null)
        {
            _musicManagerObject = new GameObject("SoundManager");
            _musicManagerObject.transform.position = Camera.main.transform.position;
            _musicManagerObject.transform.SetParent(Camera.main.transform); 
            _musicManagerObject.AddComponent<FakeMono>();
            Object.DontDestroyOnLoad(_musicManagerObject);
            _isInit = true;
        }
    }

    public static void AddMusic(string fileName, float delay, float volume, GameObject father = null, float minDis = 1, float maxDis = 10) //
    {
        AudioSource audioSource = null;
        if (!_isInit) Init();
        AudioClip clip = AssetManager.LoadData<AudioClip>(fileName);

        if (father == null) audioSource = _musicManagerObject.AddComponent<AudioSource>();
        else audioSource = father.AddComponent<AudioSource>();

        _msDic.Add(fileName, audioSource);
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.volume = Mathf.Clamp01(volume) * SettingData.VolumeData.MusicVol;

        if (father != null)
        {
            audioSource.spatialBlend = 1.0f;
            audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
            audioSource.minDistance = minDis;
            audioSource.maxDistance = maxDis;
        }


        _musicManagerObject.GetComponent<FakeMono>()
            .StartCoroutine(PlayMusicCoroutine(delay, audioSource));
    }

    public static void EndMusic(string fileName)
    {
        if (_msDic.ContainsKey(fileName) && (_msDic[fileName] != null))
            _musicManagerObject.GetComponent<FakeMono>()
                .StartCoroutine(EndMusicCoroutine(fileName, _msDic[fileName]));
    }

    public static void ChangeVolume()
    {
        foreach (AudioSource audioSource in _msDic.Values)
        {
            audioSource.volume *= SettingData.VolumeData.MusicVol;
        }
    }

    private static IEnumerator PlayMusicCoroutine(float delayTime, AudioSource audioSource)
    {
        yield return new WaitForSeconds(delayTime);
        audioSource.Play();
    }

    private static IEnumerator EndMusicCoroutine(string fileName, AudioSource audioSource, float time = 2)
    {
        float startVolume = audioSource.volume; // 
        float timeElapsed = 0f;
        while (timeElapsed < time)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, timeElapsed / time); // 
            timeElapsed += Time.deltaTime;
            yield return null; // 
        }

        audioSource.volume = 0f; // 
        _msDic.Remove(fileName);
    }

    private class FakeMono : MonoBehaviour
    {
    }

    public static void Wake()
    {
    }
}

