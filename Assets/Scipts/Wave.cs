using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
//保存每一波敌人生成所需要的属性
public class Wave {
    public GameObject enemyPrefab;
    public int count;//数量
    public float rate;//间隔时间
}
