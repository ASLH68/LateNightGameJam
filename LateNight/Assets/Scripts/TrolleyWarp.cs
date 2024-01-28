using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyWarp : MonoBehaviour
{
    TrolleyScene trolleyScript;

    private void Awake()
    {
        trolleyScript = FindObjectOfType<TrolleyScene>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            trolleyScript.StartTrolleyScene();
        }
    }
}
