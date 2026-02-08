using System;
using TMPro;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform rayOrigin;
    public RectTransform interactablePrompt;
    public TextMeshProUGUI interactableText;

    private PlayerInput _playerInput;
    private Interactable currentInteractable;
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Interact"].performed += ctx => Interact();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                interactablePrompt.gameObject.SetActive(true);
                interactableText.text = interactable.interactableAction;
            }
            else
            {
                currentInteractable = null;
                interactablePrompt.gameObject.SetActive(false);

            }
        } else {
            currentInteractable = null;
            interactablePrompt.gameObject.SetActive(false);
        }
    }

    void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

}
