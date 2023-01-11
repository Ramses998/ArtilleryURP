using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Canon : MonoBehaviour
{
    public static bool Bloqueado;

    public AudioClip clipDisparo;
    public AudioSource musica;
    private GameObject SonidoDisparo;
    private AudioSource SourceDisparo;

    [SerializeField] private GameObject BalaPrefab;
    public GameObject ParticulasDisparo;
    private GameObject puntaCanon;
    private float rotacion;

    public CanonControls canonControls;
    private InputAction apuntar;
    private InputAction modificarFuerza;
    private InputAction disparar, subir, bajar;
    private int velocidad;

    public Slider velocidadSlider;

    public Opciones opciones;

    private void Awake()
    {
        canonControls = new CanonControls();
    }

    private void OnEnable()
    {
        apuntar = canonControls.Canon.Apuntar;
        modificarFuerza = canonControls.Canon.ModificarFuerza;

        disparar = canonControls.Canon.Disparar;
        subir = canonControls.Canon.Subir;
        bajar = canonControls.Canon.Bajar;

        apuntar.Enable();
        modificarFuerza.Enable();
        disparar.Enable();
        subir.Enable();
        bajar.Enable();

        disparar.performed += Disparar;
        subir.performed += Subir;
        bajar.performed += Bajar;
    }

    private void Start()
    {
        velocidadSlider = FindObjectOfType<Slider>();
        puntaCanon = transform.Find("PuntaCanon").gameObject;
        SonidoDisparo = GameObject.Find("SonidoDisparo");
        SourceDisparo = SonidoDisparo.GetComponent<AudioSource>();

        opciones.Cargar();
        if (!opciones.musica) {
            musica.mute = true;
        }
    }

    void Update()
    {
        if (!Bloqueado) {
            rotacion += apuntar.ReadValue<float>() * AdministradorJuego.VelocidadRotacion;
        } 
        if (rotacion <= 90 && !Bloqueado) {
            transform.eulerAngles = new Vector3(rotacion, 90, 0.0f);
        }

        if (rotacion > 90) rotacion = 90;
        if (rotacion < 0) rotacion = 0;

        //velocidadSlider.value += modificarFuerza.ReadValue<float>();
    }

    private void Disparar(InputAction.CallbackContext context)
    {
        if (!Bloqueado) {
            velocidad = 4 * (int)velocidadSlider.value;
            GameObject temp = Instantiate(BalaPrefab, puntaCanon.transform.position, transform.rotation);
            Rigidbody tempRB = temp.GetComponent<Rigidbody>();
            SeguirCamara.objetivo = temp;  //Aquí se define que la bala instanciada es el objetivo.
            Vector3 direccionDisparo = transform.rotation.eulerAngles;
            direccionDisparo.y = 90 - direccionDisparo.x;
            Vector3 direccionParticulas = new Vector3(-90 + direccionDisparo.x, 90, 0);
            GameObject Particulas = Instantiate
                (ParticulasDisparo, puntaCanon.transform.position, Quaternion.Euler(direccionParticulas), transform);
            //tempRB.velocity = direccionDisparo.normalized * AdministradorJuego.VelocidadBala;
            tempRB.velocity = direccionDisparo.normalized * velocidad;
            AdministradorJuego.DisparosPorJuego--;
            if (opciones.sonido == true) {
                SourceDisparo.PlayOneShot(clipDisparo);
            }
            Bloqueado = true;
        }

    }

    private void Subir(InputAction.CallbackContext context) {
        velocidadSlider.value += 1;
    }

    private void Bajar(InputAction.CallbackContext context) {
        velocidadSlider.value -= 1;
    }
}
