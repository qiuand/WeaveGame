using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using TMPro;
public class DialogueViewer : MonoBehaviour
{
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    //[SerializeField] SlowTyper txtMessage;
    [SerializeField] TextMeshProUGUI txtMessage;
    //[SerializeField] UnityEngine.UI.Text txtMessage;

    //[SerializeField] SlowTyper txtTitle;
    //[SerializeField] Image imgMemory;
    //[SerializeField] Button btnSpeedyProgress;
    [SerializeField] DialogueController dialogueController;
    DialogueController controller;
    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller = dialogueController;
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();

        // Start the dialogue
        
        var curNode = controller.GetCurrentNode();
                    Debug.Log(curNode.text);

    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(Node newNode)
    {
        Debug.Log("ON");

        //txtMessage.text = "aaaaa";
        KillAllChildren(parentOfResponses);

        for (int i = newNode.responses.Count - 1; i >= 0; i--)
        {
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
            responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;

            txtMessage.text = response.displayText;
            Debug.Log(newNode.text);
             Debug.Log(newNode.responses[i].displayText);
            responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
        }
        
        Debug.Log("End");

        //txtMessage.text = "aaaaa";

    }
}