using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager Instance { get; set; }
    public static ResourcesManager instance => Instance;

    private static readonly string[] suffixes = { "", "k", "M", "B", "T" };


    [Header("Resources Quantity")]
    private int woodQTT;
    private int stoneQTT;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI woodTXT;
    [SerializeField] private TextMeshProUGUI stoneTXT;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        woodTXT.text = FormatNumber(woodQTT);
        stoneTXT.text = FormatNumber(stoneQTT);
    }

    public void AddResource(ResourcesType _resource, int _qtt)
    {
        switch (_resource)
        {
            case ResourcesType.Wood:
                woodQTT += _qtt;
                woodTXT.text = FormatNumber(woodQTT);
                break;
            case ResourcesType.Stone:
                stoneQTT += _qtt;
                stoneTXT.text = FormatNumber(stoneQTT);
                break;
            default:
                Debug.Log("Resource uncknown : " + _resource);
                break;
        }
    }


    public static string FormatNumber(double value)
    {
        int i = 0;
        while (value >= 1000 && i < suffixes.Length - 1)
        {
            value /= 1000;
            i++;
        }

        return value.ToString(value >= 100 ? "0" : "0.##") + suffixes[i];
    }




}

public enum ResourcesType
{
    Wood,
    Stone
};
