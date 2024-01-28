using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnim : MonoBehaviour
{
    [SerializeField] private GameObject _clockTime;
    [SerializeField] private float _onTime;
    [SerializeField] private float _offTime;

    private bool _isOn = true;

    private void OnEnable()
    {
        StartCoroutine(ClockDisplay());
    }

    private IEnumerator ClockDisplay()
    {
        while (true)
        {
            _clockTime?.SetActive(!_clockTime.activeInHierarchy);
            
            _isOn = !_isOn;

            if (_isOn)
            {
                yield return new WaitForSeconds(_onTime);
            }
            else
            {
                yield return new WaitForSeconds(_offTime);
            }
        }
    }
}
