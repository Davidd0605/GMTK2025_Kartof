using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;

    public void LoadScene()
    {
        SceneManager.LoadScene("ComicScene");
    }

    public void LoadOptionsMenu()
    {
        optionsCanvas.SetActive(true);
    }

    public void LoadMainMenu()
    {
        optionsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}