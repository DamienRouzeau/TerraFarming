using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HumanBehaviour : MonoBehaviour
{
    private int commandIndex = 0;
    private bool isInProssess = false;
    private bool isWalking = false;
    private Transform destination;
    private Commands currentTask;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        StartWork(CommandDispatcher.instance.GetNewCommand(this));
    }

    private void Update()
    {
        if (isWalking)
        {
            // Wait for path
            if (agent.pathPending)
                return;

            // If the agent is on destination and don't move
            if (agent.remainingDistance <= agent.stoppingDistance &&
                (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
            {
                isWalking = false;
                destination = null;

                StartWork(CommandDispatcher.instance.GetNewCommand(this));
            }
        }
        else
        {
            destination = null;
        }

        if (!isInProssess && !isWalking)
        {
            StartWork(CommandDispatcher.instance.GetNewCommand(this));
        }
    }

    private void StartWork(Commands _command)
    {
        currentTask = _command;
        switch (_command)
        {
            case Commands.GoTo:
                destination = CommandDispatcher.instance.forest;
                agent.SetDestination(destination.position);
                isWalking = true;
                break;

            case Commands.Get:
                StartCoroutine(Work());
                break;

            case Commands.GoHome:
                destination = CommandDispatcher.instance.home;
                agent.SetDestination(destination.position);
                isWalking = true;
                break;

            case Commands.WaitingForInstruction:
                isWalking = false;
                isInProssess = false;
                destination = CommandDispatcher.instance.home;
                agent.SetDestination(destination.position);
                ResetIndex();
                return;

            default:
                Debug.Log("Command not found");
                break;
        }
        commandIndex++;
    }

    private IEnumerator Work()
    {
        isWalking = false;
        isInProssess = true;
        yield return new WaitForSeconds(2);
        isInProssess = false;
        StartWork(CommandDispatcher.instance.GetNewCommand(this));
    }

    public void ResetIndex()
    {
        commandIndex = 0;
    }

    public int GetIndex() { return commandIndex; }
}
