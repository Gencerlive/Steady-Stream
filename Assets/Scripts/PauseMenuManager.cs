using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components")]
    private PlayerMovement playerMovement;

    [Header("Input")]
    [SerializeField] private InputActionAsset inputManager;

    private InputAction menuKey;

    [Header("GameObject")]
    [SerializeField] private GameObject pauseMenu;

    private bool menuOn;

    private void Start()
    {
        pauseMenu.SetActive(false);

        menuKey = inputManager.FindAction("Menu");

        //Get player components
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (menuKey.WasPressedThisFrame())
        {
            if (!menuOn)
            {
                PauseGame();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        menuOn = false;

        pauseMenu.SetActive(false);

        playerMovement.PreventMovement(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        menuOn = true;

        pauseMenu.SetActive(true);

        pauseMenu.SetActive(menuOn);
        playerMovement.PreventMovement(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
