using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using PlayerScripts;

public class ScnManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject deathUI;

    private Player player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        deathUI = GameObject.Find("DeathUI");

        deathUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            playerDied();
        }
    }

    void playerDied()
    {
        deathUI.SetActive(true);
        isPaused = true;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {

    }

    public void QuitGame()
    {
        Debug.Log("aaaah coooome ooooon");
        Application.Quit();
    }
}
