using UnityEngine;

public class presentTree : MonoBehaviour
{
    private GameObject pastTree;
    private GameObject player;
    public GameObject dialogueBox;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite stage0;
    [SerializeField] private Sprite stage1;

    private bool swapped;

    public string[] lines;
    void Start()
    {
        setTrash();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = stage0;
        pastTree = GameObject.Find("pastTreeObject");
        swapped = false;
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (pastTree.GetComponent<pastTree>().planted == true && swapped == false)
        {
            swapped = true;
            spriteRenderer.sprite = stage1;
            setTree();
        }
    }

    private void OnMouseDown()
    {
        if (swapped == true)
        {
            if (player.GetComponent<InventoryManager>().getHeldItemId() == 3)
            {
                dialogueBox.GetComponent<DialogueBox>().addLine("I should not chop it just yet.");
            }
            else
            {
                dialogueBox.GetComponent<DialogueBox>().addLine("They grow up so fast.");
            }
        } else
        {
            dialogueBox.GetComponent<DialogueBox>().addLine("Just a regular trash bin.");
        }

    }


    private void setTrash()
    {
        transform.position = new Vector3(7.282f, -2.47f, 0);
        transform.localScale = new Vector3(0.35f, 0.35f, 1);
    }

    private void setTree()
    {
        transform.position = new Vector3(7.38f, -0.97f, 0);
        transform.localScale = new Vector3(0.35f, 0.35f, 1);
    }
}
