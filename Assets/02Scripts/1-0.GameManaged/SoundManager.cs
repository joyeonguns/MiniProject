using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum Sound {Bgm, Effect, MaxCount,};

public class SoundManager : MonoBehaviour
{
    public int currentMusic;
    public static SoundManager instance;
    public AudioClip[] bgmList;
    public AudioSource bgSound;
    AudioClip beforSceneClip;

    public AudioMixerGroup EffectMixer;

    public AudioMixer audioMixer;

    private void Awake() {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            this.gameObject.SetActive(true);

            bgSound = gameObject.GetComponent<AudioSource>();
                      
        }
        else if (instance != this)
        {
            //instance = null;
            Destroy(this.gameObject);
        }

        beforSceneClip = bgmList[bgmList.Length - 1];
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {        
        for(int i = 0; i < bgmList.Length; i++)
        {
            if(arg0.buildIndex == i && beforSceneClip != bgmList[i])
            {
                BGMPlay(bgmList[i]);
                beforSceneClip = bgmList[i];
                currentMusic = i;
                break;
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "_Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = EffectMixer;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BGMPlay(AudioClip clip)
    {        
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();        
    }

    public void SetAudio(float bgm, float effect)
    {
        audioMixer.SetFloat("BGSound",Mathf.Log(bgm,10) * 20);
        audioMixer.SetFloat("SFXSound",Mathf.Log(effect,10) * 20);
    }
}
