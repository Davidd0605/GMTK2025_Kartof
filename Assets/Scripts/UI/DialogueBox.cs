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
    [SerializeField] private AudioManager audioManager;
    private bool isTyping;


    private int currentScene;
    private bool visitedPresentFirst = false;
    private bool visitedPresentSecond = false;
    private bool looped = false;

    [SerializeField] private GameObject sceneManager;

    void Start()
    {
        textBox.SetActive(false);
        textComponent.text = string.Empty;
        currentLine = string.Empty;
        isTyping = false;
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        currentScene = sceneManager.GetComponent<SceneController>().getCurrentRoom();
        sceneMessages();
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
            clearBox();
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
            audioManager.Play("talk", Random.Range(0.9f, 1.1f));
        }
        isTyping = false;
    }


    public void addLine(string line)
    {
        lines.Enqueue(line);
    }

    public void clearBox()
    {
        textComponent.text = string.Empty;
        textBox.SetActive(false);
    }

    private void sceneMessages()
    {
        if (currentScene == 2)
        {
            if (visitedPresentFirst == false)
            {
                visitedPresentFirst = true;
                addLine("I need to find out what that password is...");
                addLine("Maybe I can find some clues around here.");
                return;
            }

            if (visitedPresentSecond == false && looped == true)
            {
                visitedPresentSecond = true;
                addLine("Ugh... What!?");
                addLine("It seems I am stuck in a loop.");
                return;
            }
        } else
        {
            looped = true;
        }
    }
}