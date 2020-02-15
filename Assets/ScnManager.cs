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

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        deathUI = GameObject.Find("DeathUI");
        murderOath = GameObject.Find("MurderousGod").GetComponentInChildren<MurderOath>();

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
        murderOath.countEnemies();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("aaaah coooome ooooon");
        Application.Quit();
    }
}
