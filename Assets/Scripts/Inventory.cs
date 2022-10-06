using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    #region singleton
    public static Inventory Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion singleton


}


/*public class Inventory : MonoBehaviour
{
    [SerializeField] Item[] items;
    [SerializeField] GameObject inventoryPanel;

    List<Item> itemsList = new List<Item>();
    int itemIndex = 0;
    private bool inventoryIsOpen = false;
    public bool InventoryIsOpen => inventoryIsOpen;

    void Start()
    {
        itemsList.Add(null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryIsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    private void OpenInventory()
    {
        ChangeCursorState(false);
        inventoryPanel.SetActive(true);
        inventoryIsOpen = true;
    }
    private void CloseInventory()
    {
        ChangeCursorState(true);
        inventoryPanel.SetActive(false);
        inventoryIsOpen = false;
    }

*//*    public bool AddItem(Item.Type type)
    {
        if (HasItem(type)) return false;

        var item = items.FirstOrDefault(item => item.type == type);
        itemsList.Add(item);

        return true;
    }

    public bool HasItem(Item.Type type)
    {
        return itemsList.Any(item => item?.type == type);
    }*//*

    private void ChangeCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}*/