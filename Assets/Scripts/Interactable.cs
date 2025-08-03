using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] string[] lines;

    private void OnMouseDown()
    {
        GameObject dialogueManager = GameObject.Find("DialogueManager");
        dialogueManager.GetComponent<DialogueBox>().clearAllDialogue();
        foreach (var ln in lines)
        {
            dialogueManager.GetComponent<DialogueBox>().addLine(ln);
        }
    }
}
