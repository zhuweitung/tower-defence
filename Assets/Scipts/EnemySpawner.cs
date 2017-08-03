using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public Wave[] waves;
    public Transform start;
    public float waveRate = 3;//每一波之间的时间间隔
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemy());
	}
    IEnumerator SpawnEnemy()
    {
        foreach(Wave wave in waves)
        {
            for (int i = 0; i < wave.count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab,start.position,Quaternion.identity);
                if (i != wave.count - 1)
                    yield return new WaitForSeconds(wave.rate);
            }
            yield return new WaitForSeconds(waveRate);
        }
    }
}
