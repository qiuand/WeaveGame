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
            float spriteBottom = above.GetComponent<RectTransform>().position.y-0.750f;
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
    private void OnCollisionEnter(Collision collision)
    {
        print("yoy");
        if (!transform.parent.GetComponent<HitScript>().isStringHit && collision.gameObject.transform.parent.GetComponent<HitScript>() != null && !collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit)
        {
            if ((gameObject.tag == "Green" && collision.gameObject.tag == "Blue") || (gameObject.tag == "Blue" && collision.gameObject.tag == "Green"))
            {
                gameManager.went += 2;
                gameManager.bg += 1f;
                gameManager.rg -= 1f;
                gameManager.rb -= 1f;
                transform.parent.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.transform.parent.GetComponent<HitScript>().isStringHit = true;
                StartCoroutine(WeaveInOut());
            }
            else if ((gameObject.tag == "Green" && collision.gameObject.tag == "Red") || (gameObject.tag == "Red" && collision.gameObject.tag == "Green"))
            {
                gameManager.went += 2;
                gameManager.rg += 1f;
                gameManager.bg -= 1f;
                gameManager.rb -= 1f;
                transform.parent.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.GetComponent<ropeScript>().hit = true;
                StartCoroutine(WeaveInOut());
            }
            else if ((gameObject.tag == "Blue" && collision.gameObject.tag == "Red") || (gameObject.tag == "Red" && collision.gameObject.tag == "Blue"))
            {
                gameManager.went += 2;
                gameManager.rb += 1f;
                gameManager.bg -= 1f;
                gameManager.rg -= 1f;
                transform.parent.GetComponent<HitScript>().isStringHit = true;
                collision.gameObject.GetComponent<ropeScript>().hit = true;
                StartCoroutine(WeaveInOut());
            }
        }
        else if ((gameObject.tag == "Green" || gameObject.tag == "Blue" || gameObject.tag == "Red") && !transform.parent.GetComponent<HitScript>().isStringHit && collision.gameObject.tag == "Bounds")
        {
            gameManager.went += 1;
            switch (gameObject.tag)
            {
                case "Green":
                    gameManager.bg--;
                    gameManager.rg--;
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
            transform.parent.GetComponent<HitScript>().isStringHit = true;
        }
    }
    public IEnumerator WeaveInOut()
    {
        weaveTimer = 0;
        Vector3 originalPos = hook.transform.position;
        while (weaveTimer < weaveDuration)
        {
            yield return null;
            weaveTimer += Time.deltaTime;
            hook.transform.position = Vector3.Lerp(originalPos, -originalPos, weaveTimer / weaveDuration);
        }
    }

}
