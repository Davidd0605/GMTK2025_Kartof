using UnityEngine;
using UnityEngine.SceneManagement;

public class TextInput : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    public void PlaySound()
    {
        audioManager.Play("pickup", 2f);
    }
    public void Check(string inputText)
    {
        if(inputText == "7472")
        {
            SceneManager.LoadScene("VictoryScene");
        }
        else
        {
            Debug.Log(":(");
        }
    }

}
