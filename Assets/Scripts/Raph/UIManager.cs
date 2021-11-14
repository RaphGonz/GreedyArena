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
    public GameObject pauseScreen;
    public HealthBarScript healthBar; //Mettre dans l'editeur l'object health bar du canvas

    public bool onPause = false;

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);
        healthBar.gameObject.SetActive(false);
    }

    public void EnableDialogue(int seconds)
    {
        dialogueScreen.SetActive(true);
        StartCoroutine(Wait(seconds));
    }

    public void SetDialogueText(string sentence)
    {
        dialogueTextUI.GetComponent<TextMeshProUGUI>().text = sentence;
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

    public void EnablePause()
    {
        Time.timeScale = 0;
        onPause = true;
        pauseScreen.SetActive(true);
        healthBar.gameObject.SetActive(false);
    }

    public void DisablePause()
    {
        Time.timeScale = 1;
        onPause = false;
        pauseScreen.SetActive(false);
        healthBar.gameObject.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPause)
            {
                DisablePause();
            }
            else
            {
                EnablePause();
            }
        }
    }

    public void SetMaxHealth(int health)
    {
        healthBar.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        healthBar.SetHealth(health);
    }

}
