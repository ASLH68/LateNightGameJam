using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    //[SerializeField] GameObject[] vulnerableNPCs;
    [SerializeField] GameObject[] genericNPCs;
    [SerializeField] Transform spawnPoint;

    GameController gc;
    BackgroundScrolling backgroundScript;
    //int nextPointThreshold = 1;
    int genericNPCIndex = 0;

    // X value represents list (0 for vulnerable, 1 for generic), y represents index
    int mostRecentlySpawned;
    int secondMostRecentlySpawned;
    public int trolleyNPCToSpawn = -1;
    [SerializeField] GameObject busDriver;

    private void Start()
    {
        gc = FindObjectOfType<GameController>();
        backgroundScript = BackgroundScrolling.singletonInstance;
    }

    public void SpawnNewNPC(float spawnLocation)
    {
        spawnPoint.position = new Vector2(spawnLocation, spawnPoint.position.y);

        secondMostRecentlySpawned = mostRecentlySpawned;
        mostRecentlySpawned = genericNPCIndex;

        //if (gc.points >= nextPointThreshold)
        //{
        //    Instantiate(vulnerableNPCs[nextPointThreshold - 1], spawnPoint.position, spawnPoint.rotation);

        //    nextPointThreshold++;
        //}
        //else
        //{
        if (genericNPCIndex < genericNPCs.Length)
        {
            if (backgroundScript.currentStage == 1 && genericNPCIndex < 4)
            {
                genericNPCIndex = 4;
            }

            Instantiate(genericNPCs[genericNPCIndex], spawnPoint.position, spawnPoint.rotation);

            genericNPCIndex++;
        }
        //}
    }

    public void SpawnTrolleyNPC()
    {
        //if (trolleyNPCToSpawn.x == 0)
        //{
        //    Instantiate(vulnerableNPCs[(int)trolleyNPCToSpawn.y], new Vector2(position, 0), Quaternion.identity);
        //}
        //if (trolleyNPCToSpawn.y == 1)
        //{
        //    Instantiate(genericNPCs[(int)trolleyNPCToSpawn.y], new Vector2(position, 0), Quaternion.identity);
        //}
        //else
        //{
        //    Instantiate(busDriver, new Vector2(position, -1.4f), Quaternion.identity);
        //}
    }

    public void SetTrolleyNPC()
    {
        if (trolleyNPCToSpawn == -1)
        {
            trolleyNPCToSpawn = secondMostRecentlySpawned;
            Debug.Log("New trolley npc: " + trolleyNPCToSpawn);
        }
    }
}
