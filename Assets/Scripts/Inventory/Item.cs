using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;
    public int maxStack = 1;
    public int prixVente;
    public int price;
    public ItemType itemType;

    public enum ItemType
    {
        Consommable,
        Equipement,
        Ressource,
        Quête
    }
}
