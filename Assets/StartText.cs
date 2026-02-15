using UnityEngine;

public class StartText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public DialogueSequence sequenceToLoad = null;
    void Start()
    {
        if (DialogueManager.Instance)
        {
            DialogueManager.Instance.StartDialogue(sequenceToLoad);
        }
        else
        {
            Debug.LogError("DialogueManager reference is missing in StartText.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
