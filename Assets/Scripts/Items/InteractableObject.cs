using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float pickupThreshold = 2.0f;
    [SerializeField] private int itemId;


    public Material highlightMaterial;
    public Material defaultMaterial;


    private bool swapped;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swapped = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance < pickupThreshold)
        {
            GetComponent<SpriteRenderer>().material = highlightMaterial;
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
            swapped = false;
        } else
        {
            if (swapped == false)
            {
                swapped = true;
                GetComponent<SpriteRenderer>().material = defaultMaterial;
            }
        }
    }
}
