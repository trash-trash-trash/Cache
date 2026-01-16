using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIWalkToPlayerState : EnemyAIStateBase
{
    //TODO: make this smarter
    public Transform playerTransform;
    
    //this script has a lot of power... refactor

    private bool huntingPlayer = false;

    public float currentDist;
    
    public float minUpdateDistance = 0.5f;
    
    public float minKillDist = 7f;
    
    public override void OnEnable()
    {
        base.OnEnable();
        obs.enabled = false;
        rb.useGravity = false;
        playerTransform = brain.playerTransform;
        StartCoroutine(WaitUntilStopped());
    }

    IEnumerator WaitUntilStopped()
    {
        yield return new WaitForFixedUpdate();

        while (rb.linearVelocity.sqrMagnitude > 0.1f &&
               rb.angularVelocity.sqrMagnitude > 0.1f)
        {
            yield return new WaitForFixedUpdate();
            yield return null;
        }       

        agent.enabled = true;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }

        agent.SetDestination(playerTransform.position);

        //turns agent rotation off, snaps look at the player, turns it back on
        agent.updateRotation = false;
        Vector3 lookDir = playerTransform.position - transform.position;
        lookDir.y = 0f;
        if (lookDir.sqrMagnitude > 0.0001f)
        {
            agent.transform.rotation = Quaternion.LookRotation(lookDir.normalized);
        }
        agent.updateRotation = true;

        agent.isStopped = false;
        huntingPlayer = true;
    }

    
    void FixedUpdate()
    {
        if (!agent.hasPath)
            return;
       
        if (!huntingPlayer || !agent.enabled) return;

        if (Vector3.Distance(agent.destination, playerTransform.position) > minUpdateDistance)
        {
            agent.SetDestination(playerTransform.position);
        }

        currentDist = Vector3.Distance(agent.transform.position, playerTransform.position);
        if (currentDist < minKillDist)
        {
            Health HP = playerTransform.GetComponentInParent<Health>();
            if (AAAGameManager.Instance != null)
            {
                if (AAAGameManager.Instance.currentDifficulty == Difficulty.Normal)
                    HP.ChangeHealth(-1);

                else
                    HP.Kill();
            }
        }
    }


    void OnDisable()
    {
        StopAllCoroutines();

        if (agent.enabled)
        {
            agent.isStopped = true;
            agent.velocity = Vector3.zero;
            agent.enabled = false;
        }
    }
}
