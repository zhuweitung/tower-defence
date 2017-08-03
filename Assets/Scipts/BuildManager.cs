using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildManager : MonoBehaviour {
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    //当前选择的炮塔
    public TurretData selectedTurretData;

    void Update()
    {
        //当点击鼠标左键时
        if (Input.GetMouseButton(0))
        {
            //当点击的不是ui时
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                //炮台建造
                //射线碰撞检测
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //得到点击的mapCube
                    GameObject mapCube = hit.collider.gameObject;
                    Debug.Log(mapCube.name);
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn){
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }
    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }
}
