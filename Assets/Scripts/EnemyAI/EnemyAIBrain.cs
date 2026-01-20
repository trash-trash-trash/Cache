using System.Collections.Generic;
using UnityEngine;

public enum EnemyAIStates
{
    Idle,
    WalkToPlayer
}

public class EnemyAIBrain : MonoBehaviour
{
    public IsCameraLookingAtMe isCameraLookingAtMe;
    public GameObjectStateManager stateManager;

    public EnemyAIStates currentState;

    public GameObject idleObj;
    public GameObject walkToPlayerObj;

    public Dictionary<EnemyAIStates, GameObject> statesDict;

    //hack
    public Transform playerTransform;

    private bool initialized = false;

    void OnEnable()
    {
        if (!initialized)
        {
            statesDict = new Dictionary<EnemyAIStates, GameObject>()
            {
                { EnemyAIStates.Idle, idleObj },
                { EnemyAIStates.WalkToPlayer, walkToPlayerObj }
            };
        }

        isCameraLookingAtMe.AnnounceInView += FlipWalkTo;
        ChangeState(EnemyAIStates.Idle);
    }

    private void FlipWalkTo(bool inView)
    {
        if (inView)
        {
            ChangeState(EnemyAIStates.Idle);
            
            GetComponent<AudioSource>().Stop();
        }
        else
        {
            ChangeState(EnemyAIStates.WalkToPlayer);
            
            GetComponent<AudioSource>().Play();
        }
    }

    public void ChangeState(EnemyAIStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject value))
        {
            stateManager.ChangeState(value);
            currentState = newState;
        }
    }
}