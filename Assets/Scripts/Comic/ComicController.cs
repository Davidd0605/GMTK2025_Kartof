using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class ComicController : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels = new List<GameObject>();
    [SerializeField] private Vector3 exitPosition;
    [SerializeField] private Vector3 entryPosition;
    [SerializeField] private Vector3 stayPosition;

    private int panelIndex = 0;

    void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            if (i == 0)
                panels[i].transform.position = stayPosition;
            else
                panels[i].transform.position = entryPosition;
        }
    }


    void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            AdvanceComic();
        }
    }

    [SerializeField] private float moveDuration = 0.5f;

    public void AdvanceComic()
    {
        if (panelIndex < panels.Count - 1)
        {
            GameObject currentPanel = panels[panelIndex];
            GameObject nextPanel = panels[panelIndex + 1];

            //move current out, move next in
            StartCoroutine(MovePanel(currentPanel, currentPanel.transform.position, exitPosition, moveDuration));
            nextPanel.transform.position = entryPosition;
            StartCoroutine(MovePanel(nextPanel, entryPosition, stayPosition, moveDuration));

            panelIndex++;
        }
        else
        {
            Debug.Log("Comic finished. Load next scene.");
            SceneManager.LoadScene("PuzzleScene");
        }
    }


    private IEnumerator MovePanel(GameObject panel, Vector3 from, Vector3 to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            panel.transform.position = Vector3.Lerp(from, to, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        panel.transform.position = to;
    }

}
