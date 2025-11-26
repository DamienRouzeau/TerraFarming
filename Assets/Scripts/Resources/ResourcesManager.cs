using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager Instance { get; set; }
    public static ResourcesManager instance => Instance;

    private int woodQTT;
    private int stoneQTT;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void AddResource(ResourcesType _resource, int _qtt)
    {
        switch (_resource)
        {
            case ResourcesType.Wood:
                woodQTT += _qtt;
                break;
            case ResourcesType.Stone:
                stoneQTT += _qtt;
                break;
            default:
                Debug.Log("Resource uncknown : " + _resource);
                break;
        }
    }
}

public enum ResourcesType
{
    Wood,
    Stone
};
