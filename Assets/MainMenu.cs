using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //[SerializeField] string nameEssentialScene;
    [SerializeField] string nameNewGameStartScene;
  
    public void ExitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(nameNewGameStartScene, LoadSceneMode.Single);
        //SceneManager.LoadScene(nameEssentialScene, LoadSceneMode.Additive);   
    }
}
