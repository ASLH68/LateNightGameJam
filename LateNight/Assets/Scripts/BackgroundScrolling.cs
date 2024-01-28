using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public static BackgroundScrolling singletonInstance;

    NPCSpawner spawner;

    Queue<Background> backgroundQueue = new Queue<Background>();

    // 0 = city, 1 = park, 2 = trolley
    public int currentStage = 0;
    int[,] levelThresholds = { { 5, 2 }, { 3, 2 }, { 3, 1 } };
    int vulnerableOptions = 0;
    int peopleInteractedWith = 0;

    [SerializeField] List<Background> cityBackgrounds;
    [SerializeField] List<Background> parkBackgrounds;
    [SerializeField] Background trolleyBackground;
    [SerializeField] GameObject foregroundObject;
    [SerializeField] GameObject trolleyForegroundObject;
    [SerializeField] GameObject trolleyDoor;

    float currentXPos = 50f;
    float changeInX = 25f;

    private void Awake()
    {
        singletonInstance = this;

        spawner = FindObjectOfType<NPCSpawner>();

        foreach (Background city in cityBackgrounds)
        {
            backgroundQueue.Enqueue(city);
        }
    }

    private void Update()
    {
        if (currentStage == 2)
        {
            if (levelThresholds[currentStage, 0] <= peopleInteractedWith || levelThresholds[currentStage, 1] <= vulnerableOptions)
            {
                currentStage++;
                trolleyDoor.SetActive(true);
            }
        }
    }

    public void UpdateCounters(bool wasVulnerable)
    {
        if (wasVulnerable)
        {
            vulnerableOptions++;
            peopleInteractedWith++;
        }
        else
        {
            peopleInteractedWith++;
        }
    }

    public void ScrollBackground()
    {
        Background temp = backgroundQueue.Dequeue();

        if (levelThresholds[currentStage,0] <= peopleInteractedWith || levelThresholds[currentStage,1] <= vulnerableOptions)
        {
            peopleInteractedWith = vulnerableOptions = 0;

            currentStage++;

            backgroundQueue.Dequeue();
            backgroundQueue.Dequeue();

            if (currentStage == 1)
            {
                foreach (Background park in parkBackgrounds)
                {
                    backgroundQueue.Enqueue(park);
                }
            }

            if (currentStage == 2)
            {
                backgroundQueue.Enqueue(trolleyBackground);

                trolleyForegroundObject.transform.position = new Vector2(currentXPos - (changeInX / 2), 3);
                //foregroundObject.GetComponent<BoxCollider2D>().enabled = true;
                //spawner.SpawnTrolleyNPC(currentXPos);
            }

            temp = backgroundQueue.Dequeue();

            foregroundObject.transform.position = new Vector2(currentXPos - (changeInX / 2), 3);

            if (currentStage == 2)
            {
                currentXPos += changeInX / 2;
            }
        }
            
        backgroundQueue.Enqueue(temp);

        spawner.SpawnNewNPC(currentXPos);
        //GameObject gcObject = GameObject.Find("GameController");
        //GameController gc = gcObject.GetComponent<GameController>();
        //gc.spawnanother();

        peopleInteractedWith++;

        temp.ChangeLocation(new Vector2(currentXPos, 3));
        currentXPos += changeInX;
    }
}
