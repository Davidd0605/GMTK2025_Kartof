using UnityEngine;

public class UIInteractable : MonoBehaviour
{

    private GameObject player;

    [SerializeField] private float pickupThreshold = 2.0f;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject nonevents;

    public Material highlightMaterial;
    public Material defaultMaterial;

    [SerializeField] string[] lines;
    private bool swapped;
    // Update is called once per frame

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        swapped = false;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);

        if (distance < pickupThreshold)
        {


            GetComponent<SpriteRenderer>().material = highlightMaterial;
            GetComponent<SpriteRenderer>().material.SetFloat("_Thickness", Mathf.Abs(Mathf.Sin(Time.time) * 10));
            if (Input.GetKeyDown(KeyCode.E))
            {
                GameObject dialogueManager = GameObject.Find("DialogueManager");
                nonevents.SetActive(false);
                ui.SetActive(true);
                Time.timeScale = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                nonevents.SetActive(true);
                ui.SetActive(false);
                Time.timeScale = 1f;
            }
            swapped = false;
        }
        else
        {
            if (swapped == false)
            {
                swapped = true;
                GetComponent<SpriteRenderer>().material = defaultMaterial;
            }
        }
    }
}
