using UnityEngine;

namespace RPG.InventorySystem // Namespace pour organiser proprement le code
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;      // L'objet stocké dans ce slot
        public int quantity;   // Quantité de l'objet (pour les objets empilables)

        // Constructeur avec paramètres (objet + quantité)
        public InventorySlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        // Constructeur sans paramètres (slot vide)
        public InventorySlot()
        {
            this.item = null;
            this.quantity = 0;
        }

        // Vérifie si le slot est vide
        public bool IsEmpty()
        {
            return item == null || quantity <= 0;
        }

        // Ajoute une quantité à l'objet déjà dans le slot
        public void AddQuantity(int amount)
        {
            if (item != null)
            {
                quantity += amount;
            }
        }

        // Retire une quantité de l'objet dans le slot
        public void RemoveQuantity(int amount)
        {
            quantity -= amount;
            if (quantity <= 0)
            {
                ClearSlot();
            }
        }

        // Vide complètement le slot
        public void ClearSlot()
        {
            item = null;
            quantity = 0;
        }
    }
}
