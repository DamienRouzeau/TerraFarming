using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class CommandDispatcher : MonoBehaviour
{
    private static CommandDispatcher Instance { get; set; }
    public static CommandDispatcher instance => Instance;
    [SerializeField] private GameObject commandContainer;
    private List<Commands> commandsList = new List<Commands>();

    [Header("InterestsPoints")]
    public Transform forest;
    public Transform home;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public Commands GetNewCommand(HumanBehaviour human)
    {
        if(commandsList.Count == 0)
        {
            human.ResetIndex();
            return Commands.WaitingForInstruction;
        }
        if (human.GetIndex() > commandsList.Count -1)
            human.ResetIndex();
        return commandsList[human.GetIndex()];
    }

    public void GetAllCommands()
    {
        StartCoroutine(WaitToGetAllCommands());
    }

    private IEnumerator WaitToGetAllCommands()
    {
        yield return new WaitForSeconds(0.1f);
        commandsList.Clear();
        CommandBlockUI[] _commands = commandContainer.GetComponentsInChildren<CommandBlockUI>();
        foreach (CommandBlockUI _command in _commands)
        {
            commandsList.Add(_command.commandName);
        }
    }
}

public enum Commands
{
    GoTo,
    Get,
    GoHome,
    WaitingForInstruction
};
