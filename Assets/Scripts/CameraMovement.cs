using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothing;
    private Vector3 targetPosition;
    public float clamp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != player.position)
        {

            targetPosition = new Vector3(player.position.x, transform.position.y, -10);
            targetPosition.x = Mathf.Clamp(targetPosition.x, clamp, clamp+1.5f);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
