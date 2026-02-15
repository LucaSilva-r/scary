using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Breto/Dialogue Sequence")]
public class DialogueSequence : ScriptableObject
{
    public DialogueLine[] lines;
    public DialogueChoice[] choices;
    public DialogueSequence nextSequence;
}
