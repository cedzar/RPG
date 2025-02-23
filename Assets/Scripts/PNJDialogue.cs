using UnityEngine;
using TMPro;

public class PNJDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public string[] dialogueLines;

    private bool isPlayerInRange = false;
    private int currentLine = 0;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueBox.activeInHierarchy)
            {
                NextDialogueLine();
            }
            else
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        PlayerMovement.canMove = false; // ✅ Désactive le mouvement du joueur
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];
    }

    void NextDialogueLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        currentLine = 0;
        PlayerMovement.canMove = true; // ✅ Réactive le mouvement du joueur
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            EndDialogue(); // ✅ Ferme le dialogue si le joueur s’éloigne
        }
    }
}
