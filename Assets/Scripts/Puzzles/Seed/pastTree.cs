using UnityEngine;

public class pastTree : MonoBehaviour
{


    private GameObject player;
    public GameObject dialogueBox;
    public bool planted;

    public string[] lines;

    void Start()
    {
        player = GameObject.Find("Player");
        planted = false;
    }
    private void OnMouseDown()
    {
        dialogueBox.GetComponent<DialogueBox>().clearAllDialogue();
        if (!planted)
        {
            if (player.GetComponent<InventoryManager>().getHeldItemId() == 4)
            {
                planted = true;
                dialogueBox.GetComponent<DialogueBox>().addLine("I planted the seed.");
                dialogueBox.GetComponent<DialogueBox>().addLine("I should check on it later.");
                player.GetComponent<InventoryManager>().removeItem(player.GetComponent<InventoryManager>().selectedIndex);
                player.GetComponent<InventoryManager>().clearHeldItem();
            }
            else
            {
                dialogueBox.GetComponent<DialogueBox>().addLine("Looks like some kind of flower pot.");
                dialogueBox.GetComponent<DialogueBox>().addLine("Maybe I can grow something here.");
            }
        } else
        {
            dialogueBox.GetComponent<DialogueBox>().addLine("I should check on the seed later.");
        }

    }
}
