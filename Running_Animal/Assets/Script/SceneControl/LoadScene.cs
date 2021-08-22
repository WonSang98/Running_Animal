using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void OnShop()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Shop");
        GameManager.Instance.Load();
    }

    public void OnCharacter()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Character");
        GameManager.Instance.Load();
    }

    public void OnMission()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Mission");
        GameManager.Instance.Load();
    }

    public void OnStart()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Play");
        GameManager.Instance.Load();
    }
    public void OnMain()
    {
        GameManager.Instance.Save();
        GameManager.Instance.AllStop();
        SceneManager.LoadScene("Main");
        GameManager.Instance.Load();
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.AllStop();
        gameObject.GetComponent<SetPlayer>().Re_Stat();
        SceneManager.LoadScene("Main");
    }

    public void OnTalent()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Talent");
        GameManager.Instance.Load();
    }

    public void OnPreItem()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Pre_Item");
        GameManager.Instance.Load();
    }

    public void Stop_LoadScene()
    {
        StopAllCoroutines();
    }
}
