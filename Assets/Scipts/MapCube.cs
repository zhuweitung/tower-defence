using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;//保存mapcube上的炮台
    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
    }
}
