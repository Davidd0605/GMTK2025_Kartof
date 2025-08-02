using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float moveDuration = 2f;
    [SerializeField] private float wobbleAmplitude = 0.1f;
    [SerializeField] private float wobbleFrequency = 10f;
    [SerializeField] private Sprite startSprite;
    [SerializeField] private Sprite endSprite;

    private Vector3 startPosition;
    private float elapsedTime = 0f;
    private bool isMoving = false;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float uprightReturnSpeed = 5f;
    private bool isReturningUpright = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isMoving)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            //move
            Vector2 linearPosition = Vector2.Lerp(startPosition, targetPosition.position, t);
            transform.position = linearPosition;

            //wobble
            float wobbleAngle = Mathf.Sin(elapsedTime * wobbleFrequency) * wobbleAmplitude;
            transform.rotation = Quaternion.Euler(0f, 0f, wobbleAngle);

            if (t >= 1f)
            {
                isMoving = false;
                isReturningUpright = true;
                if (endSprite != null)
                    spriteRenderer.sprite = endSprite;
            }
        }
        else if (isReturningUpright)
        {
            //rotate back
            Quaternion targetRotation = Quaternion.identity;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, uprightReturnSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isReturningUpright = false;
            }
        }
    }

    public void StartMovement()
    {
        spriteRenderer.sprite = startSprite;
        startPosition = transform.position;
        elapsedTime = 0f;
        isMoving = true;
    }
}
