using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public string followType;
    bool down = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (down)
        {
            FollowTrail();
        }
    }
    public void FollowTrail()
    {
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -20f);
        GameObject.FindGameObjectWithTag(followType).transform.position=transform.position;
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
