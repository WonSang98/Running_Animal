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
    AudioClip clip2;
    AudioClip clip3;
    AudioClip clip4;

    private void Start()
    {
        LoadSound();
    }

    public void OnShop()
    {
        GameManager.Instance.goScene = "Shop";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
    }

    public void OnCharacter()
    {
        GameManager.Instance.goScene = "Character";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Sound.BGMPlay(Clip_BGM3);
    }

    public void OnMission()
    {
        GameManager.Instance.goScene = "Mission";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
    }

    public void OnStart()
    {
        GameManager.Instance.goScene = "Play";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Sound.BGMPlay(Clip_BGM4);
    }
    public void OnMain()
    {
        GameManager.Instance.goScene = "Main";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        GameManager.Instance.AllStop();
        SceneManager.LoadScene("Loading");
    }

    public void EndGame()
    {
        GameManager.Instance.goScene = "Main";
        Time.timeScale = 1;
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        GameManager.Instance.AllStop();
        gameObject.GetComponent<SetPlayer>().Re_Stat();
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
    }

    public void OnTalent()
    {
        GameManager.Instance.goScene = "Talent";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
    }

    public void OnPreItem()
    {
        GameManager.Instance.goScene = "Pre_Item";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Sound.BGMPlay(Clip_BGM2);
    }

    public void OnFail()
    {
        GameManager.Instance.goScene = "End_Fail";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip2);
    }

    public void OnSuccess()
    {
        GameManager.Instance.goScene = "End_Success";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip2);
    }

    public void OnSelect()
    {
        GameManager.Instance.AllStop();
        GameManager.Instance.goScene = "Select_Item";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip3);
    }

    public void OnPlay()
    {
        GameManager.Instance.AllStop();
        GameManager.Instance.goScene = "Play";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip4);
    }
    public void OnStopTutorial()
    {
        GameManager.Data.TutoData.tuto0 = true;
        GameManager.Instance.goScene = "Main";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        Time.timeScale = 1;
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        GameManager.Instance.AllStop();
        GameManager.Instance.GetComponent<SetPlayer>().Re_Stat();
        SceneManager.LoadScene("Loading");
    }
    public void OnTutorial()
    {
        GameManager.Instance.AllStop();
        GameManager.Instance.goScene = "Tutorial";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip4);
    }

    public void OnTutorial2()
    {
        GameManager.Instance.AllStop();
        GameManager.Instance.goScene = "Tutorial2";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip4);
        GameManager.Instance.Load();
    }

    public void OnTutorial3()
    {
        GameManager.Instance.AllStop();
        GameManager.Instance.goScene = "Tutorial3";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        SceneManager.LoadScene("Loading");
        GameManager.Sound.SFXPlay(clip4);
    }

    public void OnRecord()
    {
        GameManager.Instance.goScene = "Record";
        GameManager.Instance.Save();
        GameManager.Instance.Load();
        GameManager.Sound.Clear();
        GameManager.Sound.SFXPlay(clip);
        SceneManager.LoadScene("Loading");
        GameManager.Sound.BGMPlay(Clip_BGM2);
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
        clip2 = Resources.Load<AudioClip>("Sound/Common/002_Paper");
        clip3 = Resources.Load<AudioClip>("Sound/Common/008_Levelup");
        clip4 = Resources.Load<AudioClip>("Sound/Common/007_Stamp");
    }
}
