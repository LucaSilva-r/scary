using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Pannello Dialogo")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    [Header("Pannello Scelte")]
    public GameObject choicesPanel;
    public GameObject[] choiceContainers;
    public Button[] choiceButtons;
    public TextMeshProUGUI[] choiceTexts;

    public void Show()
    {
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
    }

    public void Hide()
    {
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
    }

    public void DisplayLine(DialogueLine line)
    {
        choicesPanel.SetActive(false);

        if (string.IsNullOrEmpty(line.speakerName))
        {
            speakerText.gameObject.SetActive(false);
        }
        else
        {
            speakerText.gameObject.SetActive(true);
            speakerText.text = line.speakerName;
        }

        dialogueText.text = line.text;
    }

    public void DisplayChoices(DialogueChoice[] choices, Action<int> onChoiceSelected)
    {
        choicesPanel.SetActive(true);

        for (int i = 0; i < choiceContainers.Length; i++)
        {
            if (i < choices.Length)
            {
                choiceContainers[i].SetActive(true);
                choiceTexts[i].text = choices[i].choiceText;

                int index = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => onChoiceSelected(index));
            }
            else
            {
                choiceContainers[i].SetActive(false);
            }
        }
    }

    public void HideChoices()
    {
        choicesPanel.SetActive(false);
    }
}
