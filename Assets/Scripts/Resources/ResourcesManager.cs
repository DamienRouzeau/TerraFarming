using UnityEngine;
using TMPro;

public class ResourcesManager : MonoBehaviour
{
    private static ResourcesManager Instance { get; set; }
    public static ResourcesManager instance => Instance;

    private static readonly string[] suffixes = { "", "k", "M", "B", "T" };


    [Header("Resources Quantity")]
    public ResourceQTT[] resourceQTT;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        foreach(ResourceQTT res in resourceQTT)
        {
            res.txt.text = FormatNumber(res.qtt);
        }
    }

    public void AddResource(ResourcesType _resource, int _qtt)
    {

        int i = resourceQTT.Length - 1;
        while(i>=0)
        {
            if(resourceQTT[i].resource == _resource)
            {
                resourceQTT[i].qtt += _qtt;
                resourceQTT[i].txt.text = FormatNumber(resourceQTT[i].qtt);
                i = -1;
            }
            i--;
        }
    }

    public void RemoveResource(ResourcesType _resource, int _qtt)
    {
        AddResource(_resource, -_qtt);
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

    public int GetQuantity(ResourcesType _resource)
    {
        int i = resourceQTT.Length - 1;
        while (i >= 0)
        {
            if (resourceQTT[i].resource == _resource)
            {
                return resourceQTT[i].qtt;
            }
            i--;
        }
        Debug.Log("Resource not found, return 0 quantity");
        return 0;
    }


}

[System.Serializable]
public class ResourceQTT
{
    public int qtt;
    public ResourcesType resource;
    public TextMeshProUGUI txt;
}

public enum ResourcesType
{
    Wood,
    Stone
};
