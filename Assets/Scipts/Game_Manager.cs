using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public GameObject endUI;
    public Text endMessage;
    public static Game_Manager Instance;
    private EnemySpawner enemySpawner;
    void Awake()
    {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "胜 利";
    }
    public void Failed()
    {
        enemySpawner.Stop();
        endUI.SetActive(true);
        endMessage.text="失 败";
    }
    public void OnButtonRetryDown()
    {
        //重载游戏场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnButtonMenuDown()
    {
        SceneManager.LoadScene(0);
    }
}
