using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // to access NavMesh agent component

public class AIControllerOLD : MonoBehaviour
{
    public NavMeshAgent navMeshAgent; // nav mesh agent component access
    public int startWaitTime = 4; // wait time of every action
    public int rotateTime = 2;// wait time when the enemy detects near the player without seeing
    public int speed = 6; // walking speed
    public int speedRun = 9;// running speed

    public int viewRadius = 15; // radius of the enemy view
    public int viewAngle = 90; // angle of the enemy view
    public LayerMask playerMask;// detects player avatar with raycast
    public LayerMask obstacleMask; // detects obstacles with the raycast
    public float meshResolution = 1f; // how many rays will cast per degree
    public int edgeIterations = 4; // 
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;// positions where the enemy patrols, waypoints
    int CurrentWaypoint;//current waypoint on where the enemy is going to

    Vector3 playerLastPosition = Vector3.zero; // last position of the player avatar when it was near the enemy
    Vector3 PlayerPosition;// last position of the player avatar when the player was seen by the enemy

    float CurrentWaitTime;
    float CurrentTimeRotation;

    //bool IsPlayerInRange; //if the player is in range of vision, enemy goes into chasing state
    //bool IsPlayerNear; // if the player is near, enemy is in hearing state
    //bool IsPatrol; // if the enemy is patroling, no player detection, enemy is in patroling state
    //bool FoundPlayer; // if the enemy has caught the player, go into attacking state


    void Start()
    {
        PlayerPosition = Vector3.zero;
       // IsPatrol = true; // enemy starts in patrolling state
       // FoundPlayer = false;
        //IsPlayerInRange = false;
       // IsPlayerNear = false;
        CurrentWaitTime = startWaitTime; //wait time varibale will change later, in different states
        CurrentTimeRotation = rotateTime;

        CurrentWaypoint = 0; //starting waypoint
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed; //set navmesh speed to walking speed of enemy
        navMeshAgent.SetDestination(waypoints[CurrentWaypoint].position); // sets position destination of starting waypoint
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Step 3
    void Move(float speed) // Move function: sets nav mesh agent speed, makes sure enemy doesn't stop moving
    {
        navMeshAgent.isStopped = false;// character is moving
        navMeshAgent.speed = speed;// set to waling speed
    }
    // Step 3
    void Stop()// Stop function: stops nav mesh agent speed when needed
    {
        navMeshAgent.isStopped = true;// character has stopped
        navMeshAgent.speed = 0; // set speed to 0
    }
    // Step 2
    void PlayerFound ()// when player is found
    {
        //FoundPlayer = true;// returns variable to true
    }

    // Step 4
    void NextWayPoint()
    {
        CurrentWaypoint = (CurrentWaypoint + 1) % waypoints.Length;// changes to next waypoint by adding one to current value
        navMeshAgent.SetDestination(waypoints[CurrentWaypoint].position);//sets navmesh destination to next waypoint position
    }

    //Step 1
    void LookingForPlayer(Vector3 player) //last known position of the player avatar,
                                          //will go to that postion then return to patrol state
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3) // if distance to player is lower than or equal to 0.3
                                                                // (very close to the player avatar)
        {
            if(CurrentWaitTime <= 0) // however if current wait time is lower than or equal to 0
            {
                //IsPlayerNear = false;
                Move(speed);
                navMeshAgent.SetDestination(waypoints[CurrentWaypoint].position);
                CurrentWaitTime = startWaitTime;
                CurrentTimeRotation = rotateTime;
            }// All of this sets the enemy NPC back to the patroling state.
            else
            {
                Stop();
                CurrentWaitTime -= Time.deltaTime;
            }
        }
    }
    // Step 5
    void EnemyViewDetection()// how the enemy detects the player, changes to different states
    {
        Collider[] PlayerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask); //  Makes an overlap sphere around the enemy
                                                                                                      //  to detect the playermask layer (player avatar)
                                                                                                      //  in the view radius. The sphere is its view radius
        for (int i = 0; i < PlayerInRange.Length; i++) // Need to explain in more detail
        {
            Transform player = PlayerInRange[i].transform;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            //if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);          //  Distance of the enemy and the player
                if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
                {
                   // IsPlayerInRange = true;             //  The player has been seeing by the enemy and then the nemy starts to chasing the player
                   // IsPatrol = false;                 //  Change the state to chasing the player
                }
            }
        }





    }

    // step 6
    private void Patrolling ()
    {

    }



}

// https://youtu.be/ieyHlYp5SLQ, look at this for help
