using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    float speed=.5f;
    public string followType;
    bool down = false;
    GameObject rope;
    // Start is called before the first frame update
    void Start()
    {
        rope = GameObject.FindGameObjectWithTag(followType);
    }

    // Update is called once per frame
    void Update()
    {
        rope.GetComponent<Rigidbody>().position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, rope.transform.position.z);
        if (down)
        {
            FollowTrail();
        }
    }
    public void FollowTrail()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3((Camera.main.ScreenToWorldPoint(Input.mousePosition).x-transform.position.x)*speed, (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y)*speed, gameObject.transform.position.z);   
    }
    public void MousieDown()
    {
        down = true;
    }
    public void MousieUp()
    {
        down = false;
    }
}
