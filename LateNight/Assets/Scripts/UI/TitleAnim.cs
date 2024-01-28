using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    [SerializeField] private GameObject _clockTime;
    [SerializeField] private float _flickerTime;

    private void OnEnable()
    {
        StartCoroutine(ClockDisplay());
    }

    private IEnumerator ClockDisplay()
    {
        while (true)
        {
            _clockTime?.SetActive(!_clockTime.activeInHierarchy);
            yield return new WaitForSeconds(_flickerTime);
        }
    }
}
