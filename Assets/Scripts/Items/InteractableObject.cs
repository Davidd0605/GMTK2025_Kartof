using UnityEngine;

public class InteractableObject : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float pickupThreshold = 2.0f;
    [SerializeField] private int itemId;
    [SerializeField] private AudioManager audioManager;

    public Material highlightMaterial;
    public Material defaultMaterial;

    [SerializeField] string[] lines;
    private bool swapped;
    // Update is called once per frame

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        swapped = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance < pickupThreshold)
        {

            
            GetComponent<SpriteRenderer>().material = highlightMaterial;
            GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", Mathf.Abs(Mathf.Sin(Time.time) * 10));
            if (Input.GetKeyDown(KeyCode.E))
            {
                try
                {
                    player.GetComponent<InventoryManager>().addItem(itemId);
                    GameObject dialogueManager = GameObject.Find("DialogueManager");
                    dialogueManager.GetComponent<DialogueBox>().clearAllDialogue();
                    foreach (var ln in lines)
                    {
                        dialogueManager.GetComponent<DialogueBox>().addLine(ln);
                    }
                    audioManager.Play("pickup", Random.Range(0.9f, 1.1f));
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
