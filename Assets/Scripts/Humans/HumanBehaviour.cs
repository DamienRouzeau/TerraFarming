using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class HumanBehaviour : MonoBehaviour
{
    private int commandIndex = 0;
    private bool isInProssess = false;
    private bool isWalking = false;
    private Transform destination;
    private Commands currentTask; // Debug
    private Location currentLocation = Location.Nowhere;
    [SerializeField] private NavMeshAgent agent;

    [Header("Stats")]
    [SerializeField] private float choppingStrenght;
    [SerializeField] private float choppingTime;
    [SerializeField] private float miningStrenght;
    [SerializeField] private float miningTime;

    [Header("Inventory")]
    [SerializeField] private int wood;
    [SerializeField] private int stone;

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
            case Commands.GoToForest:
                destination = CommandDispatcher.instance.forest;
                agent.SetDestination(destination.position);
                isWalking = true;
                break;

            case Commands.GoToCave:
                destination = CommandDispatcher.instance.cave;
                agent.SetDestination(destination.position);
                isWalking = true;
                break;

            case Commands.Get:
                StartCoroutine(GetResource());
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

    private IEnumerator GetResource()
    {
        isWalking = false;
        isInProssess = true;
        yield return new WaitForSeconds(2);
        switch(currentLocation)
        {
            case Location.Forest:
                yield return new WaitForSeconds(choppingTime);
                wood += (int)choppingStrenght;
                break;

            case Location.Cave:
                yield return new WaitForSeconds(miningTime);
                stone += (int)miningStrenght;
                break;

            case Location.Home:
                break;

            default:
                Debug.Log("Location unknown");
                break;
        }
        isInProssess = false;
        StartWork(CommandDispatcher.instance.GetNewCommand(this));
    }

    public void ResetIndex()
    {
        commandIndex = 0;
    }

    private void ClearInventory()
    {
        if(wood > 0)
        {
            ResourcesManager.instance.AddResource(ResourcesType.Wood, wood);
            wood = 0;
        }
        if (stone > 0)
        {
            ResourcesManager.instance.AddResource(ResourcesType.Stone, stone);
            stone = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Forest":
                currentLocation = Location.Forest;
                break;

            case "Home":
                currentLocation = Location.Home;
                ClearInventory();
                break;

            case "Cave":
                currentLocation = Location.Cave;
                break;

            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Forest":
                currentLocation = Location.Nowhere;
                break;

            case "Home":
                currentLocation = Location.Nowhere;
                break;

            case "Cave":
                currentLocation = Location.Nowhere;
                break;

            default:
                break;
        }
    }

    public int GetIndex() { return commandIndex; }
}
