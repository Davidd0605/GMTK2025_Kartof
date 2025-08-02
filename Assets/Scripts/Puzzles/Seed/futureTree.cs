using UnityEngine;

public class futureTree : MonoBehaviour
{


    private GameObject pastTree;
    private GameObject player;
    public GameObject dialogueBox;
    public bool chopped;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite stage0;
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject nonevents;
    public string[] lines;

    private bool swapped;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stage0;
        pastTree = GameObject.Find("pastTreeObject");
        player = GameObject.Find("Player");
        chopped = false;
        swapped = false;
    }

    void Update()
    {
        if (pastTree.GetComponent<pastTree>().planted == true && swapped == false)
        {
            spriteRenderer.sprite = stage1;
            setFullGrown();
        }

        if (chopped == true)
        {
            spriteRenderer.sprite = stage2;
            setChopped();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            nonevents.SetActive(true);
            ui.SetActive(false);
            player.GetComponent<PlayerMovement>().midEvent = false;
        }
    }

    private void OnMouseDown()
    {
        if (pastTree.GetComponent<pastTree>().planted == true)
        {
            if (!chopped)
            {
                if (player.GetComponent<InventoryManager>().getHeldItemId() == 3)
                {
                    dialogueBox.GetComponent<DialogueBox>().addLine("Timbeeeeeer!!!");
                    chopped = true;

                }
                else
                {
                    dialogueBox.GetComponent<DialogueBox>().addLine("Maybe I can craft something to chop this tree.");
                }
            } else if (chopped && player.GetComponent<PlayerMovement>().canInteract)
            {
                audioManager.Play("pickup", 1f);
                player.GetComponent<PlayerMovement>().midEvent = true;
                GameObject dialogueManager = GameObject.Find("DialogueManager");
                foreach (var ln in lines)
                {
                    dialogueManager.GetComponent<DialogueBox>().addLine(ln);
                }
                nonevents.SetActive(false);
                ui.SetActive(true);
            }
        }
    }

    private void setFullGrown()
    {
        transform.localPosition = new Vector3(30.15f, 0, 0);
        transform.localScale = new Vector3(0.2095707f, 0.3546388f, 1);
    }

    private void setChopped()
    {
        transform.localPosition = new Vector3(29.91f, -2.42f, 0);
        transform.localScale = new Vector3(0.27f, 0.35f, 1);
    }


}
