using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public void startGame(){
		SceneManager.LoadScene("LevelMenu", LoadSceneMode.Additive);
	}

	public void exitGame(){
		Application.Quit();
	}

	public void backToMainMenu(){
		SceneManager.UnloadSceneAsync("LevelMenu");
	}

	public void loadLevel1(){
		SceneManager.LoadScene("Level01", LoadSceneMode.Single);
	}
    public void loadLevel2()
    {
        SceneManager.LoadScene("Level02", LoadSceneMode.Single);
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene("Level03", LoadSceneMode.Single);
    }
}
