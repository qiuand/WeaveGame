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
    public GameObject ending1;
    public GameObject ending2;
    public GameObject imgThing;
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    //[SerializeField] SlowTyper txtMessage;
    [SerializeField] TextMeshProUGUI txtMessage;

    //[SerializeField] SlowTyper txtTitle;
    [SerializeField] Image imgMemory;
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
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    public void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    public void OnNodeEntered(Node newNode)
    {
        if(newNode.title=="bg orc route b")
        {
            gameManager.gameLock = true;
        }
        else if (newNode.title=="gr elf route")
        {
            ending2.SetActive(true);
        }
        else if (newNode.title=="br act 1 end")
        {
            ending1.SetActive(true);
        }
        Debug.Log("Entering Node: " + newNode.title);

        KillAllChildren(parentOfResponses);

        for (int i = newNode.responses.Count - 1; i >= 0; i--)
        {
            Debug.Log("i[" + i.ToString() + "], newNode.responses.Count[" + newNode.responses.Count.ToString() + "]");
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
            responceButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;

            txtMessage.text = response.messageText;
            Debug.Log("DisplayText[" + response.displayText + "]");
            responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });

            Debug.Log("Showing: " + newNode.title + ".png");
            Sprite memoryImage = Resources.Load<Sprite>(newNode.title);
            /*            Sprite memoryImage = Texture2DToSprite(memoryTexture);*/
            //imgMemory.GetComponent<Oscillate>().Begin();
            //ShowContinueButton(typeMessageAfterTitle);
            //txtMessage.Clear();
            imgThing.GetComponent<Image>().sprite = memoryImage;
        }
        Debug.Log("Showmemory");

        UnityAction showMemoryAfterTitle = delegate
        {

        };
        if (newNode.tags.Contains("END"))
        {
            gameManager.gameEnd = true;

        }
    }

    public static Sprite Texture2DToSprite(Texture2D t)
    {
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }


}