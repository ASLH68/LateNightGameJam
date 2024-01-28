using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyScene : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] busCharacters;
    [SerializeField] Sprite[] repeatCharSprites;
    [SerializeField] Sprite trolleyDriver;

    [SerializeField] DialogueBranching[] characterDialog;
    int dialogIndex = 0;

    PlayerBehavior playerControls;

    private void Awake()
    {
        playerControls = FindAnyObjectByType<PlayerBehavior>();
    }

    public void StartTrolleyScene()
    {
        // TODO determine sprite of alt character

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
            // TODO open video scene
        }

        characterDialog[dialogIndex].enabled = true;
        dialogIndex++;
    }
}
