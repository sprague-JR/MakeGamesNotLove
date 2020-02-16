using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu_Buttons : MonoBehaviour
{

    public void change_Scene(string newscene)
    {
        PlayerPrefs.SetInt("Level", 0);
        SceneManager.LoadScene(newscene);
    }
    public void Quit()
    {
        Debug.Log("should quit");
        Application.Quit();
    }
}
