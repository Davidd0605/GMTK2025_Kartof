using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class DialogueBox : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public GameObject textBox;
    private Queue<string> lines = new Queue<string>();
    private string currentLine;
    [SerializeField] private float textSpeed;
    private bool isTyping;

    void Start()
    {
        textBox.SetActive(false);
        textComponent.text = string.Empty;
        currentLine = string.Empty;
        isTyping = false;

    }

    void Update()
    {
        if (isTyping && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            textComponent.text = currentLine;
            isTyping = false;
            return;
        }
        //If can pull, pull
        if (lines.Count != 0 && !isTyping)
        {
            if (textComponent.text == string.Empty)
            {
                StartDialogue();
            } else
            {
                if(Input.GetMouseButtonDown(0))
                {
                    StartDialogue();
                }
            }

        }

        if (lines.Count == 0 && !isTyping && Input.GetMouseButtonDown(0))
        {
            textComponent.text = string.Empty;
            textBox.SetActive(false);
        }
    }

    private void StartDialogue()
    {
        //dequeue current line, set the thing to active and start typing.
        isTyping = true;
        currentLine = lines.Dequeue();
        textComponent.text = string.Empty;
        textBox.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in currentLine)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }


    public void addLine(string line)
    {
        lines.Enqueue(line);
    }


}