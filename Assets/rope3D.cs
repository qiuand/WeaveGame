using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope3D : MonoBehaviour
{
    float mass = 1;
    float ropeTimer;
    float ropeDuration = 1.0f;
    int inactiveChainLength = 10;

    public Rigidbody hook;
    public GameObject[] prefabRopeSegments;
    public int linkNum = 5;

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
            mass -= 0.005f;
        }
    }
    public void addSeg()
    {
        GameObject newSeg = Instantiate(prefabRopeSegments[0]);
        newSeg.transform.parent = transform;
        newSeg.transform.position = transform.position;
        HingeJoint hj = newSeg.GetComponent<HingeJoint>();
        hj.connectedBody = hook;
        newSeg.GetComponent<ropeScript>().below = top.gameObject;
        top.connectedBody = newSeg.GetComponent<Rigidbody>();
        top.GetComponent<ropeScript>().ResetAnchor();
        top = hj;
    }
    IEnumerator RedoBody(GameObject seg)
    {
        yield return new WaitForSeconds(5.0f);
        seg.GetComponent<BoxCollider>().isTrigger = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("jj");
    }
}
