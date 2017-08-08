using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    [HideInInspector]
    public GameObject turretGo;//保存mapcube上的炮台
    public GameObject buildEffect;//建造炮台时的粒子特效
    private Renderer renderer;
    private Color originalColor;
    [HideInInspector]
    public bool isUpgraded = false;//mapcube上的炮台是否升级过
    [HideInInspector]
    public  TurretData turretData;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }
    public void BuildTurret(TurretData  turretData)
    {
        this.turretData = turretData;
        isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.red;
        }
    }
    void OnMouseExit()
    {
        renderer.material.color = originalColor;
    }
    //升级炮台
    public void UpgradeTurret()
    {
        if (isUpgraded) return;
        Destroy(turretGo);
        isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
    //拆除炮台
    public void DestoryTurret()
    {
        Destroy(turretGo);
        isUpgraded = false;
        turretGo = null;
        turretData = null;
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }
}
