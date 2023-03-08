using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeCutter : MonoBehaviour
{
    Rigidbody2D rb;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
        {
            //Debug.Log("Getmouse");
            rb.position = cam.ScreenToWorldPoint(Input.mousePosition);
            Invoke("VisibleBlade", 0.05f);

            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Green" || hit.collider.tag == "Red" || hit.collider.tag == "Blue")
                {
                    Destroy(hit.collider.gameObject);

                    int countChild = hit.transform.parent.gameObject.transform.childCount;
                    for (int i = 0; i < countChild - 1; i++)
                    {
                        Destroy(hit.transform.parent.gameObject.transform.GetChild(i).gameObject, 2f);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(2))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void VisibleBlade()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }
}
