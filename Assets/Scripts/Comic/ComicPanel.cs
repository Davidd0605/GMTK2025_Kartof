using NUnit.Framework;
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
        foreach (GameObject panel in subPanels)
        {
            panel.SetActive(false);
        }

        if(subPanels.Count <= 1)
        {
            readyToAdvance = true;
        }
    }

    private void Update()
    {
        if(!comicInteractable)
        {
            return;
        }

        if (readyToAdvance)
        {
            delayedReady = true;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DisplayNextSubPanel();
        }
    }

    public void ActivatePanel()
    {
        DisplayNextSubPanel();
    }

    private void DisplayNextSubPanel()
    {
        comicInteractable = true;

        subPanels[currentSubPanel].SetActive(true);
        currentSubPanel++;

        if (currentSubPanel >= subPanels.Count)
        {
            readyToAdvance = true;
        }
    }
 }
