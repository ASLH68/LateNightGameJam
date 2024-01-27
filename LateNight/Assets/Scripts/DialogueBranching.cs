using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueBranching : MonoBehaviour
{
    [SerializeField] int vulnerableOption;
    [SerializeField] int closingDialogueThreshold;
    [SerializeField] string[] npcDialogue;
    [SerializeField] string[] playerResponses;

    PlayerControls playerInputs;

    static GameObject interactPrompt;
    static TextMeshProUGUI dialogueText;
    static Text leftButtonText;
    static Text rightButtonText;
    static GameObject leftButton;
    static GameObject rightButton;
    static GameObject dialogueBox;

    ButtonManager manager;

    // static PlayerController playerScript;

    int npcDialogueIndex = 0;

    bool canInteract = false;
    bool isInteracting = false;
    bool isDoneTalking = false;

    private void Awake()
    {
        playerInputs = new PlayerControls();
        playerInputs.Controls.Enable();
        manager = ButtonManager.staticInstance;

        if (interactPrompt == null)
        {
            interactPrompt = GameObject.Find("InteractPrompt");
            interactPrompt.SetActive(false);
        }

        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox");
            dialogueText = dialogueBox.GetComponent<TextMeshProUGUI>();
            dialogueBox.SetActive(false);
        }
        
        if (leftButton == null)
        {
            leftButton = GameObject.Find("LeftButton");
            leftButtonText = leftButton.transform.GetChild(0).gameObject.GetComponent<Text>();
            leftButton.SetActive(false);
        }

        if (rightButton == null)
        {
            rightButton = GameObject.Find("RightButton");
            rightButtonText = rightButton.transform.GetChild(0).gameObject.GetComponent<Text>();
            rightButton.SetActive(false);
        }

        // TODO check and set playerController
    }

    private void Update()
    {
        if (canInteract && !isInteracting && playerInputs.Controls.Interact.triggered == true)
        {
            // TODO disable movement

            manager.UpdateCurrentNPC(this);

            isInteracting = true;

            interactPrompt.SetActive(false);

            dialogueBox.SetActive(true);

            GetResponse(-1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDoneTalking)
        {
            interactPrompt.SetActive(true);

            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);

            canInteract = false;
        }
    }

    public void GetResponse(int buttonChoice)
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        if (isDoneTalking)
        {
            dialogueBox.SetActive(false);

            return;
        }

        if (buttonChoice != -1)
        {
            npcDialogueIndex += npcDialogueIndex + (buttonChoice + 1);

            if (npcDialogueIndex == vulnerableOption)
            {
                // TODO increment vulnerable choices
            }

            if (npcDialogueIndex >= closingDialogueThreshold)
            {
                isDoneTalking = true;
            }
        }

        dialogueText.text = npcDialogue[npcDialogueIndex];

        DisplayOptions();
    }

    private void DisplayOptions()
    {
        if (isDoneTalking)
        {
            rightButtonText.text = "Goodbye";
            rightButton.SetActive(true);

            return;
        }

        leftButtonText.text = playerResponses[npcDialogueIndex * 2];
        rightButtonText.text = playerResponses[(npcDialogueIndex * 2) + 1];

        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
