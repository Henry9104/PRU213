using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void Menu()
    {
        //AudioManager.instance.PlaySelect(); // sound click
        SceneManager.LoadScene(0);
    }

    public void Level()
    {
        //AudioManager.instance.PlaySelect(); 
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        //AudioManager.instance.PlaySelect(); 
        Application.Quit();
    }
}