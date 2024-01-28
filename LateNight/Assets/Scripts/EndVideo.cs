using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndVideo : MonoBehaviour
{
    int timer = 0;

    private void Awake()
    {
        StartCoroutine(VideoWait());
    }

    IEnumerator VideoWait()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);

            timer++;

            if (timer >= 10f)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
