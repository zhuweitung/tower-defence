using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 50;
    public float speed = 40;
    private Transform target;
    public GameObject explosionEffectPrefab;//爆炸特效
    private float distanceToTarget = 1.2f;//子弹爆照时距离目标的距离

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceToTarget)
        {
            target.GetComponent<EnemyMove>().TakeDamage(damage);
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(effect, 1);
        Destroy(this.gameObject);
    }
}
