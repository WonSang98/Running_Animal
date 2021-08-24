using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    AudioClip Clip_BGM;
    AudioClip Clip_BGM2;
    AudioClip Clip_BGM3;
    AudioClip Clip_BGM4;
    AudioClip clip;
    private void Start()
    {
        LoadSound();
    }

    public void OnShop()
    {
        GameManager.Instance.goScene = "Shop";
        GameManager.Instance.Save();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }

    public void OnCharacter()
    {
        GameManager.Instance.goScene = "Character";
        GameManager.Instance.Save();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
        GameManager.Sound.BGMPlay(Clip_BGM3);
    }

    public void OnMission()
    {
        GameManager.Instance.goScene = "Mission";
        GameManager.Instance.Save();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }

    public void OnStart()
    {
        GameManager.Instance.goScene = "Play";
        GameManager.Instance.Save();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
        GameManager.Sound.BGMPlay(Clip_BGM4);
    }
    public void OnMain()
    {
        GameManager.Instance.goScene = "Main";
        GameManager.Instance.Save();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        GameManager.Instance.AllStop();
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }

    public void EndGame()
    {
        GameManager.Instance.goScene = "Main";
        Time.timeScale = 1;
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        GameManager.Instance.AllStop();
        gameObject.GetComponent<SetPlayer>().Re_Stat();
        SceneManager.LoadScene("Loading");
    }

    public void OnTalent()
    {
        GameManager.Instance.goScene = "Talent";
        GameManager.Instance.Save();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }

    public void OnPreItem()
    {
        GameManager.Instance.goScene = "Pre_Item";
        GameManager.Instance.Save();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
        GameManager.Sound.BGMPlay(Clip_BGM2);
    }

    public void OnFail()
    {
        GameManager.Instance.goScene = "End_Fail";
        GameManager.Instance.Save();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }

    public void OnSuccess()
    {
        GameManager.Instance.goScene = "End_Success";
        GameManager.Instance.Save();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Instance.Load();
    }
    public void Stop_LoadScene()
    {
        StopAllCoroutines();
    }

    void LoadSound()
    {
        Clip_BGM = Resources.Load<AudioClip>("Sound/BGM/000_Main_BGM");
        Clip_BGM2 = Resources.Load<AudioClip>("Sound/BGM/001_PreItem_BGM");
        Clip_BGM3 = Resources.Load<AudioClip>("Sound/BGM/002_Character_BGM");
        Clip_BGM4 = Resources.Load<AudioClip>("Sound/BGM/003_Play_BGM");
        clip = Resources.Load<AudioClip>("Sound/Common/000_Manu_Sound");
    }
}
