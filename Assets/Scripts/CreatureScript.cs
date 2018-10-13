using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScript : MonoBehaviour
{

    public event Action OnStartMove;
    public event Action OnEndMove;
    public event Action OnStartTurn;
    public event Action OnEndTurn;
    public event Action OnStartDeath;
    public event Action OnEndDeath;
    public event Action OnStartCollide;
    public event Action OnEndCollide;
    public event Action OnStartChase;
    public event Action OnEndChase;

    [Header("Control")]
    public PlayerControl player;
    public Animation creatureAnimation;
    public AudioSource attackAudio;
    public AudioSource deathAudio;
    [Header("Animations")]
    public string turningAnimation;
    public string walkingAnimation;
    public string hitAnimation;
    public string deathAnimation;
    [Header("Movement")]
    public int maxWaypoints;
    public int speed = 1;
    public bool instantTurn = false;
    [Header("Attack")]
    public float attackJump = 0;
    public float attackDelay = 0;
    public bool canSee = true;
    public float viewDistance = 0;
    public float viewAngle = 0;
    [Header("HitPoints")]
    public int maxHP = 1;
    public int currentHP = 0;

    private int currentPoint = 0;
    private List<Transform> pointList = new List<Transform>();
    private bool turning = false;
    private bool dead = false;
    private bool finishedTurning = false;
    private bool chaseMode = false;
    
    public void Start()
    {
        for (int i = 0; i < maxWaypoints; i++)
        {
            Transform p = transform.Find("Point"+i.ToString());
            pointList.Add(p);
        }
        TeleportToPoint(currentPoint);
        creatureAnimation.Play(walkingAnimation);
        currentHP = maxHP;
    }

    private void Update()
    {

        if (dead) { return; }

        if (canSee && CanSeePlayer()) // player is in chase range of creature
        {
            if (!chaseMode) // chaseMode starting now
            {
                if (turning) { if (OnEndTurn != null) { OnEndTurn(); } }
                else { if (OnEndMove != null) { OnEndMove(); } }
                chaseMode = true;
                creatureAnimation.Play(walkingAnimation);
                attackAudio.Play();
                player.StartChase();
                if (OnStartChase != null) { OnStartChase(); }
            }

            ChaseTowardsTarget();
            return;
        }
        else if (chaseMode) // chaseMode stopping now, prep patrol mode
        {
            chaseMode = false;
            player.EndChase();
            if (OnEndChase != null) { OnEndChase(); }
            finishedTurning = false;
            turning = true;
            creatureAnimation.Play(turningAnimation);
            if (OnStartTurn != null) { OnStartTurn(); }
        }

        if (turning)
        {
            if (finishedTurning)
            { // Start Moving
                if (OnEndTurn != null) { OnEndTurn(); }
                turning = false;
                creatureAnimation.Play(walkingAnimation);
                if (OnStartMove != null) { OnStartMove(); }
            }   
            else
            { // Keep Turning
                TurnTowardsTarget(pointList[currentPoint].position);
            }
        }
        else // moving
        {
            if (AtTarget(pointList[currentPoint].position))
            { // Start Turning
                currentPoint++; if (currentPoint == maxWaypoints) { currentPoint = 0; }
                if (OnEndMove != null) { OnEndMove(); }

                if (instantTurn)
                {
                    FaceAtTarget(pointList[currentPoint].position);
                }
                else // go into animated turning mode
                {
                    finishedTurning = false;
                    turning = true;
                    creatureAnimation.Play(turningAnimation);
                }

                if (OnStartTurn != null) { OnStartTurn(); }
            }
            else
            { // Keep Moving
                MoveTowardsTarget(pointList[currentPoint].position);
            }
        }
    }

    public void StartCollide()
    {
        if (dead) { return; }
        dead = true;
        FaceAtTarget(player.transform.position);
        player.FaceAtTarget(creatureAnimation.transform.position);
        TeleportForward(attackJump);
        creatureAnimation.Play(hitAnimation);
        attackAudio.Play();
        if (OnStartCollide != null) { OnStartCollide(); }
        player.KillInSeconds(attackDelay);
        if (OnEndCollide != null) { OnEndCollide(); }
    }
    
    public void StartDeath()
    {
        if (dead) { return; }
        currentHP--;
        if (OnStartDeath != null) { OnStartDeath(); }
        if (currentHP < 1)
        {
            dead = true;
            Destroy(creatureAnimation.GetComponent<CapsuleCollider>());
            creatureAnimation.Play(deathAnimation);
            deathAudio.Play();
            Invoke("EndDeath", creatureAnimation.clip.length);
        }
    }

    private void EndDeath()
    {
        if (OnEndDeath != null) { OnEndDeath(); }
    }

    private bool CanSeePlayer()
    {
        RaycastHit hitInfo;
        Vector3 creaturePosition = creatureAnimation.transform.position;
        Vector3 playerPosition = player.transform.position;
        creaturePosition.y += 1; // check from chest not feet to get over stairs etc
        playerPosition.y += 1; // check from chest not feet to get over stairs etc

        if ( (Vector3.Distance(creaturePosition, playerPosition) < viewDistance) // player in range of creature view
            && (Vector3.Angle(creatureAnimation.transform.forward, (playerPosition - creaturePosition).normalized) < viewAngle) // player in angle of creature view
            && (Physics.Linecast(creaturePosition, playerPosition, out hitInfo)) // line from creature to player hit a collider
            && (hitInfo.transform.tag == "Player") ) // that collider belongs to the player and is not obstructed by other stuff
        {
            return true;
        }

        return false;
    }

    private void ChaseTowardsTarget()
    {
        TurnTowardsTarget(player.transform.position);
        MoveTowardsTarget(player.transform.position);

    }

    private bool AtTarget(Vector3 target)
    {
        return creatureAnimation.transform.position == target;
    }

    private void TeleportToPoint(int index)
    {
        creatureAnimation.transform.position = pointList[index].transform.position;
    }

    private void TeleportForward(float distance)
    {
        creatureAnimation.transform.position += creatureAnimation.transform.forward * distance;
    }

    private void FaceAtTarget(Vector3 target)
    {
        creatureAnimation.transform.LookAt(target);
    }

    private void MoveTowardsTarget(Vector3 target)
    {
        creatureAnimation.transform.position = Vector3.MoveTowards(creatureAnimation.transform.position, target, speed * Time.deltaTime);
    }
    
    private void TurnTowardsTarget(Vector3 target)
    {
        Quaternion q = Quaternion.LookRotation(target - creatureAnimation.transform.position);
        creatureAnimation.transform.rotation = Quaternion.RotateTowards(creatureAnimation.transform.rotation, q, speed * Time.deltaTime * 15);
        finishedTurning = (Quaternion.Angle(creatureAnimation.transform.rotation, q) < 0.05);
    }
    
}
