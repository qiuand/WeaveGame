using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    public Sprite i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, i11, i12, i13, i14, i15;
    public GameObject exposit;
    public Sprite brImg, bgImg, grImg;
    public GameObject storyImg;
    AudioSource src;
    public GameObject diagCon;

    bool completeTrigger = false;

    public AudioClip ding;

    public GameObject containStory;

    public GameObject newTextie;
    public GameObject narrativeCrawl;
    public static string triggerType = "";
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

    public GameObject diagText;

    public static GameObject[] oldArray;
    public static GameObject[] newArray;
    GameObject[] rArray;

    public static float went;
    public static float rb, rg, bg;
    [SerializeField] GameObject stats;
    [SerializeField] GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        rArray=new GameObject[] {gReturn, bReturn, rReturn};
        bArray = new GameObject[] { greenB, blueB, redB };
        stringArray = new GameObject[] { green, blue, red };
        returnArray = new GameObject[] { gReturn, bReturn, rReturn };
        prefabArray = new GameObject[] { greenPre, bluePre, redPre };

        oldArray = new GameObject[] { green, blue, red };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            narrativeCrawl.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Select");
        }
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
            exposit.SetActive(false);
            if (!completeTrigger)
            {
                switch (triggerType)
                {
                    case "br":
                        diagCon.GetComponent<DialogueViewer>().OnNodeSelected(0);
                        newTextie.GetComponent<TMPro.TextMeshProUGUI>().text = diagText.GetComponent<TMPro.TextMeshProUGUI>().text+"<br><color=green>Press Space to Continue";
                        break;
                    case "bg":
                        diagCon.GetComponent<DialogueViewer>().OnNodeSelected(1);
                        newTextie.GetComponent<TMPro.TextMeshProUGUI>().text = diagText.GetComponent<TMPro.TextMeshProUGUI>().text + "<br><color=green>Press Space to Continue";
                        break;
                    case "gr":
                        diagCon.GetComponent<DialogueViewer>().OnNodeSelected(2);
                        newTextie.GetComponent<TMPro.TextMeshProUGUI>().text = diagText.GetComponent<TMPro.TextMeshProUGUI>().text + "<br><color=green>Press Space to Continue";
                        break;
                }

                storyImg.SetActive(true);
                containStory.GetComponent<Animator>().Play("Thing");
                containStory.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, containStory.transform.position.z);
/*                storyImg.transform.position = new Vector3(cam.transform.position.x+15, cam.transform.position.y+10, newTextie.transform.position.z);
                newTextie.transform.position = new Vector3(cam.transform.position.x+15, cam.transform.position.y-4, newTextie.transform.position.z);*/

                completeTrigger = true;
            }
            levelIncrement++;
            if (Input.GetKeyDown(KeyCode.Space)){
                StartCoroutine(cam.GetComponent<camControl>().CamLerp());
                went = 0;
                isCamDone = false;
                recentCamera = true;
                src.PlayOneShot(ding);

                triggerType = "";
            }
            
        }
        if(isCamDone && recentCamera)
        {
            recentCamera = false;
            for (int i = 0; i < oldArray.Length; i++)
            {
                print(oldArray.Length);
                GameObject newRope = Instantiate(prefabArray[i], new Vector3(oldArray[i].transform.Find("Hook").transform.position.x/*rArray[i].transform.position.x*/, cam.transform.position.y, 0), Quaternion.identity);
                oldArray[i] = newRope;
                newRope.transform.position = new Vector3(rArray[i].transform.position.x, newRope.transform.position.y, newRope.transform.position.z);
            }

            greenB.GetComponent<Follow>().rope = oldArray[0].transform.Find("Hook").gameObject;
            blueB.GetComponent<Follow>().rope = oldArray[1].transform.Find("Hook").gameObject;
            redB.GetComponent<Follow>().rope = oldArray[2].transform.Find("Hook").gameObject;


            redB.transform.position = oldArray[2].transform.Find("Hook").gameObject.transform.position;
            redB.transform.position = new Vector3(redB.transform.position.x, redB.transform.position.y, -260);
            greenB.transform.position = oldArray[0].transform.Find("Hook").gameObject.transform.position;
            greenB.transform.position = new Vector3(greenB.transform.position.x, greenB.transform.position.y, -260);
            blueB.transform.position = oldArray[1].transform.Find("Hook").gameObject.transform.position;
            blueB.transform.position = new Vector3(blueB.transform.position.x, blueB.transform.position.y, -260);
            /*            newTextie.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y-10, newTextie.transform.position.z);
            */            /*storyImg.SetActive(true);
                        storyImg.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, newTextie.transform.position.z);*/
            containStory.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, containStory.transform.position.z);

            completeTrigger = false;
        }
    }
    public void ConnectLink(GameObject connector, GameObject connectee)
    {
        connectee.GetComponent<Rigidbody>().transform.position = connector.transform.position;
        connectee.GetComponent<Rigidbody>().isKinematic = false;
    }
}
