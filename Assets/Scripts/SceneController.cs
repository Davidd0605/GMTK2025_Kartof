using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private List<RoomController> rooms = new List<RoomController>();
    [SerializeField] private int startingRoom = 2;
    [SerializeField] private int currentRoom;

    [SerializeField] private float timeSpentInScene = 4f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject wheel;
    [SerializeField] private TimeTravelEffect timeTravelEffect;

    private bool isTransitioning = false;
    private float transitionTimer = 0f;
    private float postEffectDelay = 0f;

    private void Start()
    {
        currentRoom = startingRoom;
        transitionTimer = timeSpentInScene;

        rooms[currentRoom].TransportPlayer(player, Vector3.zero);
        rooms[currentRoom].TransportCamera(camera);
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
        Vector3 displacement = player.transform.position - rooms[currentRoom].playerStartPoint.position;
        
        currentRoom = (++currentRoom)%rooms.Count;

        rooms[currentRoom].TransportPlayer(player, displacement);
        rooms[currentRoom].TransportCamera(camera);

        Animator anim = wheel.GetComponent<Animator>();
        if (anim != null)
            anim.SetInteger("Timeline", currentRoom-(rooms.Count/2));

        //reset flags
        isTransitioning = false;
        transitionTimer = timeSpentInScene;
        postEffectDelay = 0f;
    }
}
