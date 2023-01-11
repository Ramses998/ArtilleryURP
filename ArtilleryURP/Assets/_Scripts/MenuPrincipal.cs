using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject MenuOpciones;
    public GameObject MenuInicial;
    [SerializeField] Button musicButton;
    [SerializeField] Button soundButton;

    [SerializeField] Text musicText;
    [SerializeField] Text soundText;
    [SerializeField] AudioSource musica;

    public Opciones opciones;

    private void Start()
    {
        opciones.Cargar();

        if (opciones.musica == true)
        {
            musicText.text = "Sí";
        }
        else {
            musicText.text = "No";
            musica.mute = true;
        }

        if (opciones.sonido == true)
        {
            soundText.text = "Sí";
        }
        else
        {
            soundText.text = "No";
        }
    }

    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
    }

    public void FinalizarJuego()
    {
        Application.Quit();
    }

    public void MostrarOpciones()
    {
        MenuInicial.SetActive(false);
        MenuOpciones.SetActive(true);
    }

    public void MostrarMenuIniciar()
    {
        MenuInicial.SetActive(true);
        MenuOpciones.SetActive(false);
    }

    public void ModificarMusica()
    {
        if (musicText.text == "Sí")
        {
            musicText.text = "No";
            musica.mute = true;
            opciones.musica = false;
        }
        else {
            musicText.text = "Sí";
            musica.mute = false;
            opciones.musica = true;
        }
        opciones.Guardar();
    }

    public void ModificarSonido()
    {
        if (soundText.text == "Sí")
        {
            soundText.text = "No";
            opciones.sonido = false;
        }
        else
        {
            soundText.text = "Sí";
            opciones.sonido = true;
        }
        opciones.Guardar();
    }
}
