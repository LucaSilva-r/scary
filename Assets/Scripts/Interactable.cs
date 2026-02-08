using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool oneshot = true;
    public UnityEvent onInteract;
    public string interactableAction = "Interact";

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

        onInteract?.Invoke();
        Debug.Log("Interacted with " + gameObject.name);
        if (oneshot)
        {
            Destroy(this);
        }
    }
}
