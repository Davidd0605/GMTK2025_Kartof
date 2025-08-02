using UnityEngine;

public class CreditsMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float stopAtY;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed*Time.deltaTime, transform.position.z); 
    }
}
