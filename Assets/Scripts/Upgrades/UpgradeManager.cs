using UnityEngine;
using System.Collections.Generic;


public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager Instance { get; set; }
    public static UpgradeManager instance => Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    public void OnUpgrade(UpgradeButton btn)
    {
        if (btn.upgrade.level == btn.upgrade.levels.Length - 1) return;

        if(ResourcesManager.instance.GetQuantity(btn.upgrade.levels[btn.upgrade.level + 1].resource) >= btn.upgrade.levels[btn.upgrade.level+1].cost)
        { // Player have enought resource
            ResourcesManager.instance.RemoveResource(btn.upgrade.levels[btn.upgrade.level + 1].resource, btn.upgrade.levels[btn.upgrade.level + 1].cost);
            btn.upgrade.level++;
            UpdateUpgradeUI(btn);
        }
        else
        {
            // Not enought resource
        }
    }

    public void UpdateUpgradeUI(UpgradeButton btn)
    {
        if(btn.upgrade.level == btn.upgrade.levels.Length -1)
        {
            btn.priceTXT.text = "Max level";
            return;
        }
        btn.priceTXT.text = btn.upgrade.levels[btn.upgrade.level + 1].cost.ToString();
        btn.description.text = btn.upgrade.levels[btn.upgrade.level + 1].description.ToString();
    }
}
