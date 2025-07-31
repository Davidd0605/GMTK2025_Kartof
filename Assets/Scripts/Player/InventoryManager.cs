using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{

    private int[] itemIdList = new int[5];
    [SerializeField] private Button[] buttonList = new Button[5];
    [SerializeField] private Sprite emptySlotSprite;
    private int selectedIndex = -1;



    private void Start()
    {
        for (int i = 0; i < itemIdList.Length; i++)
        {
            itemIdList[i] = -1;
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedIndex = -1;
            resetCursor();
        }
    }

    private void changeSelectedIndex(int index)
    {
        selectedIndex = index;
        if (index != -1 && itemIdList[index] != -1)
        {

            setCursor(buttonList[index].image.sprite);
        }
    }

    public void handleInventoryClick(int index)
    {
        if (selectedIndex != -1)
        {
            if(ItemInteractionMatrix
                .GetCombinationResult(itemIdList[selectedIndex], itemIdList[index]) != -1) //if there is some combination
            {
                itemIdList[index] = ItemInteractionMatrix
                .GetCombinationResult(itemIdList[selectedIndex], itemIdList[index]);
                buttonList[index].image.sprite = SpriteMap.GetSprite(itemIdList[index]);
                removeItem(selectedIndex);

            } else
            {
                swapItems(index, selectedIndex);
            }
            index = -1;
            resetCursor();
        } 
        changeSelectedIndex(index);
    }

    public void addItem(int id)
    {
        bool added = false;
        
        for (int i = 0; i < itemIdList.Length; i++)
        {
            if (itemIdList[i] == -1)
            {
                itemIdList[i] = id;
                buttonList[i].image.sprite = SpriteMap.GetSprite(id);
                added = true;
                break;
            }
        }

        if (!added)
        {
            throw new System.InvalidOperationException("Inventory is full");
        }
    }
    public void removeItem(int index)
    {
        itemIdList[index] = -1;
        buttonList[index].image.sprite = emptySlotSprite;
    }
    private void swapItems(int index1, int index2)
    {
        Sprite auxSprite = buttonList[index1].image.sprite;
        buttonList[index1].image.sprite = buttonList[index2].image.sprite;
        buttonList[index2].image.sprite = auxSprite;

        int auxValue = itemIdList[index1];
        itemIdList[index1] = itemIdList[index2];
        itemIdList[index2] = auxValue;
    }
    private void setCursor(Sprite sprite)
    {
        if (sprite == null)
        {
            Debug.LogWarning("Cursor sprite is null.");
            return;
        }

        Texture2D cursorTexture = sprite.texture;

        Vector2 hotspot = new Vector2(
            sprite.textureRect.width / 2,
            sprite.textureRect.height / 2
        );

        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
    private void resetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
