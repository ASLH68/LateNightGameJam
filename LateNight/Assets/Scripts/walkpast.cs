using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkpast : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            GameObject gcObject = GameObject.Find("GameController");
            GameController gc = gcObject.GetComponent<GameController>();
            gc.passivedialogueoption();


        }
    }
}
