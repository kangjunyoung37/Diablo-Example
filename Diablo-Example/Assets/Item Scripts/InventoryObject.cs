using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using kang.InventorySystem.Items;
using System.Linq;
namespace kang.InventorySystem.Inventory
{


    public enum InterfaceType
    {
        Inventory,
        Equipment,
        QuickSlot,
        Box,
    }
    [CreateAssetMenu(fileName = "New Inventory",menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject
    {
        public ItemObjectDatabase database;
        public InterfaceType type;

        [SerializeField]
        private Inventory container = new Inventory();

        public InventorySlot[] Slots => container.slots;

        public int EmptySlotCount
        {
            get
            {
                int counter = 0;
                foreach(InventorySlot slot in Slots)
                {
                    if(slot.item.id < 0)//���Կ� �������� ���ٴ� ��
                    {
                        counter++;
                    }
                }
                return counter;
            }
        }

        public bool AddItem(Item item, int amount)
        {
            if (EmptySlotCount <= 0)
            {
                return false;
            }
            InventorySlot slot =  FindItemInInventory(item);
            if (!database.itemObjects[item.id].stackable || slot == null)
            {
                if(EmptySlotCount <=0)
                {
                    return false;
                }
                GetEmptySlot().UpdateSlot(item, amount);
            }
            else
            {
                slot.AddAmount(amount);
            }
            return true;

        }
        public InventorySlot FindItemInInventory(Item item)
        {
            return Slots.FirstOrDefault(i => i.item.id == item.id);
        }
        public InventorySlot GetEmptySlot()
        {
            return Slots.FirstOrDefault(i => i.item.id < 0);
        }
        public bool IsContainItem(ItemObject itemObject)
        {
            return Slots.FirstOrDefault(i => i.item.id == itemObject.data.id) != null;

        }

        public void SwapItems(InventorySlot itemSlotA, InventorySlot itemSlotB)//������ ���� A�� ������ ���� B�� �ٲ��ִ� �޼ҵ�
        {
            if(itemSlotA == itemSlotB)
            {
                return;
            }
            if(itemSlotB.CanPlaceInSlot(itemSlotA.ItemObject)&& itemSlotA.CanPlaceInSlot(itemSlotB.ItemObject))
            {
                InventorySlot tempSlot = new InventorySlot(itemSlotB.item, itemSlotB.amount);
                itemSlotB.UpdateSlot(itemSlotA.item,itemSlotA.amount);
                itemSlotA.UpdateSlot(tempSlot.item,tempSlot.amount);

            }
        }
    }

   

}