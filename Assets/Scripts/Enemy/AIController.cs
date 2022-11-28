using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent NavAgent; // nav mesh agent component access

    public Transform player; //to identify the player avatar within the scene

    public LayerMask whatIsGround; // to identify the ground, playable area
    public LayerMask whatIsPlayer; // to identify the player avatar

    public float health; // enemy health variable

    //Patrolling Waypoints
    public Vector3 PatrolPoint; // patroling positions, using Vector3 Coords
    bool PatrolPointSet; //a bool variable to determine whether the NPC is still patroling at the waypoint
    public float PatrolPointRange; // how far from waypoint position

    //Attacking
    public float attackdelay; // wait delay time of every attack action
    bool alreadyAttacked; // bool variable for if the player attacks

    //State determination
    public float viewRadius; //variable storing how far the enemy can detect the player
    public float attackRange;// variable storing how close the player needs to be in order for the enemy to perform attack action
    public bool playerInSightRange; // is the player avatar within view range?
    public bool playerInAttackRange;// is the player avatar within attack range?

    //extra - identifying navmesh angular speed variable for quaterion rotation speed
    private float speed = GameObject.Find("Enemy").GetComponent<NavMeshAgent>().angularSpeed;

    private void Awake()
    {
        player = GameObject.Find("Player").transform; // identifying the player avatar
        NavAgent = GetComponent<NavMeshAgent>(); // using nav mesh agent component
    }

    private void Update() // How states are determined, using bool variables, updated all the time
    {
        //Checking if the player avatar is seen by the NPC, and checking to see if the player avatar is close enough to attack
        playerInSightRange = Physics.CheckSphere(transform.position, viewRadius, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        // for both bool variables, an invisible sphere (radius determined by view radius or attack range), is used to detect the player

        if (!playerInSightRange && !playerInAttackRange) Patrolling(); // if the player avatar isn't in sight range or attack range,
                                                                      // then continue patrolling - Patrol State

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();// if the player is in sight range but not in attack range,
                                                                      // then chase the player avatar - Chase State

        if (playerInAttackRange && playerInSightRange) AttackPlayer();// if the player is in sight range and in attack range,
                                                                      // then attack the player - Attack State
    }

    private void Patrolling()// Patrol state function
    {
        if (!PatrolPointSet) GenerateNextPatrolPoint(); // if patrol waypoint is not set, then find the next waypoint position

        if (PatrolPointSet)                       // if patrol waypoint is set, then set NavMesh component to waypoint position.
            NavAgent.SetDestination(PatrolPoint);//Enemy will go towards set waypoint.

        Vector3 distanceToWalkPoint = transform.position - PatrolPoint; //Stores the distance away from current waypoint.
        // Current position - waypoint position = distance.
        if (distanceToWalkPoint.magnitude < 1f) // Waypoint is reached, once the distance is lower than 1
            PatrolPointSet = false;             // once NPC reaches waypoint co-ordinates, PatrolPointSet is set to false, finds new waypoint
    }
    private void GenerateNextPatrolPoint()// Determining Patrol Way Points function
    {
        float randomX = Random.Range(-PatrolPointRange, PatrolPointRange); //Randomly calculated coordinates for x axis, using the PatrolPointRange

        PatrolPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z);
        // New coordinates being generated, randomX is added to current enemy position to create new coordinates

        if (Physics.Raycast(PatrolPoint, -transform.up, 2f, whatIsGround))// detects if there is no surround collisions and if the enemy is on the ground
            PatrolPointSet = true;// next waypoint is set, once coordinates are found
    }

    private void ChasePlayer() // Chase state Function
    {
        NavAgent.SetDestination(player.position);// once in chase state, Enemy NPC moves towards player avatar position,
                                                 // only when in view radius
    }

    private void AttackPlayer() // Attack state Function
    {
        NavAgent.SetDestination(transform.position);//Enemy doesn't move once in range

        transform.LookAt(player);// turns to look at player avatar

        var step = speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(player.rotation, player.rotation, step);
        // improved code, changed so it rotates enemy rather than flips it when player jumps over enemy

        if (!alreadyAttacked)
        {
            ///Attack code here
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackdelay);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)//not finished, for attacking mechanic
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()// the enemy would despawn when "defeated"
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() // visualising spheres with colours
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
