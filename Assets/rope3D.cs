using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope3D : MonoBehaviour
{
    bool inThing=false;
    float weaveTimer;
    float weaveDuration = 1.0f;
    float mass = 1;
    float ropeTimer;
    float ropeDuration = 1.0f;
    int inactiveChainLength = 10;
    bool weaving = false;
    public Rigidbody hook;
    public GameObject[] prefabRopeSegments;
    public int linkNum = 5;
    List<GameObject> hingeList= new List<GameObject>();

    public HingeJoint top;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
        ropeTimer = ropeDuration;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void GenerateRope()
    {
        float increment = .4f;
        Rigidbody prevBod = hook;
        for (int i = 0; i < linkNum; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSegments[0]);
            hingeList.Add(newSeg);
            newSeg.GetComponent<Rigidbody>().mass = mass;
            newSeg.transform.parent = transform;
            newSeg.transform.position = new Vector3(transform.position.x, transform.position.y + increment, transform.position.z);
            increment-=.4f;
            HingeJoint hj = newSeg.GetComponent<HingeJoint>();
            hj.connectedBody = prevBod;
            if (i == 0)
            {
                top = hj;
                newSeg.gameObject.tag = gameObject.tag;
            }
            StartCoroutine(RedoBody(newSeg));
            prevBod = newSeg.GetComponent<Rigidbody>();
            mass -= 0.0025f;
        }
    }
    public void addSeg()
    {
        GameObject newSeg = Instantiate(prefabRopeSegments[0]);
        newSeg.transform.parent = transform;
        newSeg.transform.position = transform.position;
        HingeJoint hj = newSeg.GetComponent<HingeJoint>();
        hj.connectedBody = hook;
        newSeg.GetComponent<ropeScript3D>().below = top.gameObject;
        top.connectedBody = newSeg.GetComponent<Rigidbody>();
        top.GetComponent<ropeScript3D>().ResetAnchor();
        top = hj;
    }
    IEnumerator RedoBody(GameObject seg)
    {
        yield return new WaitForSeconds(2.0f);
        seg.GetComponent<BoxCollider>().isTrigger = false;
    }
    public void ProcessCollision(Collision collision, GameObject collidee)
    {
        if (!weaving)
        {
            StartCoroutine(WeaveInOut(collision));
        }

        if (!transform.GetComponent<HitScript>().isStringHit && collision.gameObject.transform.parent.GetComponent<HitScript>() != null && !collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit)
        {
            StartCoroutine(SetKinematic());
            print("5");
            if ((collidee.tag == "Green" && collision.gameObject.tag == "Blue") || (collidee.tag == "Blue" && collision.gameObject.tag == "Green"))
            {
                gameManager.went += 2;
                gameManager.bg += 1f;
                gameManager.rg -= 1f;
                gameManager.rb -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
            }
            else if ((collidee.tag == "Green" && collision.gameObject.tag == "Red") || (collidee.tag == "Red" && collision.gameObject.tag == "Green"))
            {
                gameManager.went += 2;
                gameManager.rg += 1f;
                gameManager.bg -= 1f;
                gameManager.rb -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
            }
            else if ((collidee.tag == "Blue" && collision.gameObject.tag == "Red") || (collidee.tag == "Red" && collision.gameObject.tag == "Blue"))
            {
                gameManager.went += 2;
                gameManager.rb += 1f;
                gameManager.bg -= 1f;
                gameManager.rg -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
            }
        }
    }
    public void Skip(GameObject collidee)
    {
        if (!transform.GetComponent<HitScript>().isStringHit)
        {
            gameManager.went += 1;
            transform.GetComponent<HitScript>().isStringHit = true;

            switch (collidee.tag)
            {
                case "Green":
                    gameManager.bg--;
                    gameManager.rg--;
                    break;
                case "Blue":
                    gameManager.rb--;
                    gameManager.bg--;
                    break;
                case "Red":
                    gameManager.rb--;
                    gameManager.rg--;
                    break;
            }
        }
     }
    public IEnumerator WeaveInOut(Collision collider)
    {
        weaving = true;
        weaveTimer = 0;
        Vector3 originalPos = hook.transform.position;
        Vector3 newPos;
        if (inThing)
        {
            newPos = new Vector3(originalPos.x, originalPos.y, collider.gameObject.transform.position.z + 5);
        }
        else
        {
            newPos = new Vector3(originalPos.x, originalPos.y, collider.gameObject.transform.position.z - 5);
        }

        while (weaveTimer < weaveDuration)
        {
            yield return null;
            weaveTimer += Time.deltaTime;
            hook.transform.position = Vector3.Lerp(originalPos, newPos, weaveTimer / weaveDuration);
        }
        weaving = false;
    }
    public IEnumerator SetKinematic()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < hingeList.Count - 1; i++)
        {
            hingeList[i].GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
