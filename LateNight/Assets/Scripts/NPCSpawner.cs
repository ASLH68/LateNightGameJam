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

    // X value represents list (0 for vulnerable, 1 for generic), y represents index
    Vector2 mostRecentlySpawned;
    Vector2 secondMostRecentlySpawned;
    Vector2 trolleyNPCToSpawn = new Vector2(2, 0);
    [SerializeField] GameObject busDriver;

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

    public void SpawnTrolleyNPC(float position)
    {
        if (trolleyNPCToSpawn.x == 0)
        {
            Instantiate(vulnerableNPCs[(int)trolleyNPCToSpawn.y], new Vector2(position, 0), Quaternion.identity);
        }
        else if (trolleyNPCToSpawn.y == 1)
        {
            Instantiate(genericNPCs[(int)trolleyNPCToSpawn.y], new Vector2(position, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(busDriver, new Vector2(position, -1.4f), Quaternion.identity);
        }
    }

    public void SetTrolleyNPC()
    {
        if (trolleyNPCToSpawn.x == 2)
        {
            trolleyNPCToSpawn = secondMostRecentlySpawned;
            Debug.Log("New trolley npc: " + trolleyNPCToSpawn);
        }
    }
}
