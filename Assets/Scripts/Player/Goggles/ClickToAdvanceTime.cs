using UnityEngine;

public class ClickToAdvanceTime : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;

    private void Click()
    {
        Debug.Log("Clicked");

        if (sceneController == null)
        {
            return;
        }

        sceneController.AdvanceTime();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get all colliders at the click point
            Collider2D[] hits = Physics2D.OverlapPointAll(worldPoint);

            foreach (var hit in hits)
            {
                if (hit.gameObject == this.gameObject)
                {
                    Click();
                    break;
                }
            }
        }
    }
}
