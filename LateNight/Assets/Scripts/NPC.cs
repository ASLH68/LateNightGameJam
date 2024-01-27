using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool canpressE;
    [SerializeField] public GameObject dialogueoptions;

    // Start is called before the first frame update
    void Start()
    {
        canpressE = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canpressE)
        {
            dialogueoptions.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            canpressE = true;

        }
    }

}
