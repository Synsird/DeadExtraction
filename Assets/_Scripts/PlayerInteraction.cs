using System;
using UnityEditor;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float interactRange = 50f; // Default value set for visibility in editor

    [SerializeField] private float alertRange = 10f;

    // Small crosshair that alerts the player if an interactable is nearby their current look position
    public CanvasGroup alertPrefab;
    public CanvasGroup ContextMenuPrefab;

    private Camera mainCamera;

    private RaycastHit _hit;
    Vector3 checkPoint;

    // Layer that interactables are on
    public LayerMask iLayer;

    private void Awake()
    {
        // Ensure there's a main camera tagged in the scene
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("Main camera not found. Ensure your camera is tagged as 'Main Camera'.");
    }

    // Update is called once per frame
    void Update()
    {
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        Interactable hitObj = null;
        Vector3 forwardDir = mainCamera.transform.forward; // Simplified from TransformDirection(Vector3.forward)
        Vector3 rayOrigin = mainCamera.transform.position;
        Debug.DrawLine(rayOrigin, rayOrigin + forwardDir * interactRange, Color.red);

        bool rayHit = Physics.Raycast(rayOrigin, forwardDir, out RaycastHit hit, interactRange, iLayer);
        checkPoint = rayHit ? hit.point : rayOrigin + forwardDir * interactRange;

        if(rayHit)
        {
            hitObj = hit.transform.GetComponent<Interactable>();
            if(hitObj != null)
            {
                ContextMenuPrefab.alpha = 1;
            }
        }
        else
        {
            ContextMenuPrefab.alpha = 0;
        }

        if(hitObj != null)
        {
            alertPrefab.alpha = 0;
        }
        else
        {
            CheckForNearbyInteractables(checkPoint);
        }
    }

    private void CheckForNearbyInteractables(Vector3 _checkPoint)
    {
        Collider[] nearbyCollisions = Physics.OverlapSphere(_checkPoint, alertRange, iLayer);
        alertPrefab.alpha = nearbyCollisions.Length > 0 ? 1 : 0;
    }

    private void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(mainCamera.transform.position + mainCamera.transform.forward * (interactRange - alertRange), alertRange); // Draw alert range at potential maximum distance
        }
    }
}
