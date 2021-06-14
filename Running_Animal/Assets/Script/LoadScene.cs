using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void OnShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OnCharacter()
    {
        SceneManager.LoadScene("Character");
    }

    public void OnMission()
    {
        SceneManager.LoadScene("Mission");
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Play");
    }

}
