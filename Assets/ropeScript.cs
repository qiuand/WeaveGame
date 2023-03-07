using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeScript : MonoBehaviour
{
    public bool hit = false;
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
        if (!hit && collision.gameObject.GetComponent<ropeScript>() != null && !collision.gameObject.GetComponent<ropeScript>().hit)
        {
            if ((gameObject.tag == "Green" && collision.gameObject.tag == "Blue") || (gameObject.tag == "Blue" && collision.gameObject.tag == "Green"))
            {
                print(1);
                gameManager.went += 2;
                gameManager.bg += 1f;
                gameManager.rg -= 1f;
                gameManager.rb -= 1f;
                hit = true;
                collision.gameObject.GetComponent<ropeScript>().hit = true;

            }
            else if ((gameObject.tag == "Green" && collision.gameObject.tag == "Red") || (gameObject.tag == "Red" && collision.gameObject.tag == "Green"))
            {
                gameManager.went += 2;
                gameManager.rg += 1f;
                gameManager.bg -= 1f;
                gameManager.rb -= 1f;
                hit = true;
                collision.gameObject.GetComponent<ropeScript>().hit = true;

            }
            else if ((gameObject.tag == "Blue" && collision.gameObject.tag == "Red") || (gameObject.tag == "Red" && collision.gameObject.tag == "Blue"))
            {
                gameManager.went += 2;
                gameManager.rb += 1f;
                gameManager.bg -= 1f;
                gameManager.rg -= 1f;
                hit = true;
                collision.gameObject.GetComponent<ropeScript>().hit = true;
            }
        }
        else if ((gameObject.tag == "Green" || gameObject.tag == "Blue" || gameObject.tag == "Red") && collision.gameObject.tag == "Bounds" && !hit)
        {
            gameManager.went += 1;
            switch (gameObject.tag)
            {
                case "Green":
                    gameManager.bg--;
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
            hit = true;
        }
    }
}
