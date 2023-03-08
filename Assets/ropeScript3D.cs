using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript3D : MonoBehaviour
{
    float weaveDuration=1.0f;
    float weaveTimer;
    public GameObject hook;
    public bool hit = false;
    public GameObject above, below;
    // Start is called before the first frame update
    void Start()
    {
        above = GetComponent<HingeJoint>().connectedBody.gameObject;
        ropeScript aboveSegment = above.GetComponent<ropeScript>();
        if (aboveSegment != null)
        {
            aboveSegment.below = gameObject;
            float spriteBottom = above.GetComponent<RectTransform>().position.y+331.50f;
            GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, spriteBottom, 0);
        }
        else
        {
            GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, 0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ResetAnchor()
    {
        above = GetComponent<HingeJoint>().connectedBody.gameObject;
        ropeScript aboveSeg = above.GetComponent<ropeScript>();
        if (aboveSeg != null)
        {
            aboveSeg.below = gameObject;
            float spriteBottom = above.GetComponent<RectTransform>().rect.yMax;
            GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, spriteBottom, 0);
        }
        else
        {
            GetComponent<HingeJoint>().connectedAnchor = new Vector3(0, 0, 0);
        }
    }

}
