using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using PlayerScripts;
using PlayerScripts.oaths;

public class ScnManager : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject deathUI;

    private Player player;
    private MurderOath murderOath;
    private int level;

    private void Start()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Player>();
        deathUI = GameObject.Find("DeathUI");
        murderOath = GameObject.Find("MurderousGod").GetComponentInChildren<MurderOath>();

        deathUI.SetActive(false);

        if(PlayerPrefs.GetInt("Level", 0) == 0)
        {
            level = 0;
        }
        else
        {
            level = PlayerPrefs.GetInt("Level");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead)
        {
            playerDied();
        }
        if (player.nextLevel)
        {
            NextLevel();
        }
    }

    void playerDied()
    {
        deathUI.SetActive(true);
        isPaused = true;
    }

    public void ReplayGame()
    {
        Debug.Log("Replay");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        murderOath.countEnemies();
        Debug.Log(level);
        if (level == 2)
        {
            Debug.Log("Finish go home");
            MainMenu();
        }
        level++;
        PlayerPrefs.SetInt("Level", level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("aaaah coooome ooooon");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }
}
