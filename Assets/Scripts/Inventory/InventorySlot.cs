using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int slotID;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
    }

    public void OnClick()
    {
        inventoryManager.changeSelectedIndex(slotID);
    }
}
