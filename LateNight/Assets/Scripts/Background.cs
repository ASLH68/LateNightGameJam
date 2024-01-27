using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
