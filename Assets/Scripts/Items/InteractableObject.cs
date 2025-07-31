using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float pickupThreshold = 2.0f;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance < pickupThreshold)
        {
            //Enable outline
            if (Input.GetKeyDown(KeyCode.E)) //pickup
            {
                Destroy(gameObject);
                player.GetComponent<InventoryManager>().addItem(2, gameObject.GetComponent<SpriteRenderer>().sprite);
            }
        }
    }
}
