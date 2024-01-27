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
    [SerializeField] int vulnerableOptions = 0;
    [SerializeField] int peopleInteractedWith = 0;

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













    //Vector2 cityThresholds = new Vector2(6, 2);
    //Vector2 parkThresholds = new Vector2(4, 1);
    //Vector2 trolleyThresholds = new Vector2(3, 1);

    //float backgroundSpawnX;

    //int currentLocation = 0;
    //int peopleInteractedWith = 0;
    //int timesBeingVulnerable = 0;

    //bool transitionQueued = false;

    //[SerializeField] Sprite cityBackground;
    //[SerializeField] Sprite parkBackground;
    //[SerializeField] Sprite trolleyBackground;

    //[SerializeField] Sprite foregroundTransition;

    //private void Update()
    //{
    //    switch (currentLocation)
    //    {
    //        case 0:
    //            if (peopleInteractedWith >= cityThresholds.x || timesBeingVulnerable >= cityThresholds.y)
    //            {
    //                peopleInteractedWith = timesBeingVulnerable = 0;
    //                transitionQueued = true;
    //                currentLocation++;
    //            }
    //            break;

    //        case 1:
    //            if (peopleInteractedWith >= parkThresholds.x || timesBeingVulnerable >= parkThresholds.y)
    //            {
    //                peopleInteractedWith = timesBeingVulnerable = 0;
    //                transitionQueued = true;
    //                currentLocation++;
    //            }
    //            break;

    //        case 2:
    //            if (peopleInteractedWith >= trolleyThresholds.x || timesBeingVulnerable >= trolleyThresholds.y)
    //            {
    //                peopleInteractedWith = timesBeingVulnerable = 0;
    //                transitionQueued = true;
    //                currentLocation++;
    //            }
    //            break;
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Background"))
    //    {
    //        gameObject.SetActive(false);

    //        if (transitionQueued)
    //        {
    //            // TODO swap to new background
    //        }
    //        else
    //        {
    //            gameObject.transform.position = new Vector2(backgroundSpawnX, gameObject.transform.position.y);
    //        }
    //    }
    //}
}
