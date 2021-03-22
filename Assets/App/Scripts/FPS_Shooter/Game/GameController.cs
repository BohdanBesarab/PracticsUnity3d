using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using  UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("Game")]public Player player;
    public GameObject enemyContainer;
    
    [Header("UI")] public Text healthText;
    public Text ammoText;
    public Text enemyText;
    public Text infoText;
    

    private bool gameOver = false;
    private float resetTimer = 3f;
    private void Start()
    {
        infoText.gameObject.SetActive(false);
    }

    void Update()
    {
        healthText.text = "Health :" + " " + player.health;
        ammoText.text = "Ammo :" + " " + player.Ammo;
        int aliveEnemies = 0;
        foreach (Enemy  enemy in enemyContainer.GetComponentsInChildren<Enemy>())
        {
            if ( enemy.Killed ==false)
            {
                aliveEnemies++;
            }
        }
        enemyText.text = "Enemy : " + " " + aliveEnemies;
        gameOver = true;
        if (aliveEnemies == 0)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You Win!\nGood job!";
        }

        if (player.killed == true)
        {
            infoText.gameObject.SetActive(true);
            infoText.text = "You lose!\nTry again!";


            if (gameOver == true)
            {
                resetTimer -= Time.deltaTime;
                if (resetTimer <= 0)
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }
}
