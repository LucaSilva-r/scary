using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool oneshot = true;
    public UnityEvent onInteract;
    public string interactableAction = "Interact";
    public DialogueSequence dialogueOnInteract;

    public void Interact()
    {
        onInteract?.Invoke();
        Debug.Log("Interacted with " + gameObject.name);

        if (dialogueOnInteract != null && DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(dialogueOnInteract);
        }

        if (oneshot)
        {
            Destroy(this);
        }
    }
}
