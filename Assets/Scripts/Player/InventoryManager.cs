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

    public void changeSelectedIndex(int index)
    {
        Debug.Log("Changing cursor sprite");
        selectedIndex = index;

        if (itemIdList[index] != -1)
        {

            setCursor(buttonList[index].image.sprite);
        }
    }

    public void addItem(int id, Sprite sprite)
    {
        bool added = false;
        
        for (int i = 0; i < itemIdList.Length; i++)
        {
            if (itemIdList[i] == -1)
            {
                itemIdList[i] = id;
                buttonList[i].image.sprite = sprite;
                added = true;
                break;
            }
        }

        if (!added)
        {
            Debug.LogError("Inventory full! Cannot add anymore items.");
        }
    }

    public void removeItem(int index)
    {
        itemIdList[index] = -1;
        buttonList[index].image.sprite = emptySlotSprite;
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
