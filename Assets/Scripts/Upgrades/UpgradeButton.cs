using UnityEngine;
using TMPro;


public class UpgradeButton : MonoBehaviour
{
    public Upgrade upgrade;
    public TextMeshProUGUI description;
    public TextMeshProUGUI priceTXT;

    private void Start()
    {
        UpgradeManager.instance.UpdateUpgradeUI(this);
    }
}
