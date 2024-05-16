using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }

    public GameObject PauseMenu;
    public GameObject InventoryMenu;

    private bool pauseMenuToggled = false;

    private InputAction inputActions;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {

    }

    public void TogglePauseMenu()
    {
        if(InventoryMenu.gameObject.activeInHierarchy)
        {
            InventoryMenu.SetActive(false);
            Destroy(InventoryMenu.transform.GetChild(2).transform.GetChild(1).gameObject);
        }
        else
        {
            pauseMenuToggled = !pauseMenuToggled;

            if (pauseMenuToggled)
            {
                PauseMenu.SetActive(pauseMenuToggled);
                if (Cursor.lockState == CursorLockMode.Locked)
                {
                    Cursor.lockState = CursorLockMode.None;
                    FirstPersonCamera.Instance.EnableInput(false);
                }
            }
            else
            {
                PauseMenu.SetActive(pauseMenuToggled);
                if (Cursor.lockState == CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    FirstPersonCamera.Instance.EnableInput(true);
                }
            }
        }
    }
}