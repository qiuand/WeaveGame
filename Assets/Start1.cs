using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start1 : MonoBehaviour
{
    public GameObject front;
    public GameObject prologue;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            if (index == 0)
            {
                front.SetActive(false);
                prologue.SetActive(true);
            }
            else if (index == 1)
            {
                SceneManager.LoadScene("Main");
            }
            index++;
        }
    }
}
