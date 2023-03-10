using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class dialogue : ScriptableObject
{
    public box[] diagArray=new box[10];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
public struct box{
    public string dialogue;
    public box[] leadsTo;

    public box(string nDialogue, box[] nLeadsTo)
    {
        dialogue = nDialogue;
        leadsTo = nLeadsTo;
    }
}
