using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static int countEnemyAlive = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 1;// 每一波之间的时间间隔
    private Coroutine coroutine;

	void Start () {
        coroutine= StartCoroutine(SpawnEnemy());
	}
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab,start.position,Quaternion.identity);
                countEnemyAlive++;
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (countEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        //所有生成的敌人均已消灭
        while (countEnemyAlive > 0)
        {
            yield return 0;
        }
        //游戏胜利
        Game_Manager.Instance.Win();
    }
    public void Stop()
    {
        //StopCoroutine("SpawnEnemy");
        StopCoroutine(coroutine);
    }
}
