using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float speed = 10;
    private Transform[] positions;//拐点集合
    private int index = 0;
	// Use this for initialization
	void Start () {
        positions = WayPoints.positions;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    void Move()
    {
        if (index > positions.Length - 1)
        {
            ReachEnd();
            return;//到达终点
        }
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position)<0.2f)
        {
            index++;
        }
    }
    void ReachEnd()
    {
        GameObject.Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        EnemySpawner.countEnemyAlive--;
    }
    public void TakeDamage(int damage)
    {

    }
}
