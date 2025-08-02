using UnityEngine;

public class futureTree : MonoBehaviour
{


    private GameObject pastTree;
    public bool chopped;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite stage0;
    [SerializeField] private Sprite stage1;
    [SerializeField] private Sprite stage2;

    void Start()
    {
        pastTree = GameObject.Find("pastTreeObject");
        chopped = false;
    }

    void Update()
    {
        
    }
}
