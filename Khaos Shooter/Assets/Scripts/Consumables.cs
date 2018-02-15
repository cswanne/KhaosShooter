using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumables : MonoBehaviour
{

    public Transform fuelCanister;
    public Transform ammoBox;
    private Transform fuelClone = null;
    private Transform ammoClone = null;
    private int nextCanisterSeconds = 4;
    public Slider fuelSlider;
    public Slider ammoSlider;

    private Text fuelSliderText;
    private Text ammoSliderText;

    private void Start()
    {
        fuelSliderText = fuelSlider.GetComponentInChildren<Text>();
        ammoSliderText = ammoSlider.GetComponentInChildren<Text>();
    }

    private bool CantCreate(Vector3 pos)
    {
        return false;
        //GameObject asteriod = FindClosestAsteriod(pos.y);
        //return (asteriod != null && asteriod.transform.position.x > pos.x && asteriod.transform.position.x < pos.x + 100);
    }

    private void NewCanister()
    {

        if (fuelClone == null) {
            Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-6, 6), 0);
            //Based on random postion, try not to appear directly in front of ansteriod
            if (CantCreate(pos)) {
                Assistant.canisterDestroyTime = Time.time;
                return;
            }
            Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
            fuelClone = Instantiate(fuelCanister, pos, rot) as Transform;
        }
        if (ammoClone == null) {
            Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-6, 6), 0);
            //Based on random postion, try not to appear directly in front of ansteriod
            if (CantCreate(pos)) {
                Assistant.canisterDestroyTime = Time.time;
                return;
            }
            Quaternion rot = Quaternion.Euler(new Vector3(90, 0, 0));
            ammoClone = Instantiate(ammoBox, pos, rot) as Transform;
        }
    }

    private GameObject FindClosestAsteriod(float positionY)
    {
        GameObject[] asteriods = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject obj in asteriods) {
            float diff = (obj.transform.position.y - positionY);
            Vector3 compare = new Vector3(0, obj.transform.position.y, 0);
            float curDistance = compare.sqrMagnitude;
            if (diff >= -1 && diff <= 1 && curDistance < distance) {
                closest = obj;
                distance = curDistance;
            }
        }
        return closest;
    }

    void Update()
    {
        if (Assistant.gameOver) return;
        if (Assistant.canisterDestroyTime == 0 || Time.time > Assistant.canisterDestroyTime + nextCanisterSeconds) {
            NewCanister();
        }
        fuelSlider.value = Mathf.Clamp01(Assistant.currentFuel / 1000f);
        ammoSlider.value = Mathf.Clamp01(Assistant.currentAmmo / 50f);
        fuelSliderText.text = string.Format("{0:0}%", fuelSlider.value * 100f);
        ammoSliderText.text = string.Format("{0:0}%", ammoSlider.value * 100f);
    }

}