using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrolleyScene : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] busCharacters;
    [SerializeField] Sprite[] repeatCharSprites;
    [SerializeField] Sprite trolleyDriver;

    [SerializeField] DialogueBranching[] characterDialog;
    int dialogIndex = 0;

    PlayerBehavior playerControls;
    NPCSpawner spawner;

    private void Awake()
    {
        playerControls = FindObjectOfType<PlayerBehavior>();
        spawner = FindObjectOfType<NPCSpawner>();
    }

    public void StartTrolleyScene()
    {
        if (spawner.trolleyNPCToSpawn == -1)
        {
            busCharacters[busCharacters.Length - 1].sprite = trolleyDriver;
        }
        else
        {
            busCharacters[busCharacters.Length - 1].sprite = repeatCharSprites[spawner.trolleyNPCToSpawn];
        }

        playerControls.control.Disable();
        Camera.main.transform.position = transform.position;

        characterDialog[dialogIndex].enabled = true;
        dialogIndex++;
    }

    public void MoveToNextDialog()
    {
        busCharacters[dialogIndex - 1].enabled = false;

        if (dialogIndex >= characterDialog.Length)
        {
            SceneManager.LoadScene(1);
        }

        characterDialog[dialogIndex].enabled = true;
        dialogIndex++;
    }
}
