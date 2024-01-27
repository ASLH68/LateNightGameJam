using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public static BackgroundScrolling singletonInstance;

    Queue<Background> backgroundQueue = new Queue<Background>();

    // 0 = city, 1 = park, 2 = trolley
    int currentStage = 0;
    int[,] levelThresholds = { { 6, 2 }, { 3, 1 }, { 3, 1 } };
    int vulnerableOptions = 0;
    int peopleInteractedWith = 0;

    [SerializeField] List<Background> cityBackgrounds;
    [SerializeField] List<Background> parkBackgrounds;
    [SerializeField] Background trolleyBackground;
    [SerializeField] GameObject foregroundObject;

    float currentXPos = 50f;
    float changeInX = 25f;

    private void Awake()
    {
        singletonInstance = this;

        foreach (Background city in cityBackgrounds)
        {
            backgroundQueue.Enqueue(city);
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
            }

            temp = backgroundQueue.Dequeue();

            foregroundObject.transform.position = new Vector2(currentXPos - (changeInX / 2), 3);

            if (currentStage == 2)
            {
                currentXPos += changeInX / 2;
            }
        }
            
        backgroundQueue.Enqueue(temp);
        
        temp.ChangeLocation(new Vector2(currentXPos, 3));
        currentXPos += changeInX;
    }
}