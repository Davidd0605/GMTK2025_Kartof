using System;
using UnityEngine;

public class AnimalSequenceController : MonoBehaviour
{
    [SerializeField] private GameObject bear;
    [SerializeField] private GameObject destoryedBear;

    [SerializeField] private GameObject bull;
    [SerializeField] private GameObject lion;

    [SerializeField] private int bearPressesRequired;
    [SerializeField] private int bullPressesRequired;
    [SerializeField] private int lionPressesRequired;

    [SerializeField] private int currentBearPresses;
    [SerializeField] private int currentBullPresses;
    [SerializeField] private int currentLionPresses;

    [SerializeField] private LayerMask targetLayer;

    [SerializeField] private DialogueBox dialogue; // why isnt this a singleton?

    private Boolean useLock = false;

    private void Update()
    {
        if (useLock)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 10f, targetLayer);

            if (hit.collider != null)
            {
                CountAnimalClick(hit);
            }
        }
    }

    private void CountAnimalClick(RaycastHit2D hit)
    {
        if (hit.collider.gameObject == bear)
        {
            currentBearPresses++;
        }
        else if (hit.collider.gameObject == bull)
        {
            currentBullPresses++;
        }
        else if (hit.collider.gameObject == lion)
        {
            currentLionPresses++;
        }

        if (currentLionPresses > lionPressesRequired || currentBearPresses > bearPressesRequired || currentBullPresses > bullPressesRequired)
        {
            dialogue.GetComponent<DialogueBox>().clearAllDialogue();
            dialogue.addLine("Something isn't right, try again.");
            currentLionPresses = 0;
            currentBearPresses = 0;
            currentBullPresses = 0;
        }
        else if (currentBearPresses == bearPressesRequired && currentBullPresses == bullPressesRequired && currentLionPresses == lionPressesRequired)
        {
            destoryedBear.SetActive(true);
            bear.SetActive(false);
            useLock = true;
        }
    }
}
