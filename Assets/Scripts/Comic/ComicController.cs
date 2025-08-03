using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicController : MonoBehaviour
{
    [SerializeField] private List<ComicPanel> panels = new List<ComicPanel>();
    [SerializeField] private Vector3 exitPosition;
    [SerializeField] private Vector3 entryPosition;
    [SerializeField] private Vector3 stayPosition;
    [SerializeField] private float moveDuration = 0.5f;

    private bool useLock = false;

    private int panelIndex = 0;

    void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].transform.position = i == 0 ? stayPosition : entryPosition;
        }

        panels[0].ActivatePanel();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ComicPanel currentPanel = panels[panelIndex];

            bool advanced = currentPanel.TryAdvanceSubPanel();

            if (!advanced && currentPanel.delayedReady)
            {
                AdvanceComic();
            }
        }
    }

    private void AdvanceComic()
    {
        if (panelIndex < panels.Count - 1)
        {
            ComicPanel currentPanel = panels[panelIndex];
            ComicPanel nextPanel = panels[panelIndex + 1];

            StartCoroutine(MovePanel(currentPanel, currentPanel.transform.position, exitPosition, moveDuration));
            nextPanel.transform.position = entryPosition;
            StartCoroutine(MovePanel(nextPanel, entryPosition, stayPosition, moveDuration));

            panelIndex++;
            panels[panelIndex].ActivatePanel();
        }
        else
        {
            Debug.Log("Comic finished. Load next scene.");
            if(!useLock)
            {
                useLock = true;
                LevelLoader.Instance.LoadNextLevel("PuzzleScene");
            }
            
        }
    }

    private IEnumerator MovePanel(ComicPanel panel, Vector3 from, Vector3 to, float duration)
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
