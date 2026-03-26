//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Events : MonoBehaviour
//{
//    public void Menu()
//    {
//        SaveManager.DeleteSave(); // xóa save khi về menu
//        if (AudioManager.instance != null) AudioManager.instance.PlaySelect();
//        SceneManager.LoadScene(0);
//    }

//    public void Level()
//    {
//        if (AudioManager.instance != null) AudioManager.instance.PlaySelect();
//        SceneManager.LoadScene(1);
//    }

//    public void Retry()
//    {
//        if (AudioManager.instance != null) AudioManager.instance.PlaySelect();
//        SceneManager.LoadScene(1);
//    }

//    public void Quit()
//    {
//        if (AudioManager.instance != null) AudioManager.instance.PlaySelect();
//        Application.Quit();
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void Menu()
    {
        Time.timeScale = 1f; 
        SaveManager.DeleteSave();

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySelect();

        SceneManager.LoadScene("Menu"); 
    }

    public void Level()
    {
        Time.timeScale = 1f; 

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySelect();

        SceneManager.LoadScene("Level1");
        Debug.Log("CLICK LEVEL");
    }

    public void Retry()
    {
        Time.timeScale = 1f; 

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySelect();

        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySelect();

        Application.Quit();
    }
}