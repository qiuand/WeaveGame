using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript : MonoBehaviour
{
    public GameObject above, below;
    // Start is called before the first frame update
    void Start()
    {
        above = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        ropeScript aboveSegment = above.GetComponent<ropeScript>();
        if (aboveSegment != null)
        {
            aboveSegment.below = gameObject;
            float spriteBottom = above.GetComponent<SpriteRenderer>().bounds.size.y;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(1);
    }
}
