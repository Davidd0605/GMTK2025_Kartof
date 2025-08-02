using System;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    [SerializeField] private int windUpKeyItemId;
    [SerializeField] private InventoryManager inventory; 

    [SerializeField] private LayerMask targetLayer;

    private Boolean useLock = false;


    private void Update()
    {
        if(useLock)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 10f, targetLayer);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                CheckItemClick();
            }
        }
    }

    private void CheckItemClick()
    {
        //Debug.Log("with item id =" + inventory.getHeldItemId());
        if(inventory.getHeldItemId() == windUpKeyItemId)
        {
            ActivateDino();
        }
    }

    private void ActivateDino()
    {
        useLock = true;
        inventory.removeItem(inventory.selectedIndex);
        inventory.clearHeldItem();
        GetComponent<DinoMovement>().StartMovement();
    }

}
