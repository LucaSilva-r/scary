using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform rayOrigin;
    public RectTransform interactablePrompt;
    public TextMeshProUGUI interactableText;

    private PlayerInput _playerInput;
    private Interactable currentInteractable;
    private Camera mainCamera; // 1. Variable to store the camera

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Interact"].performed += ctx => Interact();

        // 2. Cache the main camera so we don't look for it every frame
        mainCamera = Camera.main; 
        
        // Ensure the prompt starts hidden
        interactablePrompt.gameObject.SetActive(false); 
    }

    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        
        // I kept your 1f distance, but you might want to increase this slightly (e.g., 2f or 3f)
        if (Physics.Raycast(ray, out hit, 2f, ~LayerMask.GetMask("Ignore Raycast", "Player")))
        {
            Debug.Log(hit.collider.gameObject.name);
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactablePrompt.gameObject.SetActive(true);
                interactableText.text = interactable.interactableAction;

                // --- NEW CODE START ---
                
                // 3. Get the center of the object's physical collider
                Vector3 worldCenter = hit.collider.bounds.center;

                // 4. Convert that 3D world point to a 2D screen point
                Vector3 screenPos = mainCamera.WorldToScreenPoint(worldCenter);

                // 5. Apply the position to the UI element
                interactablePrompt.position = screenPos;
                
                // --- NEW CODE END ---
            }
            else
            {
                ClearInteraction();
            }
        } 
        else 
        {
            ClearInteraction();
        }
    }

    // Helper method to avoid repeating code
    private void ClearInteraction()
    {
        currentInteractable = null;
        interactablePrompt.gameObject.SetActive(false);
    }

    void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}