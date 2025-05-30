using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoPersonaje", menuName = "Personaje")]
public class Personaje : ScriptableObject
{
    public GameObject personajeJugable;
    public Sprite imagen;
    public String nombre;
    // esto basicamente es para tener los peronajes en un archivo en unity y poder darle nombre e imagen en el selector 
}
