using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float timeSpentInScene = 4f;
    [SerializeField] private int nextScene = 0;
    [SerializeField] private bool canSwitch = true;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    [SerializeField] private TimeTravelEffect timeTravelEffect;

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

        timeTravelEffect.TriggerTimeTravel();
        yield return new WaitForSeconds(timeTravelEffect.effectDuration + timeTravelEffect.effectDisplacement);

        player.transform.position = new Vector2(player.transform.position.x, -3 + sceneIndex*50);
        camera.transform.position = new Vector3(camera.transform.position.x, sceneIndex*50, -10);
        if (sceneIndex < 2)
            sceneIndex++;
        else
            sceneIndex = -2;
        nextScene = sceneIndex;
        canSwitch = true;

    }
}
