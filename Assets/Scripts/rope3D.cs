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
/*                newSeg.gameObject.tag = gameObject.tag;*/
            }
            StartCoroutine(RedoBody(newSeg));
            prevBod = newSeg.GetComponent<Rigidbody>();
            if (i <= 2)
            {
                newSeg.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            if(i>=linkNum/2)
            {
                print("yeeee");
                newSeg.gameObject.tag = "Fillie";
            }
            if (i == linkNum-1)
            {
/*                newSeg.GetComponent<Rigidbody>().transform.localPosition = new Vector3(newSeg.GetComponent<Rigidbody>().transform.position.x, newSeg.GetComponent<Rigidbody>().transform.position.y - 20, newSeg.GetComponent<Rigidbody>().transform.position.z);
*/                newSeg.GetComponent<Rigidbody>().isKinematic = true;
            }
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
/*        seg.GetComponent<BoxCollider>().isTrigger = false;*/
    }
    public void ProcessCollision(Collider collision, GameObject collidee)
    {
/*        if (!weaving)
        {
            StartCoroutine(WeaveInOut(collision));
        }*/

        if (!transform.GetComponent<HitScript>().isStringHit && collision.gameObject.transform.parent.GetComponent<HitScript>() != null && !collidee.gameObject.transform.parent.GetComponent<HitScript>().isStringHit && collidee.gameObject.tag != "Fillie" && (collidee.gameObject.tag=="Green"|| collidee.gameObject.tag == "Blue"|| collidee.gameObject.tag == "Red"))
        {
            StartCoroutine( collision.gameObject.transform.parent.GetComponent<rope3D>().SetKinematic());
            if ((collidee.tag == "Green" && collision.gameObject.tag == "Blue") || (collidee.tag == "Blue" && collision.gameObject.tag == "Green"))
            {
                HandleButton("bg");
                gameManager.went += 2;
                gameManager.bg += 1f;
                gameManager.rg -= 1f;
                gameManager.rb -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
                StartCoroutine(SetKinematic());
            }
            else if ((collidee.tag == "Green" && collision.gameObject.tag == "Red") || (collidee.tag == "Red" && collision.gameObject.tag == "Green"))
            {
                HandleButton("gr");
                gameManager.went += 2;
                gameManager.rg += 1f;
                gameManager.bg -= 1f;
                gameManager.rb -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
                StartCoroutine(SetKinematic());

            }
            else if ((collidee.tag == "Blue" && collision.gameObject.tag == "Red") || (collidee.tag == "Red" && collision.gameObject.tag == "Blue"))
            {
                HandleButton("br");
                gameManager.went += 2;
                gameManager.rb += 1f;
                gameManager.bg -= 1f;
                gameManager.rg -= 1f;
                transform.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
                StartCoroutine(SetKinematic());

            }
        }
        if (!transform.GetComponent<HitScript>().isStringHit && collision.gameObject.tag == "Bounds" && gameManager.went==2)
        {
            StartCoroutine(SetKinematic());
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
    public void HandleButton(string condition)
    {
        gameManager.triggerType = condition;
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
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < hingeList.Count - 1; i++)
        {
            Destroy(hingeList[i].GetComponent<Collider>());
            /*            hingeList[i].GetComponent<Rigidbody>().isKinematic = true;*/
        }
    }
}
