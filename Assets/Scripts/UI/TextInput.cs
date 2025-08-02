using UnityEngine;
using UnityEngine.SceneManagement;

public class TextInput : MonoBehaviour
{
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
