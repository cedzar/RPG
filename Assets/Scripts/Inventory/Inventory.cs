using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 26;
    public int playerGold = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(Item item, int quantity)
    {
        // Si l'objet est stackable, empile dans un slot existant
        if (item.maxStack > 1)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == item && slot.quantity < item.maxStack)
                {
                    int totalQuantity = slot.quantity + quantity;
                    if (totalQuantity <= item.maxStack)
                    {
                        slot.quantity = totalQuantity;
                        return true;
                    }
                    else
                    {
                        slot.quantity = item.maxStack;
                        quantity = totalQuantity - item.maxStack;
                    }
                }
            }
        }

        // Si l'objet n'est pas stackable ou qu'aucun slot n'est disponible
        if (slots.Count < maxSlots)
        {
            InventorySlot newSlot = new InventorySlot(item, quantity);
            slots.Add(newSlot);
            return true;
        }

        Debug.Log("Inventaire plein !");
        return false;
    }

    public void RemoveItem(Item item, int quantity)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == item)
            {
                slot.quantity -= quantity;
                if (slot.quantity <= 0)
                {
                    slots.Remove(slot);
                }
                return;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;

    public InventorySlot(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}
