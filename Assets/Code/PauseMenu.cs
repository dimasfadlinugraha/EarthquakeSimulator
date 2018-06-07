using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject player;
    public GameObject gameController;

    public void resumeGame()
    {
        player.transform.position = player.GetComponent<PlayerMovement>().lastPlayerPosition;
        player.transform.rotation = player.GetComponent<PlayerMovement>().lastPlayerRotation;
        
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void loadControllerTutorial()
    {
        string headerText = "Controller Tutorial";
        GameController gameControllerComponent = gameController.GetComponent<GameController>();
        gameControllerComponent.setActivePanel(true,false,false);
        gameControllerComponent.canvas.GetComponent<Canvas>().enabled = true;
    }
   
}
