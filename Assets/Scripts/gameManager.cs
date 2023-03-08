using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameManager : MonoBehaviour
{
    public GameObject bounds;
    float camSpeed = 5;
    public static bool firstRope = false;
    float camSpace=20;
    public GameObject bluePre, redPre, greenPre;
    public GameObject []prefabArray;
    float originalStringYPos = -5.5f;
    int levelIncrement = 0;
    bool recentCamera = false;
    public static bool isCamDone=false;
    public GameObject green, blue, red;
    public GameObject[] stringArray;
    public GameObject greenB, redB, blueB;
    public GameObject[] bArray;
    int extLength = 15;
    public GameObject gReturn, bReturn, rReturn;
    public GameObject[] returnArray;

    public static GameObject[] oldArray;
    public static GameObject[] newArray;

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
        prefabArray = new GameObject[] { greenPre, bluePre, redPre };

        oldArray = new GameObject[] { green, blue, red };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cam.GetComponent<Rigidbody>().velocity = new Vector3(0, camSpeed, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            cam.GetComponent<Rigidbody>().velocity = new Vector3(0, -camSpeed, 0);
        }
        else
        {
            cam.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }
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
            for (int i = 0; i < oldArray.Length; i++)
            {
                print(oldArray.Length);
                GameObject newRope = Instantiate(prefabArray[i], new Vector3(oldArray[i].transform.Find("Hook").transform.position.x, cam.transform.position.y, 0), Quaternion.identity);
                oldArray[i] = newRope;
            }
            greenB.GetComponent<Follow>().rope = oldArray[0].transform.Find("Hook").gameObject;
            blueB.GetComponent<Follow>().rope = oldArray[1].transform.Find("Hook").gameObject;
            redB.GetComponent<Follow>().rope = oldArray[2].transform.Find("Hook").gameObject;


            redB.transform.position = oldArray[2].transform.Find("Hook").gameObject.transform.position;
            greenB.transform.position = oldArray[0].transform.Find("Hook").gameObject.transform.position;
            blueB.transform.position = oldArray[1].transform.Find("Hook").gameObject.transform.position;

        }
    }
    public void ConnectLink(GameObject connector, GameObject connectee)
    {
        connectee.GetComponent<Rigidbody>().transform.position = connector.transform.position;
        connectee.GetComponent<Rigidbody>().isKinematic = false;
    }
}
