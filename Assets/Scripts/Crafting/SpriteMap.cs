using System.Collections.Generic;
using UnityEngine;

public static class SpriteMap
{
    private static Dictionary<int, Sprite> map = new Dictionary<int, Sprite>();
    private static bool initialized = false;

    private static Dictionary<int, string> paths = new Dictionary<int, string>()
    {
        //add new item sprites here
        { 0, "Sprites/Images" },
        { 1, "Sprites/WOOD" },
        { 2, "Sprites/ROCK" },
        { 3, "Sprites/AXZ" }
    };

    public static void Initialize()
    {
        if (initialized) return;

        foreach (var kvp in paths)
        {
            Sprite sprite = Resources.Load<Sprite>(kvp.Value);
            if (sprite != null)
            {
                map[kvp.Key] = sprite;
            }
            else
            {
                Debug.LogWarning($"Sprite not found at path: {kvp.Value}");
            }
        }

        initialized = true;
    }

    public static Sprite GetSprite(int id)
    {
        if (!initialized) Initialize();

        return map.TryGetValue(id, out var sprite) ? sprite : null;
    }
}
