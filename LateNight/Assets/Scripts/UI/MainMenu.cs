using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerInput _controls;

    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _quitPromptDisplay;

    public enum Panel
    {
        CONTROLS,
        CREDITS
    }

    private void Awake()
    {
        _controls = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _controls.currentActionMap.Enable();
    }

    private void Start()
    {
        _controls.currentActionMap.FindAction("Back").started += DisplayPrompt;
        _settings.SetActive(false);
        _quitPromptDisplay.SetActive(false);
    }

    /// <summary>
    /// Loads the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Opens the settings
    /// </summary>
    /// <param name="val"></param>
    public void OpenSettings()
    {
        var val = _settings.activeInHierarchy;
        _settings.SetActive(!val);
        _title.SetActive(val);
    }

    #region Quit Game
    /// <summary>
    /// Displays the quit prompt from the input action
    /// </summary>
    /// <param name="obj"></param>
    private void DisplayPrompt(InputAction.CallbackContext obj)
    {
        _settings.SetActive(false);
        var val = _quitPromptDisplay.activeInHierarchy;
        _quitPromptDisplay?.SetActive(!val);
        _title.SetActive(val);
    }

    /// <summary>
    /// Displays the quit prompt 
    /// </summary>
    public void DisplayPrompt()
    {
        _settings.SetActive(false);
        var val = _quitPromptDisplay.activeInHierarchy;
        _quitPromptDisplay?.SetActive(!val);
        _title.SetActive(val);
    }

    // Quits the application
    public void ConfirmQuit()
    {
        Application.Quit();
    }
    #endregion

    private void OnDisable()
    {
        _controls.currentActionMap.Disable();
    }
}
