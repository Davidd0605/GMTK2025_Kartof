using UnityEngine;

public class RoomController : MonoBehaviour
{

    [SerializeField] private Transform cameraPosition;
    [SerializeField] public Transform playerStartPoint;

    public void TransportPlayer(GameObject player, Vector3 displacement)
    {
        player.transform.position = playerStartPoint.position + displacement;
    }

    public void TransportCamera(GameObject camera)
    {
        camera.transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, -10);
        camera.GetComponent<CameraMovement>().clamp = cameraPosition.position.x - 1.5f;
    }
}
