using UnityEngine;

public class InventoryTabSystem : MonoBehaviour
{
    public GameObject MinionPanel, PlayerPanel;

    public void EnableMinionPanel()
    {
        MinionPanel.SetActive(true);
        PlayerPanel.SetActive(false);
    }

    public void EnablePlayerPanel()
    {
        MinionPanel.SetActive(false);
        PlayerPanel.SetActive(true);
    }
}
