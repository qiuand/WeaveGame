using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    public static float went;
    public static float rb, rg, bg;
    [SerializeField] GameObject stats;
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stats.GetComponent<TMPro.TextMeshProUGUI>().text = "<color=red>Red</color>&<color=blue>Blue</color>: " + rb + "<br><color=red>Red</color>&<color=green>Green</color>: " + rg + "<br><color=blue>Blue</color>&<color=green>Green</color>: " + bg;
        if (went >= 3)
        {
            went = 0;
            StartCoroutine(cam.GetComponent<camControl>().CamLerp());
        }
    }
}
