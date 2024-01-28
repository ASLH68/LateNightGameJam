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
    FollowingCam followCamScript;
    NPCSpawner spawner;

    private void Awake()
    {
        playerControls = FindObjectOfType<PlayerBehavior>();
        spawner = FindObjectOfType<NPCSpawner>();
        followCamScript = FindObjectOfType<FollowingCam>();
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
        playerControls.gameObject.transform.position = transform.position;
        followCamScript.enabled = false;
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

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
