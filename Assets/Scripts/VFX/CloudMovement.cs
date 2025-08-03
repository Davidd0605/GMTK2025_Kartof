using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 2f;

    private void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
    }

    private void Update()
    {
        if (startPoint == null || endPoint == null) return;

        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, speed * Time.deltaTime);

        //teleport back
        if (Vector3.Distance(transform.position, endPoint.position) < 0.01f)
        {
            transform.position = startPoint.position;
        }
    }
}
