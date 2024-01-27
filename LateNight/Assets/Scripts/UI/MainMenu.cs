using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private PlayerInput _controls;

    private GameObject _currentPanel;
    [SerializeField] private GameObject _controlPanel;
    [SerializeField] private GameObject _creditPanel;

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
        _controls.currentActionMap.FindAction("Back").started += DisablePanels;
        DisablePanels();
    }

    /// <summary>
    /// Loads the game
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Switches panel to the one pressed
    /// </summary>
    /// <param name="panel"></param>
    public void SwitchPanel(GameObject panel)
    {
        _currentPanel?.SetActive(false);
        panel.SetActive(true);
        _currentPanel = panel;
    }

    /// <summary>
    /// Closes the game
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Returns to the main page
    /// </summary>
    public void Back()
    {
        _currentPanel?.SetActive(false);
    }

    /// <summary>
    /// Disables panels
    /// </summary>
    public void DisablePanels(InputAction.CallbackContext obj)
    {
        DisablePanels();
    }

    private void DisablePanels()
    {
        _controlPanel?.SetActive(false);
        _creditPanel?.SetActive(false);
    }

    private void OnDisable()
    {
        _controls.currentActionMap.Disable();
    }
}
