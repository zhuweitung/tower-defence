using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    //当前选择的炮塔
    private TurretData selectedTurretData;//当前UI面板上选择的要建造的炮台
    private GameObject selectedTurretGo;//当前点击了的已建造在游戏中的炮台
    public Text moneyText;
    private int money = 1000;//初始金钱
    public Animator moneyAnimator;
    public GameObject upgradeCanvas;
    public Button buttonUpgrade;
    private Animator upgradeCanvasAnimator;

    void Start()
    {
        upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }
    void ChangeMoney(int change)
    {
        money += change;
        moneyText.text = "￥" + money;
    }

    void Update()
    {
        //当点击鼠标左键时
        if (Input.GetMouseButtonDown(0))
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
                    //GameObject mapCube = hit.collider.gameObject;
                    MapCube mapcube = hit.collider.GetComponent<MapCube>();
                    //Debug.Log(mapcube.name);
                    //mapcube上没有炮台
                    if (selectedTurretData != null && mapcube.turretGo == null)
                    {
                        //金钱足够建造
                        if (money > selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapcube.BuildTurret(selectedTurretData.turretPrefab);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else if (mapcube.turretGo != null)
                    {
                        //升级炮台
                        if (mapcube.turretGo == selectedTurretGo&&upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                            //HideUpgradeUI();
                        }
                        else
                        {
                            ShowUpgradeUI(mapcube.transform.position, mapcube.isUpgraded);
                        }
                        selectedTurretGo = mapcube.turretGo;
                    }
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
    void ShowUpgradeUI(Vector3 pos,bool isDisableUpgrade=false)
    {
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }
    IEnumerator HideUpgradeUI()
    {
        upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        upgradeCanvas.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {

    }
    public void OnDestoryButtonDown()
    {

    }
}
