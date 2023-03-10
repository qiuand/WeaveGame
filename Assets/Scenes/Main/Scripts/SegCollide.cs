using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegCollide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (gameObject.transform.parent.GetComponent<rope3D>()!=null)
        {
            gameObject.transform.parent.GetComponent<rope3D>().ProcessCollision(collision, gameObject);
        }
    }
}
