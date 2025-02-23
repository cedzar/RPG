using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Référence du joueur
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Décalage de la caméra
    public bool useLimits = true;

    // ✅ On utilise un Box Collider 2D pour définir les limites
    public BoxCollider2D mapBounds;

    private Vector2 minLimit;
    private Vector2 maxLimit;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        if (mapBounds != null)
        {
            // ✅ Calcul automatique de la taille de la caméra
            halfHeight = Camera.main.orthographicSize;
            halfWidth = halfHeight * Camera.main.aspect;

            // ✅ Calcul automatique des limites
            minLimit = mapBounds.bounds.min + new Vector3(halfWidth, halfHeight);
            maxLimit = mapBounds.bounds.max - new Vector3(halfWidth, halfHeight);
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Calcul de la position désirée
        Vector3 desiredPosition = player.position + offset;

        // ✅ Applique les limites calculées automatiquement
        if (useLimits)
        {
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minLimit.x, maxLimit.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minLimit.y, maxLimit.y);
        }

        // Mouvement fluide de la caméra
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
