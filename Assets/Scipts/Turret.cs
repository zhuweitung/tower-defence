using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private List<GameObject> enemys = new List<GameObject>();//存储进入攻击范围的敌人
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
    public Transform head;//炮台头部引用
    public bool useLaser = false;
    public LineRenderer laserRenderer;
    public float damageRate=70;//激光的持续伤害
    public GameObject laserEffect;//激光掉血特效

    void Start()
    {
        timer = attackRate;
    }

    void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (!useLaser)
        {
            timer += Time.deltaTime;
            if (enemys.Count > 0 && timer >= attackRate)
            {
                timer = 0;
                Attack();
            }
        }
        else if(enemys.Count>0)
        {
            if (laserRenderer.enabled == false) laserRenderer.enabled = true;
            laserEffect.SetActive(true);
            if (enemys[0] == null)
            {
                UpdateEnemys();
            }
            if (enemys.Count > 0)
            {
                laserRenderer.SetPositions(new Vector3[] { firePosition.position, enemys[0].transform.position });
                enemys[0].GetComponent<EnemyMove>().TakeDamage(damageRate*Time.deltaTime);
                laserEffect.transform.position = enemys[0].transform.position;
                Vector3 pos = transform.position;
                pos.y = enemys[0].transform.position.y;
                laserEffect.transform.LookAt(pos);
            }
        }
        else
        {
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
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
