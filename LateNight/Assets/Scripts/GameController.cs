using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] public float points;
    [SerializeField] public GameObject Npc0;
    [SerializeField] public GameObject Npc1;
    [SerializeField] public GameObject Npc2;

    BackgroundScrolling backgroundManager;

    // Start is called before the first frame update
    void Start()
    {
        backgroundManager = BackgroundScrolling.singletonInstance;   
    }

    // Update is called once per frame
    void Update()
    {
        if(points == 0)
        {
            Npc0.SetActive(true);
        }
        else
        {
            Npc0.SetActive(false);
        }
        if(points == 1)
        {
            Npc1.SetActive(true);
        }
        if(points == 2)
        {
            Npc2.SetActive(true);
        }
    }

    public void passivedialogueoption()
    {
        points += 1;
        backgroundManager.UpdateCounters(false);
    }
    public void personaldialogueoption()
    {
        points += 2;
        backgroundManager.UpdateCounters(true);
    }
}
