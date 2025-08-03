using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private int[] itemIdList = new int[5];
    [SerializeField] private Button[] buttonList = new Button[5];
    [SerializeField] private Sprite emptySlotSprite;
    [SerializeField] private AudioManager audioManager;
    public int selectedIndex = -1;
    [SerializeField] private Color higlight;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        for (int i = 0; i < itemIdList.Length; i++)
        {
            itemIdList[i] = -1;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            clearHeldItem();
        }
    }

    private void changeSelectedIndex(int index)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].image.color = Color.white;
        }

        selectedIndex = index;

        if (index != -1 && itemIdList[index] != -1)
        {
            buttonList[index].image.color = higlight;
            setCursor(buttonList[index].image.sprite);
        }
        else
        {
            resetCursor();
        }
    }

    public void handleInventoryClick(int index)
    {
        if (selectedIndex != -1)
        {
            if (ItemInteractionMatrix.GetCombinationResult(itemIdList[selectedIndex], itemIdList[index]) != -1)
            {
                audioManager.Play("craft", 1f);
                itemIdList[index] = ItemInteractionMatrix.GetCombinationResult(itemIdList[selectedIndex], itemIdList[index]);
                buttonList[index].image.sprite = SpriteMap.GetSprite(itemIdList[index]);
                removeItem(selectedIndex);
            }
            else
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

    public int getHeldItemId()
    {
        if (selectedIndex != -1)
        {
            return itemIdList[selectedIndex];
        }
        else
        {
            return -1;
        }
    }

    public void clearHeldItem()
    {
        changeSelectedIndex(-1);
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
