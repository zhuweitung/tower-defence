﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public static int countEnemyAlive = 0;
    public Wave[] waves;
    public Transform start;
    public float waveRate = 1;// 每一波之间的时间间隔
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
    }
}
