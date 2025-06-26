using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> _inventoryItemsData;
    [SerializeField] private List<InventoryItem> _inventoryItems;
    [SerializeField] private List<SelectedInventoryItem> _selectedItems;

    public IReadOnlyList<SelectedInventoryItem> SelectedItems => _selectedItems;

    private void Awake()
    {
        for (int i = 0; i < _inventoryItemsData.Count; i++)
        {
            if (i < _inventoryItems.Count)
            {
                _inventoryItems[i].Initialize(_inventoryItemsData[i], this);
            }
        }
    }

    public void SelectItems(InventoryItemData itemData)
    {
        foreach (var item in _selectedItems)
        {
            if (!item.IsFillen)
            {
                item.Initialize(itemData);
                break;
            }
        }
    }
}

[Serializable]
public class InventoryItemData
{
    public string Name;
    public int Amount;
    public Sprite Icon;
}
