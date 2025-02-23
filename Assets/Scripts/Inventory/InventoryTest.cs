using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Item potionRouge;
    public Item epeeFer;
    public Item pierreMagique;

    void Start()
    {
        // Ajouter 5 Potions Rouges (stackables)
        Inventory.instance.AddItem(potionRouge, 5);

        // Ajouter une Épée en Fer (non stackable)
        Inventory.instance.AddItem(epeeFer, 1);

        // Ajouter 3 Pierres Magiques (stackables)
        Inventory.instance.AddItem(pierreMagique, 3);

        // Mise à jour de l'affichage
        FindObjectOfType<InventoryUI>().UpdateUI();
    }
}
