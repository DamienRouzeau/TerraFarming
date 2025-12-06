using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "Upgrade", menuName = "Game/Upgrades/Upgrade")]
public class Upgrade : ScriptableObject
{
    public string name;
    public Levels[] levels;
    public int level = 0;
}

[System.Serializable]
public class Levels
{
    public int cost;
    public ResourcesType resource;
    public string description;
}