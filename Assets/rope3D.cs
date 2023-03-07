using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope3D : MonoBehaviour
{
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
        Rigidbody prevBod = hook;
        for (int i = 0; i < linkNum; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSegments[0]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint hj = newSeg.GetComponent<HingeJoint>();
            hj.connectedBody = prevBod;
            if (i == 0)
            {
                top = hj;
                newSeg.gameObject.tag = gameObject.tag;
            }
            prevBod = newSeg.GetComponent<Rigidbody>();
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
}
