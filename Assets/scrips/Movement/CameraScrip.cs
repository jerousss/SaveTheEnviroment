using UnityEngine;

public class CameraScrip : MonoBehaviour
{
 public GameObject archer;
    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x =    archer.transform.position.x;
        transform.position = position;
        
    }
}
