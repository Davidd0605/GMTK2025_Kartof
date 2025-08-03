using System.Collections.Generic;
using UnityEngine;

public class ComicPanel : MonoBehaviour
{
    private bool comicInteractable = false;
    private bool readyToAdvance;
    public bool delayedReady = false;

    [SerializeField] private List<GameObject> subPanels = new List<GameObject>();
    private int currentSubPanel = 0;

    private void Start()
    {
        for (int i = 0; i < subPanels.Count; i++)
        {
            subPanels[i].SetActive(i == 0);
        }

        if (subPanels.Count <= 1)
        {
            readyToAdvance = true;
        }
    }

    public void ActivatePanel()
    {
        comicInteractable = true;
        DisplayNextSubPanel();
    }

    public bool TryAdvanceSubPanel()
    {
        if (!comicInteractable || readyToAdvance)
        {
            delayedReady = readyToAdvance;
            return false;
        }

        DisplayNextSubPanel();
        return true;
    }

    private void DisplayNextSubPanel()
    {
        if (currentSubPanel < subPanels.Count)
        {
            subPanels[currentSubPanel].SetActive(true);
            currentSubPanel++;
        }

        if (currentSubPanel >= subPanels.Count)
        {
            readyToAdvance = true;
        }
    }
}
