using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueBranching : MonoBehaviour
{
    [SerializeField] int[] vulnerableOption;
    [SerializeField] int closingDialogueThreshold;
    [SerializeField] string[] npcDialogue;
    [SerializeField] string[] playerResponses;
    [SerializeField] Sprite _npcSprite;
    [SerializeField] AudioClip[] clips;
    private Image _npcPortrait;
    private AudioSource _source;


    PlayerControls playerInputs;

    public GameObject trolleyInteractPrompt;

    static GameObject interactPrompt;
    static TextMeshProUGUI dialogueText;
    static Text leftButtonText;
    static Text rightButtonText;
    static GameObject leftButton;
    static GameObject rightButton;
    static GameObject dialogueBox;

    ButtonManager manager;
    static NPCSpawner spawner;
    GameController gc;
    TrolleyScene trolleyScript;

    static PlayerBehavior playerScript;

    int npcDialogueIndex = 0;

    bool shouldShowButtons = true;
    bool canPressE = false;
    bool canInteract = false;
    bool isInteracting = false;
    bool isDoneTalking = false;
    bool wasVulnerable = false;
    [SerializeField] bool isTrolleyDialogue;

    private void Start()
    {
        playerInputs = new PlayerControls();
        playerInputs.Gameplay.Enable();
        manager = ButtonManager.staticInstance;
        playerScript = FindObjectOfType<PlayerBehavior>();
        gc = FindObjectOfType<GameController>();
        _source = GetComponent<AudioSource>();
        if (isTrolleyDialogue)
        {
            trolleyScript = FindObjectOfType<TrolleyScene>();
        }

        if (spawner == null)
        {
            spawner = FindObjectOfType<NPCSpawner>();
        }

        if (interactPrompt == null)
        {
            interactPrompt = GameObject.Find("InteractPrompt");
            interactPrompt.SetActive(false);
        }


        if (dialogueBox == null)
        {
            dialogueBox = GameObject.Find("DialogueBox");
            dialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
            GameObject.FindObjectOfType<GameController>().DialogueBox = dialogueText;
            dialogueText.font = GameObject.FindObjectOfType<GameController>().CityFont;
            interactPrompt.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameController>().CityKey;
            dialogueBox.SetActive(false);
        }

        if (_npcPortrait == null)
        {
            dialogueBox.SetActive(true);
            _npcPortrait = GameObject.Find("Portrait").GetComponent<Image>();

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

            if (isTrolleyDialogue)
            {
                //trolleyInteractPrompt.SetActive(false);
            }
            else
            {
                interactPrompt.SetActive(false);
            }

            dialogueBox.SetActive(true);
            _npcPortrait.sprite = _npcSprite;

            GetResponse(-1);
        }
        else if (shouldShowButtons && isInteracting && canPressE && playerInputs.Gameplay.Interact.triggered == true)
        {
            if (isDoneTalking)
                shouldShowButtons = false;

            canPressE = true;
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }

        if (wasVulnerable)
        {
            Debug.Log("Lo siento");
        }

    }

    public void ForceStart()
    {
        playerScript.control.Disable();

        manager = ButtonManager.staticInstance;
        manager.UpdateCurrentNPC(this);

        isInteracting = true;

        npcDialogueIndex = 0;

        interactPrompt.SetActive(false);

        dialogueBox.SetActive(true);

        _npcPortrait = GameObject.Find("Portrait").GetComponent<Image>();
        _npcPortrait.sprite = _npcSprite;

        GetResponse(-1);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDoneTalking)
        {
            if (isTrolleyDialogue)
            {
                //trolleyInteractPrompt.SetActive(true);
            }
            else
            {
                interactPrompt.SetActive(true);
                interactPrompt.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 1.6f);
            }

            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (isTrolleyDialogue)
            {
                //trolleyInteractPrompt.SetActive(false);
            }
            else
            {
                interactPrompt.SetActive(false);
            }

            canInteract = false;
        }
    }

    public void GetResponse(int buttonChoice)
    {
        leftButton.SetActive(false);
        rightButton.SetActive(false);

        _source.clip = clips[Random.Range(0, 2)];
        _source.Play();

        if (isDoneTalking)
        {
            dialogueBox.SetActive(false);

            if (isTrolleyDialogue)
            {
                trolleyScript.MoveToNextDialog();
            }
            else
            {
                playerScript.control.Enable();
            }

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
                if (npcDialogueIndex == vulner && !wasVulnerable)
                {
                    gc.personaldialogueoption();
                    spawner.SetTrolleyNPC();
                    wasVulnerable = true;
                }
            }

            if (npcDialogueIndex >= closingDialogueThreshold || npcDialogueIndex >= npcDialogue.Length)
            {
                isDoneTalking = true;
            }
        }

        if (npcDialogueIndex >= npcDialogue.Length && isTrolleyDialogue)
        {
            trolleyScript.MoveToNextDialog();
            return;
        }
 
        dialogueText.text = npcDialogue[npcDialogueIndex];
        

        DisplayOptions();
    }

    private void DisplayOptions()
    {
        /*if (isDoneTalking)
        {
            rightButtonText.text = "Goodbye";
            rightButton.SetActive(true);

            return;
        }
        */
        leftButtonText.text = playerResponses[npcDialogueIndex * 2];
        rightButtonText.text = playerResponses[(npcDialogueIndex * 2) + 1];

        canPressE = true;
        //leftButton.SetActive(true);
        //rightButton.SetActive(true);
    }

    //public void NPCResponseDialogue()
    //{
    //    _source.clip = clips[Random.Range(0, 1)];
    //    _source.Play();
    //}
}
