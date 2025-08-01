using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float pickupThreshold = 2.0f;
    [SerializeField] private int itemId;
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
            if (Input.GetKeyDown(KeyCode.E)) //pickup
            {
                //check if can be added
                try
                {
                    player.GetComponent<InventoryManager>().addItem(itemId);
                    Destroy(gameObject);

                } catch (System.InvalidOperationException e)
                {
                    //Display some message idk
                }

            }
        }
    }
}
