using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class CommandDispatcher : MonoBehaviour
{
    private static CommandDispatcher Instance { get; set; }
    public static CommandDispatcher instance => Instance;

    [SerializeField] private GameObject commandContainer;
    private List<CommandBlockUI> commandsList = new List<CommandBlockUI>();
    private List<HumanBehaviour> humans = new List<HumanBehaviour>();

    [Header("InterestsPoints")]
    public Transform forest;
    public Transform home;
    public Transform cave;



    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public Commands GetNewCommand(HumanBehaviour human)
    {
        if(!humans.Contains(human))
        {
            humans.Add(human); // Si l'humain n'est pas encore enregistré, l'ajouter à la liste
        }

        if(commandsList.Count == 0)
        {
            human.ResetIndex();
            return Commands.WaitingForInstruction;
        }
        if (human.GetIndex() > commandsList.Count -1) // Fin de la boucle, retour à la première tache
            human.ResetIndex();

        foreach(CommandBlockUI _command in commandsList)
        {
            _command.SetActiveCurrentTask(false);
        }
        commandsList[human.GetIndex()].SetActiveCurrentTask(true);

        return commandsList[human.GetIndex()].commandName;
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
            commandsList.Add(_command);
        }
        foreach(HumanBehaviour _human in humans)
        {
            _human.ResetIndex();
        }
    }
}

public enum Commands
{
    GoToForest,
    Get,
    GoHome,
    WaitingForInstruction,
    GoToCave
};

public enum Location
{
    Forest,
    Home,
    Nowhere,
    Cave
};
