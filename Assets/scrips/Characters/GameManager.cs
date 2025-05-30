using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] public static GameManager instance;
    [SerializeField] public List<Personaje> personajes;

    private void Awake()
    {
        // esto es para el selector, es el game manager hace que se active el personaje de la lista y lo destruya cuando se acabe
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
