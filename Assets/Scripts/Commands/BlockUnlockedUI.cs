using UnityEngine;
using UnityEngine.UI;

public class BlockUnlockedUI : MonoBehaviour
{
    public Button button;
    public GameObject commandPrefab;
    public Transform commandListContent;

    void Start()
    {
        button.onClick.AddListener(AddToCommandList);
    }

    void AddToCommandList()
    {
        Instantiate(commandPrefab, commandListContent);
    }
}
