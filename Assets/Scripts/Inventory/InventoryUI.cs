using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    // Références aux objets UI
    public GameObject slotPrefab;
    public Transform slotParent;
    public TextMeshProUGUI argentText;
    public TextMeshProUGUI nomItemText;
    public TextMeshProUGUI descriptionItemText;
    public TextMeshProUGUI prixItemText;

    // Surbrillance
    public GameObject surbrillancePrefab;
    private GameObject slotSelectionne;

    // Liste des instances de slots
    private List<GameObject> slotInstances = new List<GameObject>();

    // Etat de l'inventaire (ouvert/fermé)
    private bool inventaireOuvert = false;
    private CanvasGroup canvasGroup;


    void Start()
    {
        Debug.Log("InventoryUI - Start() lancé");

        // Initialisation du CanvasGroup
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("InventoryUI - CanvasGroup manquant sur le UI_Inventaire !");
            return;
        }

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        // Vérifie si Inventory.instance est null
        if (Inventory.instance == null)
        {
            Debug.LogError("InventoryUI - Inventory.instance est null !");
            return;
        }

        // Génération des slots
        for (int i = 0; i < Inventory.instance.maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            slotInstances.Add(slot);

            // Ajout dynamique du EventTrigger
            EventTrigger trigger = slot.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;

            int index = i; // On stocke l'index pour l'utiliser dans le lambda

            // Ajout du Listener pour appeler OnClickSlot avec le bon index
            entry.callback.AddListener((data) => { OnClickSlot(index); });
            trigger.triggers.Add(entry);
        }

        Debug.Log("InventoryUI - Tous les slots générés");
        UpdateUI();
    }

    void Update()
    {
        // Ouverture/Fermeture de l'inventaire avec TAB
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventaireOuvert = !inventaireOuvert;

            if (inventaireOuvert)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;

                // Sélection automatique du premier slot
                SelectionnerSlot(0);
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }

            // Bloque le mouvement du joueur quand l'inventaire est ouvert
            PlayerMovement.canMove = !inventaireOuvert;
        }
    }

    public void UpdateUI()
    {
        Debug.Log("InventoryUI - UpdateUI() lancé");

        // Vérifie si Inventory.instance est null
        if (Inventory.instance == null)
        {
            Debug.LogError("InventoryUI - Inventory.instance est null dans UpdateUI()");
            return;
        }

        // Vérifie si les slots sont initialisés
        if (Inventory.instance.slots == null)
        {
            Debug.LogError("InventoryUI - Inventory.instance.slots est null");
            return;
        }

        // Affichage de l'argent du joueur
        argentText.text = "Or : " + Inventory.instance.playerGold.ToString();

        // Mise à jour des slots
        for (int i = 0; i < slotInstances.Count; i++)
        {
            if (i < Inventory.instance.slots.Count)
            {
                InventorySlot slotData = Inventory.instance.slots[i];
                Image iconImage = slotInstances[i].transform.GetChild(0).GetComponent<Image>();
                TextMeshProUGUI quantityText = slotInstances[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                iconImage.sprite = slotData.item.icon;
                iconImage.enabled = true;

                if (slotData.item.maxStack > 1 && slotData.quantity > 1)
                {
                    quantityText.text = slotData.quantity.ToString();
                    quantityText.enabled = true;
                }
                else
                {
                    quantityText.text = "";
                    quantityText.enabled = false;
                }
            }
            else
            {
                Image iconImage = slotInstances[i].transform.GetChild(0).GetComponent<Image>();
                TextMeshProUGUI quantityText = slotInstances[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>();

                iconImage.sprite = null;
                iconImage.enabled = false;
                quantityText.text = "";
                quantityText.enabled = false;
            }
        }
    }

    public void OnClickSlot(int index)
    {
        // Sélectionne le slot et affiche les infos
        SelectionnerSlot(index);
    }

    public void SelectionnerSlot(int index)
    {
        // Détruit l'ancienne surbrillance
        if (slotSelectionne != null)
        {
            Destroy(slotSelectionne);
        }

        // Vérifie que l'index est valide
        if (index < slotInstances.Count)
        {
            GameObject slot = slotInstances[index];

            // Instancie la surbrillance comme enfant du slot
            slotSelectionne = Instantiate(surbrillancePrefab, slot.transform);
            
            // Réinitialisation du RectTransform
            RectTransform rt = slotSelectionne.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.anchoredPosition = Vector2.zero;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            // Affiche les informations de l'objet sélectionné
            if (index < Inventory.instance.slots.Count)
            {
                InventorySlot slotData = Inventory.instance.slots[index];
                nomItemText.text = slotData.item.itemName;
                descriptionItemText.text = slotData.item.description;
                prixItemText.text = "Prix : " + slotData.item.prixVente.ToString() + " Or";
            }
            else
            {
                nomItemText.text = "";
                descriptionItemText.text = "";
                prixItemText.text = "";
            }
        }
    }
}
