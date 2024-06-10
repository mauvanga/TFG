using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextoHistoria : MonoBehaviour
{

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private float typingTime = 0.05f;
    private float delayBetweenLines = 3f;

    private bool didDialogueStart;
    private bool dialogueEnd = false;
    private int lineIndex;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueEnd)
            {
                if (!didDialogueStart)
                {
                    StartDialogue();
                }

                else if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();

                }

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
            StartCoroutine(dosSegundos());
        }
        else
        {
            dialoguePanel.SetActive(false);
            dialogueEnd = true;
            StartCoroutine(LoadNextSceneWithDelay());

        }
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

    private IEnumerator dosSegundos()
    {
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator LoadNextSceneWithDelay()
    {
        yield return new WaitForSeconds(delayBetweenLines);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
