using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    CanonControls canonControls;
    private InputAction pausar;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public PauseManager pM;

    private void Awake()
    {
        pauseMenu = GameObject.Find("Pause");
        pauseMenu.SetActive(false);
        canonControls = new CanonControls();
        Time.timeScale = 1f;
    }

    private void OnEnable()
    {
        pausar = canonControls.Canon.Pausa;
        pausar.Enable();
        pausar.performed += Pausar;
    }

    private void OnDisable()
    {
        pausar.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Salud");
        //AdministradorJuego.DisparosPorJuego = 3;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pausar(InputAction.CallbackContext context) {
        PausarBoton();
    }

    public void PausarBoton() {
        if (!AdministradorJuego.matchEnded) {
            Debug.Log("Pausa");
            if (isPaused)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                isPaused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
        }

    }

    public void Salir() {
        SceneManager.LoadScene("Menu");
    }

    public void Reiniciar() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
