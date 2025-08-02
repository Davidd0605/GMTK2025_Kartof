using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextInput : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private DialogueBox dialogue;
    [SerializeField] private String failedText;

    public void PlaySound()
    {
        audioManager.Play("digit", 1f);
    }
    public void Check(string inputText)
    {
        if(inputText == "7472")
        {
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            dialogue.clearBox();
            dialogue.addLine(failedText);
        }
    }

}
