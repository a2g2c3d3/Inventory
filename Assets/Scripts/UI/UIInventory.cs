using UnityEngine;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UISlot uiSlotPrefab;
    [SerializeField] private Transform slotParent;
    private List<UISlot> uiSlots = new List<UISlot>();

    public void InitInventoryUI(int slotCount) //slotCount = 100
    {
        if (uiSlots.Count == 0) //슬롯이없으면 만들어라
        {
            for (int i = 0; i < slotCount; i++)
            {
                UISlot newSlot = Instantiate(uiSlotPrefab, slotParent);
                uiSlots.Add(newSlot);
                uiSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateInventoryUI(List<InventoryItem> inventory)
    {
        for (int i = 0; i < uiSlots.Count; i++)
        {
            if (i < inventory.Count)
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].SetItem(inventory[i]);
            }
            else
            {
                uiSlots[i].gameObject.SetActive(false);
            }
        }
    }
}