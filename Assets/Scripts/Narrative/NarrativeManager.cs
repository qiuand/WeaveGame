using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NarrativeManager : MonoBehaviour
{
    private static int MAXBRANCHES = 3;

    private static int MAXOPTIONS = 3;

    private string sampleDialogue = "This is the sample dialogue";

    private string[,] inGameDialogues = new string[MAXOPTIONS, MAXBRANCHES];
    public GameObject dialogueObj;

    void Start()
    {
        dialogueObj.GetComponent<TMPro.TextMeshProUGUI>().text = sampleDialogue;
        SetUp();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetUp()
    {
        inGameDialogues[0, 0] = "This is the branch 1 in first option";
        inGameDialogues[0, 1] = "This is the branch 2 in first option";
        inGameDialogues[0, 2] = "This is the branch 3 in first option";

        inGameDialogues[0, 0] = "This is the branch 1 in second option";
        inGameDialogues[0, 1] = "This is the branch 2 in second option";
        inGameDialogues[0, 2] = "This is the branch 3 in second option";
    }

    public void ChooseBranchInChoice(int option, int branch)
    {
        dialogueObj.GetComponent<TMPro.TextMeshProUGUI>().text = inGameDialogues[option, branch];
    }
}
