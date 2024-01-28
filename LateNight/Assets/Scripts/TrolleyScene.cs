using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrolleyScene : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] busCharacters;
    [SerializeField] Sprite[] repeatCharSprites;
    //[SerializeField] Sprite trolleyDriver;

    [SerializeField] DialogueBranching[] characterDialogue;
    [SerializeField] DialogueBranching trolleyDriverDialogue;
    [SerializeField] DialogueBranching[] repeatCharDialogue;
    [SerializeField] DialogueBranching leavingDialogue;
    int dialogIndex = 0;
    bool hasSpokenToFinalChar = false;

    PlayerControls inputs;
    PlayerBehavior playerControls;
    FollowingCam followCamScript;
    NPCSpawner spawner;

    float timer = 0;

    private void Awake()
    {
        playerControls = FindObjectOfType<PlayerBehavior>();
        spawner = FindObjectOfType<NPCSpawner>();
        followCamScript = FindObjectOfType<FollowingCam>();
        inputs = new PlayerControls();
    }

    private void Update()
    {
        if (inputs.Gameplay.Interact.triggered == true)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator TrolleyTimer()
    {
        timer = 0;

        while (true)
        {
            yield return new WaitForSeconds(1f);

            timer += 1;

            if (timer > 60)
            {
                trolleyDriverDialogue.enabled = false;

                foreach (DialogueBranching dialogue in characterDialogue)
                {
                    dialogue.enabled = false;
                }

                foreach (DialogueBranching dialogue in repeatCharDialogue)
                {
                    dialogue.enabled = false;
                }

                hasSpokenToFinalChar = true;
                leavingDialogue.enabled = true;
                leavingDialogue.ForceStart();
            }
        }
    }

    public void StartTrolleyScene()
    {
        inputs.Gameplay.Enable();

        if (spawner.trolleyNPCToSpawn == -1)
        {
            busCharacters[busCharacters.Length - 1].sprite = null;
        }
        else
        {
            busCharacters[busCharacters.Length - 1].sprite = repeatCharSprites[spawner.trolleyNPCToSpawn];
        }

        playerControls.control.Disable();
        playerControls.gameObject.transform.position = transform.position;
        followCamScript.enabled = false;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        characterDialogue[dialogIndex].enabled = true;
        dialogIndex++;

        StartCoroutine("TrolleyTimer");
    }

    public void MoveToNextDialog()
    {
        busCharacters[dialogIndex - 1].enabled = false;

        if (dialogIndex >= characterDialogue.Length && !hasSpokenToFinalChar)
        {
            hasSpokenToFinalChar = true;

            if (spawner.trolleyNPCToSpawn == -1)
            {
                trolleyDriverDialogue.enabled = true;
            }
            else
            {
                repeatCharDialogue[spawner.trolleyNPCToSpawn].enabled = true;
            }
            //SceneManager.LoadScene(1);
        }
        else if (hasSpokenToFinalChar)
        {
            SceneManager.LoadScene("EndingVideo");
        }
        else
        {
            characterDialogue[dialogIndex].enabled = true;
            dialogIndex++;
        }

        StartCoroutine("TrolleyTimer");
    }
}
