﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public List<GameObject> enemys = new List<GameObject>();//存储进入攻击范围的敌人
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Enemy")
        {
            enemys.Remove(col.gameObject);
        }
    }
    public float attackRate = 1;//一秒攻击一次
    private float timer = 0;//计时器
    public GameObject bulletPrefab;//子弹
    public Transform firePosition;

    void Start()
    {
        timer = attackRate;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(enemys.Count>0&&timer>=attackRate)
        {
            timer = 0;
            Attack();
        }
    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
        }
        else
        {
            timer = attackRate;
        }

    }
    void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        int i;
        for (i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null) emptyIndex.Add(i);
        }
        for (i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
}
