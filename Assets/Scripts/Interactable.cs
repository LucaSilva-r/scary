using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool oneshot = true;
    public UnityEvent onInteract;
    public string interactableAction = "Interact";
    private bool hasBeenInteractedWith = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact()
    {
        if (oneshot && hasBeenInteractedWith)
        {
            return;
        }
        hasBeenInteractedWith = true;
        onInteract?.Invoke();
        Debug.Log("Interacted with " + gameObject.name);

    }
}
