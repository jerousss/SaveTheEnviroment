using UnityEngine;

public class CameraScrip : MonoBehaviour
{
    public GameObject character;

    void Update()
    {
        Vector3 position = transform.position;
        position.x = character.transform.position.x;
        position.y = character.transform.position.y + 3f;
        transform.position = position;

    }
    // clase para que la camara siga al objeto donde se crea el jugador
}
