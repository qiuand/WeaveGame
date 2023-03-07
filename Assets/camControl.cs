using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camControl : MonoBehaviour
{
    float inDistance = 5f;
    float camTimer;
    float camDuration = 1f;
    Camera cam;
    Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        originalPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator CamLerp()
    {
        while (camTimer < camDuration)
        {
            yield return null;
            camTimer += Time.deltaTime;
            cam.transform.position = Vector3.Lerp(originalPos, new Vector3(originalPos.x, originalPos.y + inDistance, originalPos.z), camTimer/camDuration);
        }
        originalPos = cam.transform.position;
        camTimer = 0;
    }
}
