using UnityEngine;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private UISlot uiSlotPrefab;
    [SerializeField] private Transform slotParent;
    private List<UISlot> uiSlots = new List<UISlot>();

    public void InitInventoryUI(int slotCount) //slotCount = 100
    {
        // 효율적인 오브젝트 풀링을 위해 한 번만 슬롯을 생성
        if (uiSlots.Count == 0)
        {
            for (int i = 0; i < slotCount; i++)
            {
                UISlot newSlot = Instantiate(uiSlotPrefab, slotParent);
                uiSlots.Add(newSlot);
            }
        }
    }

    public void UpdateInventoryUI(List<ItemData> inventory)
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