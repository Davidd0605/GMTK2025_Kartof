using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float soundCooldown;
    private bool playSound = true;
    private Vector2 movement;
    private static PlayerMovement instance;

    void Update()
    {
        // Get horizontal input using the old Input system
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(horizontalInput, 0f).normalized;
        anim.SetInteger("Horizontal", (int)horizontalInput);
        if (horizontalInput != 0f)
            sprite.transform.localScale = new Vector2(horizontalInput * Mathf.Abs(sprite.transform.localScale.x),
                                                                        sprite.transform.localScale.y);
        if (playSound && horizontalInput != 0)
        {
            playSound = false;
            StartCoroutine(WalkingSound());
        }
    }

    IEnumerator WalkingSound()
    {
        audioManager.Play("walk");
        yield return new WaitForSeconds(soundCooldown);
        playSound = true;
    }

    void FixedUpdate()
    {
        // Use MovePosition for smooth physics-based movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
