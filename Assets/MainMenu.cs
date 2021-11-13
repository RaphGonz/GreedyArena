using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //A modifier apr�s pour lancer la sc�ne du jeu
    }

    public void ShowControls()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2); //A modifier apr�s pour lancer la sc�ne des controls
    }
}
