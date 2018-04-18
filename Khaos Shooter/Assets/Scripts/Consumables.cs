using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Consumables : MonoBehaviour
{

    public Slider fuelSlider;
    public Slider ammoSlider;

    private Text fuelSliderText;
    private Text ammoSliderText;
    private CommonProc commonProc;
    private Globals globals;


    private void Start()
    {
        GameObject lm = GameObject.Find("LevelManager");
        commonProc = lm.GetComponent<CommonProc>();
        globals = lm.GetComponent<Globals>();

        fuelSliderText = fuelSlider.GetComponentInChildren<Text>();
        ammoSliderText = ammoSlider.GetComponentInChildren<Text>();
    }

    private bool CantCreate(Vector3 pos)
    {
        return false;
        //GameObject asteriod = FindClosestAsteriod(pos.y);
        //return (asteriod != null && asteriod.transform.position.x > pos.x && asteriod.transform.position.x < pos.x + 100);
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
        fuelSlider.value = Mathf.Clamp01(globals.currentFuel / 1000f);
        ammoSlider.value = Mathf.Clamp01(globals.currentAmmo / 50f);
        fuelSliderText.text = string.Format("{0:0}%", fuelSlider.value * 100f);
        ammoSliderText.text = string.Format("{0:0}%", ammoSlider.value * 100f);
    }

}