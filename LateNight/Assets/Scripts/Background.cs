using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public enum whatZone { city, park, trolley };
    public whatZone backgroundType;

    BackgroundScrolling backgroundManager;
    [SerializeField] GameObject invisibleWall;

    [SerializeField] bool hasTriggered = false;

    private void Start()
    {
        backgroundManager = BackgroundScrolling.singletonInstance;
    }

    public void ChangeLocation(Vector2 position)
    {
        hasTriggered = false;
        invisibleWall.SetActive(false);
        gameObject.SetActive(false);
        transform.position = position;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            invisibleWall.SetActive(true);

            if (backgroundType != whatZone.trolley)
            {
                backgroundManager.ScrollBackground();
            }

            if(backgroundType == whatZone.city)
            {
                GameObject.FindObjectOfType<GameController>().DialogueBox.font = GameObject.FindObjectOfType<GameController>().CityFont;
                GameObject.FindObjectOfType<GameController>().InteractImg.sprite = GameObject.FindObjectOfType<GameController>().CityKey;
            }
            else if (backgroundType == whatZone.park) 
            {
                GameObject.FindObjectOfType<GameController>().DialogueBox.font = GameObject.FindObjectOfType<GameController>().ParkFont;
                GameObject.FindObjectOfType<GameController>().InteractImg.sprite = GameObject.FindObjectOfType<GameController>().ParkKey;
                GameObject playerob = GameObject.FindWithTag("Player");
                PlayerBehavior pb = playerob.GetComponent<PlayerBehavior>();
                pb.switchSprite();
            }
            else if (backgroundType == whatZone.trolley)
            {
                GameObject.FindObjectOfType<GameController>().DialogueBox.font = GameObject.FindObjectOfType<GameController>().TrolleyFont;
                GameObject.FindObjectOfType<GameController>().InteractImg.sprite = GameObject.FindObjectOfType<GameController>().TrolleyKey;
            }
        }
    }
}
