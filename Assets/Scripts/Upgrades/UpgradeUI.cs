using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject commandUI;
    [SerializeField] private GameObject upgradeButtonUI;
    [SerializeField] private GameObject upgradeMenuUI;
    
    public void OpenUpgrades()
    {
        commandUI.SetActive(false);
        upgradeButtonUI.SetActive(false);
        upgradeMenuUI.SetActive(true);
    }
    public void CloseUpgrades()
    {
        commandUI.SetActive(true);
        upgradeButtonUI.SetActive(true);
        upgradeMenuUI.SetActive(false);
    }
}
