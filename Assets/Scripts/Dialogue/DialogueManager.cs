using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public DialogueUI dialogueUI;
    public InputActionReference advanceDialogueAction;

    [Header("Eventi")]
    public UnityEvent<string> onEventTrigger;
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;

    public bool IsActive { get; private set; }

    private DialogueSequence currentSequence;
    private int currentLineIndex;
    private bool waitingForChoice;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void OnEnable()
    {
        if (advanceDialogueAction != null && advanceDialogueAction.action != null)
        {
            advanceDialogueAction.action.performed += OnAdvanceInput;
            advanceDialogueAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (advanceDialogueAction != null && advanceDialogueAction.action != null)
        {
            advanceDialogueAction.action.performed -= OnAdvanceInput;
        }
    }

    private void OnAdvanceInput(InputAction.CallbackContext ctx)
    {
        if (!IsActive || waitingForChoice)
            return;

        AdvanceLine();
    }

    public void StartDialogue(DialogueSequence sequence)
    {
        if (sequence == null || sequence.lines == null || sequence.lines.Length == 0)
            return;

        currentSequence = sequence;
        currentLineIndex = 0;
        IsActive = true;
        waitingForChoice = false;

        dialogueUI.Show();
        onDialogueStart?.Invoke();
        ShowCurrentLine();
    }

    public void AdvanceLine()
    {
        if (!IsActive || currentSequence == null)
            return;

        currentLineIndex++;

        if (currentLineIndex < currentSequence.lines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            OnSequenceFinished();
        }
    }

    private void ShowCurrentLine()
    {
        DialogueLine line = currentSequence.lines[currentLineIndex];
        dialogueUI.DisplayLine(line);

        if (!string.IsNullOrEmpty(line.eventTrigger))
        {
            onEventTrigger?.Invoke(line.eventTrigger);
        }
    }

    private void OnSequenceFinished()
    {
        if (currentSequence.choices != null && currentSequence.choices.Length > 0)
        {
            waitingForChoice = true;
            dialogueUI.DisplayChoices(currentSequence.choices, OnChoiceSelected);
        }
        else if (currentSequence.nextSequence != null)
        {
            StartDialogue(currentSequence.nextSequence);
        }
        else
        {
            EndDialogue();
        }
    }

    private void OnChoiceSelected(int choiceIndex)
    {
        waitingForChoice = false;
        dialogueUI.HideChoices();

        DialogueChoice choice = currentSequence.choices[choiceIndex];

        if (choice.nextSequence != null)
        {
            StartDialogue(choice.nextSequence);
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        IsActive = false;
        currentSequence = null;
        dialogueUI.Hide();
        onDialogueEnd?.Invoke();
    }
}
