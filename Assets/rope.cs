using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    float ropeTimer;
    float ropeDuration = 1.0f;
    int inactiveChainLength = 10;

    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegments;
    public int linkNum=5;

    public HingeJoint2D top;
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
        Rigidbody2D prevBod = hook;
        for (int i=0; i< linkNum; i++)
        {
            GameObject newSeg = Instantiate(prefabRopeSegments[0]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;
            if (i == 0)
            {
                top = hj;
                newSeg.gameObject.tag = gameObject.tag;
            }
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
    public void addSeg()
    {
        GameObject newSeg = Instantiate(prefabRopeSegments[0]);
        newSeg.transform.parent = transform;
        newSeg.transform.position = transform.position;
        HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;
        newSeg.GetComponent<ropeScript>().below = top.gameObject;
        top.connectedBody = newSeg.GetComponent<Rigidbody2D>();
        top.GetComponent<ropeScript>().ResetAnchor();
        top = hj;

    }
}
