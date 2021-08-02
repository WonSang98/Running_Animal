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
        SceneManager.LoadScene("Main");
        GameManager.Instance.Load();
    }

    public void OnTalent()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Talent");
        GameManager.Instance.Load();
    }

    public void OnGameOver()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("End_Game");
        GameManager.Instance.Load();
    }

    public void OnPreItem()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene("Pre_Item");
        GameManager.Instance.Load();
    }
}
