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
            if (i<10)
            {
                newSeg.gameObject.tag = gameObject.tag;
            }
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;
            prevBod = newSeg.GetComponent<Rigidbody2D>();
        }
    }
}
