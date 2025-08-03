using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextInput : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private DialogueBox dialogue;
    [SerializeField] private String sucessText;
    [SerializeField] private String failedText;

    public void PlaySound()
    {
        audioManager.Play("digit", 1f);
    }
    public void Check(string inputText)
    {
        if(inputText == "7472")
        {
            dialogue.clearBox();
            dialogue.addLine(sucessText);
            Invoke("LoadVictoryScene", 2f);
        }
        else
        {
            dialogue.clearBox();
            dialogue.addLine(failedText);
        }
    }

    private void LoadVictoryScene()
    {
        SceneManager.LoadScene("VictoryScene");
    }

}
