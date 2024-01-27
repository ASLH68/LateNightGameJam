using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] vulnerableNPCs;
    [SerializeField] GameObject[] genericNPCs;
    [SerializeField] Transform spawnPoint;

    GameController gc;
    int nextPointThreshold = 1;
    int genericNPCIndex = 0;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
    }

    public void SpawnNewNPC(float spawnLocation)
    {
        spawnPoint.position = new Vector2(spawnLocation, spawnPoint.position.y);

        if (gc.points >= nextPointThreshold)
        {
            Instantiate(vulnerableNPCs[nextPointThreshold - 1], spawnPoint.position, spawnPoint.rotation);

            nextPointThreshold++;
        }
        else
        {
            Instantiate(genericNPCs[genericNPCIndex], spawnPoint.position, spawnPoint.rotation);

            genericNPCIndex++;
        }
    }
}
