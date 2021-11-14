using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject dialogueScreen;
    public GameObject dialogueTextUI;

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
    }

    public void EnableDialogue(int seconds)
    {
        dialogueScreen.SetActive(true);
        StartCoroutine(Wait(seconds));
    }

    public void SetDialogueText(string sentence)
    {
        dialogueTextUI.GetComponent<Text>().text = sentence;
    }

    public void DisableDialogue()
    {
        dialogueScreen.SetActive(false);
    }

    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        DisableDialogue();
    }



}
