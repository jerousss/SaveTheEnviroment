using Unity.Mathematics;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class InicioJugador : MonoBehaviour
{
    private GameObject jugadorInstanciado;

    void Start()
    {
        int indexJugador = PlayerPrefs.GetInt("JugadorIndex");
        jugadorInstanciado= Instantiate(GameManager.instance.personajes[indexJugador].personajeJugable, transform.position, Quaternion.identity);
    }

   void FixedUpdate()
    {
        
        if (jugadorInstanciado != null)
        {
            transform.position = jugadorInstanciado.transform.position;
        }
    }
    
//clase para llamar al personaje selecionado de la lista en un game object y poder instanciarlo en el mapa 
}
