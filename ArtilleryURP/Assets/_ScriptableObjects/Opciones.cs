using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Opciones", menuName = "Herramientas/Opciones", order = 1)]

public class Opciones : SistemaGuardado
{
    public bool musica = true;
    public bool sonido = true;

    public void CambiarMusica(bool habraMusica) {
        musica = habraMusica;
    }

    public void CambiarSonido(bool habraSonido)
    {
        sonido = habraSonido;
    }
}
