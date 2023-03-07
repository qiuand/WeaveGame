using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    float originalStringYPos = -5.5f;
    int levelIncrement = 0;
    bool recentCamera = false;
    public static bool isCamDone=false;
    public GameObject green, blue, red;
    public GameObject[] stringArray;
    public GameObject greenB, redB, blueB;
    public GameObject[] bArray;
    int extLength = 30;
    public GameObject gReturn, bReturn, rReturn;
    public GameObject[] returnArray;
    public static float went;
    public static float rb, rg, bg;
    [SerializeField] GameObject stats;
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        bArray = new GameObject[] { greenB, blueB, redB };
        stringArray = new GameObject[] { green, blue, red };
        returnArray = new GameObject[] { gReturn, bReturn, rReturn };
    }

    // Update is called once per frame
    void Update()
    {
        stats.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>Red</color>&<color=blue>Blue</color>: " + rb + "<br><color=red>Red</color>&<color=green>Green</color>: " + rg + "<br><color=blue>Blue</color>&<color=green>Green</color>: " + bg;
        if (went >= 3)
        {
            levelIncrement++;
            isCamDone = false;
            recentCamera = true;
            went = 0;
            StartCoroutine(cam.GetComponent<camControl>().CamLerp());
        }
        if(isCamDone && recentCamera)
        {
            recentCamera = false;
            for (int i = 0; i < stringArray.Length; i++)
            {
                stringArray[i].transform.position = new Vector3(stringArray[i].transform.position.x, 1f, stringArray[i].transform.position.z);
                stringArray[i].GetComponent<HitScript>().isStringHit = false;
                for (int j = 0; j < extLength; j++)
                {
                    stringArray[i].GetComponent<rope>().addSeg();
                }
            }
            redB.transform.position = GameObject.Find("rHook").transform.position;
            greenB.transform.position = GameObject.Find("gHook").transform.position;
            blueB.transform.position = GameObject.Find("bHook").transform.position;

        }
    }
}
