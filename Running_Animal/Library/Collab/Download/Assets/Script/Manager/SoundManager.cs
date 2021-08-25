using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager
{

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    AudioMixer mixer;
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    AudioClip[] BGMList;

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        BGMList = Resources.LoadAll<AudioClip>("Sound/BGM");
        mixer = Resources.Load<AudioMixer>("Sound/SoundMixer");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
    }

    public void SFXPlay(AudioClip clip)
    {
        AudioSource audioSource = _audioSources[(int)Sound.Effect];
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        audioSource.PlayOneShot(clip);
    }

    public void BGMPlay(AudioClip clip)
    {
        AudioSource audioSource = _audioSources[(int)Sound.Bgm];
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void BGMClear()
    {
        _audioSources[0].clip = null;
        _audioSources[0].Stop();
    }


}



