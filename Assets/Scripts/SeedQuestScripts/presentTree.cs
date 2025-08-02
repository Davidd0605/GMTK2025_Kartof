using UnityEngine;

public class presentTree : MonoBehaviour
{
    private GameObject pastTree;
    void Start()
    {
        pastTree = GameObject.Find("pastTreeObject");
    }

    void Update()
    {
        if (pastTree.GetComponent<pastTree>().planted == true)
        {
            //swap sprites

        }
    }
}
