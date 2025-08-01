using UnityEngine;

public class ClickToAdvanceTime : MonoBehaviour
{
    [SerializeField] private SceneController sceneController;
    [SerializeField] private LayerMask targetLayer;

    private void Click()
    {
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

            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, 10f, targetLayer);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Click();
            }
        }
    }

}
