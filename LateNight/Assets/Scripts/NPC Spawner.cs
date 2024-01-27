using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] vulnerableNPCs;
    [SerializeField] GameObject[] genericNPCs;

    GameController gc;
    int nextPointThreshold = 1;
    int genericNPCIndex = 0;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void SpawnNewNPC(float spawnLocation)
    {
        GameObject temp;

        if (gc.points >= nextPointThreshold)
        {
            temp = vulnerableNPCs[nextPointThreshold - 1];

            nextPointThreshold++;
        }
        else
        {
            temp = genericNPCs[genericNPCIndex];

            genericNPCIndex++;
        }

        temp.transform.position = new Vector2(spawnLocation, 3);
        temp.SetActive(true);
    }
}
