using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueBranching : MonoBehaviour
{
    [SerializeField] int[] vulnerableOption;
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
    GameController gc;

    static PlayerBehavior playerScript;

    int npcDialogueIndex = 0;

    bool canInteract = false;
    bool isInteracting = false;
    bool isDoneTalking = false;
    bool wasVulnerable = false;

    private void Start()
    {
        playerInputs = new PlayerControls();
        playerInputs.Gameplay.Enable();
        manager = ButtonManager.staticInstance;
        playerScript = FindObjectOfType<PlayerBehavior>();
        gc = FindObjectOfType<GameController>();

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
    }

    private void Update()
    {
        if (canInteract && !isInteracting && playerInputs.Gameplay.Interact.triggered == true)
        {
            playerScript.control.Disable();

            manager.UpdateCurrentNPC(this);

            isInteracting = true;

            interactPrompt.SetActive(false);

            dialogueBox.SetActive(true);

            GetResponse(-1);
        }

        if(wasVulnerable)
        {
            Debug.Log("Lo siento");
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

            playerScript.control.Enable();

            if (!wasVulnerable)
            {
                gc.passivedialogueoption();
            }

            return;
        }

        if (buttonChoice != -1)
        {
            npcDialogueIndex += npcDialogueIndex + (buttonChoice + 1);

            foreach (int vulner in vulnerableOption)
            {
                if (npcDialogueIndex == vulner)
                {
                    gc.personaldialogueoption();
                    wasVulnerable = true;
                }
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
        //if (isDoneTalking)
        //{
        //    rightButtonText.text = "Goodbye";
        //    rightButton.SetActive(true);

        //    return;
        //}

        leftButtonText.text = playerResponses[npcDialogueIndex * 2];
        rightButtonText.text = playerResponses[(npcDialogueIndex * 2) + 1];

        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }
}
