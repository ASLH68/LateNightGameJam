using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public float points;
    [SerializeField] public GameObject[] Npc;
    [SerializeField] Transform spawnPoint;
    [SerializeField] bool spawnonce;
    BackgroundScrolling backgroundManager;
    NPCSpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        backgroundManager = BackgroundScrolling.singletonInstance;
        spawner = FindObjectOfType<NPCSpawner>();
        spawnonce = true;
    }

    // Update is called once per frame
    void Update()
    {
        //switch(points)
        //{
        //    case 0:
        //        if(spawnonce)
        //        {
        //            Instantiate(Npc[0], spawnPoint);
        //            spawnonce = false;
        //        }
        //        break;
        //    case 1:
        //        if (spawnonce)
        //        {
        //            Instantiate(Npc[1], spawnPoint);
        //            spawnonce = false;
        //        }
        //        break;
        //    case 2:
        //        if (spawnonce)
        //        {
        //            Instantiate(Npc[2], spawnPoint);
        //            spawnonce = false;
        //        }
        //        break;
        //    default:
        //        if (spawnonce)
        //        {
        //            Instantiate(Npc[0], spawnPoint);
        //            spawnonce = false;
        //        }
        //        break;
        //}
    }

    //public void spawnanother()
    //{
    //    spawnonce = true;
    //}

    public void passivedialogueoption()
    {
        //points += 1;
        backgroundManager.UpdateCounters(false);
    }
    public void personaldialogueoption()
    {
        points += 1;
        backgroundManager.UpdateCounters(true);

        if (points == 1)
        {

        }
    }
}
