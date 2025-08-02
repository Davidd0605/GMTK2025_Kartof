using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsVictoryMenu : MonoBehaviour
{

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
