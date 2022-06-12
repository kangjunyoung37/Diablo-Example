using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kang.InventorySystem.Inventory;
using kang.InventorySystem.Items;
public class TextItems : MonoBehaviour
{
    public InventoryObject inventoryObject;
    public ItemObjectDatabase Database;
    public void AddNewItem()
   {
        if(Database.itemObjects.Length > 0)
        {
            ItemObject newItemObject = Database.itemObjects[2];
            Item newItem = new Item(newItemObject);

            inventoryObject.AddItem(newItem, 1);
        }
    }

}
