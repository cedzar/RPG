using UnityEngine;

public class OmbreAnimation : MonoBehaviour
{
    public Transform joueur;
    public Vector2 offset = new Vector2(0, -0.1f);

    // Animation du Scale
    public float scaleVariation = 0.05f;
    public float scaleSpeed = 2f;

    private Vector3 baseScale;

    void Start()
    {
        // On enregistre la taille de base de l'ombre
        baseScale = transform.localScale;
    }

    void Update()
    {
        // L'ombre suit toujours le joueur avec un léger décalage en Y
        transform.position = (Vector2)joueur.position + offset;

        // Animation du Scale pour un effet de compression
        float scaleFactor = 1 + Mathf.Sin(Time.time * scaleSpeed) * scaleVariation;
        transform.localScale = new Vector3(baseScale.x * scaleFactor, baseScale.y * (2 - scaleFactor), baseScale.z);
    }
}
