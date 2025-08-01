using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float timeSpentInScene = 4f;
    [SerializeField] private int nextScene = 0;
    [SerializeField] private bool canSwitch = true;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject wheel;
    [SerializeField] private TimeTravelEffect timeTravelEffect;

    private bool isTransitioning = false;
    private float transitionTimer = 0f;
    private float postEffectDelay = 0f;

    private void Start()
    {
        AdvanceTime();
    }

    void Update()
    {
        if (isTransitioning)
        {
            //waiting for effect duration before finishing transition
            if (postEffectDelay > 0)
            {
                postEffectDelay -= Time.deltaTime;
                if (postEffectDelay <= 0)
                {
                    FinishTransition();
                }
            }
            return;
        }

        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
            if (transitionTimer <= 0)
            {
                StartTransition();
            }
        }
    }

    public void AdvanceTime()
    {
        if (!isTransitioning)
        {
            StartTransition();
        }
    }

    private void StartTransition()
    {
        isTransitioning = true;

        if (timeTravelEffect != null)
        {
            timeTravelEffect.TriggerTimeTravel();
            postEffectDelay = timeTravelEffect.effectDuration + timeTravelEffect.effectDisplacement;
        }
        else
        {
            postEffectDelay = 0f;
        }
    }

    private void FinishTransition()
    {

        player.transform.position = new Vector2(player.transform.position.x, -3 + nextScene * 50);

        camera.transform.position = new Vector3(camera.transform.position.x, nextScene * 50, -10);


        Animator anim = wheel.GetComponent<Animator>();
        if (anim != null)
            anim.SetInteger("Timeline", nextScene);


        nextScene = (nextScene < 2) ? nextScene + 1 : -2;

        //reset flags
        isTransitioning = false;
        transitionTimer = timeSpentInScene;
        postEffectDelay = 0f;
    }
}
