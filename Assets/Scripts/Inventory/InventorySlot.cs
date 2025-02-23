using UnityEngine;

namespace RPG.InventorySystem // Namespace pour organiser proprement le code
{
    [System.Serializable]
    public class InventorySlot
    {
        public Item item;      // L'objet stock� dans ce slot
        public int quantity;   // Quantit� de l'objet (pour les objets empilables)

        // Constructeur avec param�tres (objet + quantit�)
        public InventorySlot(Item item, int quantity)
        {
            this.item = item;
            this.quantity = quantity;
        }

        // Constructeur sans param�tres (slot vide)
        public InventorySlot()
        {
            this.item = null;
            this.quantity = 0;
        }

        // V�rifie si le slot est vide
        public bool IsEmpty()
        {
            return item == null || quantity <= 0;
        }

        // Ajoute une quantit� � l'objet d�j� dans le slot
        public void AddQuantity(int amount)
        {
            if (item != null)
            {
                quantity += amount;
            }
        }

        // Retire une quantit� de l'objet dans le slot
        public void RemoveQuantity(int amount)
        {
            quantity -= amount;
            if (quantity <= 0)
            {
                ClearSlot();
            }
        }

        // Vide compl�tement le slot
        public void ClearSlot()
        {
            item = null;
            quantity = 0;
        }
    }
}
