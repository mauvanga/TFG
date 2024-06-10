using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogoFin : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;

    private bool didDialogueStart;
    private int lineIndex;
    private bool isDialogueFinished;
    public GameObject fin;


    // Update is called once per frame
    void Update()
    {

        if (!isDialogueFinished)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }

            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }

        }else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            StartCoroutine(HideDialoguePanelWithDelay(5f));
            StartCoroutine(ShowFinPanelWithDelay(5f));
            isDialogueFinished = true;

        }
    }
    private IEnumerator HideDialoguePanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        dialoguePanel.SetActive(false);
    }

    private IEnumerator ShowFinPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        fin.SetActive(true);
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

}
