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
    private Camera mainCamera;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Interact"].performed += ctx => Interact();

        mainCamera = Camera.main;

        interactablePrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        // Se il dialogo è attivo, nascondi il prompt e blocca le interazioni
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsActive)
        {
            ClearInteraction();
            return;
        }

        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f, ~LayerMask.GetMask("Ignore Raycast", "Player")))
        {
            Debug.Log(hit.collider.gameObject.name);
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                interactablePrompt.gameObject.SetActive(true);
                interactableText.text = interactable.interactableAction;

                Vector3 worldCenter = hit.collider.bounds.center;
                Vector3 screenPos = mainCamera.WorldToScreenPoint(worldCenter);
                interactablePrompt.position = screenPos;
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

    private void ClearInteraction()
    {
        currentInteractable = null;
        interactablePrompt.gameObject.SetActive(false);
    }

    void Interact()
    {
        // Blocca interazione se il dialogo è attivo
        if (DialogueManager.Instance != null && DialogueManager.Instance.IsActive)
            return;

        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }
}
