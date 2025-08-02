using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsVictoryMenu : MonoBehaviour
{

    public void LoadMainMenu()
    {
        //call to main menu
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
