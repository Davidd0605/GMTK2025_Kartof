using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float timeSpentInScene = 4f;
    [SerializeField] private int nextScene = 2;
    [SerializeField] private bool canSwitch = true;
    private static SceneController instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSwitch)
        {
            canSwitch = false;
            StartCoroutine(LoadScene(nextScene));
        }
    }

    private IEnumerator LoadScene(int sceneIndex)
    {
        Debug.Log(sceneIndex);
        yield return new WaitForSeconds(timeSpentInScene);
        SceneManager.LoadScene("Timeline"+sceneIndex);
        if (sceneIndex < 5)
            sceneIndex++;
        else
            sceneIndex = 1;
        nextScene = sceneIndex;
        canSwitch = true;
    }
}
