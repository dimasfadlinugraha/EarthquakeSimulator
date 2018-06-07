using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    private CharacterController playerController;
    private PlayerMovement playerLogic;

    public Canvas canvas;
    public GameObject levelTutorialPanel;
    public GameObject controllerTutorialPanel;
    public GameObject mapTutorialPanel;
    public GameObject homeLights;
    public AudioSource earthquakeSound;
    public AudioSource rattlingGlassSound;
    public AudioSource openWorldSound;

    public float gravity = 20.0F;

    public bool earthquake = true;
    public float earthquakeForce = 0.05f;
    public float earthquakeDelayDuration = 5.0f;
    public float slowDownAmount = 1.0f;
    public bool safeZone = false;

    public bool belowTable = false;

    public bool gasState = true;
    public bool electricityState = true;
    public bool flashlight = false;
    public bool flashlightAcquired = false;
    public bool phone = false;
    public bool phoneAcquired = false;
    public bool personSaved = false;

    public bool phaseOne = false;
    public bool phaseTwo = false;
    public bool phaseThree = false;

    public int level = 1;

    void Start()
    {
        playerController = player.GetComponent<CharacterController>();
        playerLogic = player.GetComponent<PlayerMovement>();
        controllerTutorialPanel.SetActive(false);
        levelTutorialPanel.SetActive(true);
        mapTutorialPanel.SetActive(false);
        canvas.GetComponent<Canvas>().enabled = true;
        rattlingGlassSound.GetComponent<AudioSource>().Play();
        earthquakeSound.GetComponent<AudioSource>().Play();
        openWorldSound.GetComponent<AudioSource>().Play();
    }
    void Update()
    {

        if (canvas.GetComponent<Canvas>().enabled == true)
        {
            playerController.enabled = false;
            if (Input.GetButton("Cancel"))
            {
                setActivePanel(false, true, false);
                canvas.GetComponent<Canvas>().enabled = false;
                playerController.enabled = true;
            }
        }

        if (Input.GetButtonDown("Tutorial"))
        {
            canvas.GetComponent<Canvas>().enabled = true;
        }

        if (earthquake == true)
        {
            if (earthquakeDelayDuration >= 0)
            {
                playerController.transform.localPosition = playerController.transform.localPosition + Random.insideUnitSphere * earthquakeForce;
                if (belowTable == true)
                    earthquakeDelayDuration -= Time.deltaTime * slowDownAmount;
                if (safeZone == true)
                    earthquakeDelayDuration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                earthquake = false;
                rattlingGlassSound.GetComponent<AudioSource>().Stop();
                earthquakeSound.GetComponent<AudioSource>().Stop();
            }
        }

        if (level == 1)
        {
            if (belowTable == true)
            {
                deactivateTutorial("1");
                canvas.GetComponent<Canvas>().enabled = true;
            }
            if (belowTable == true && earthquake == false) // Level 1
            {
                if (Input.GetButton("Select") && phaseOne == false)
                {
                    playerLogic.getOutFromTable();
                    belowTable = false;
                    phaseOne = true;
                    deactivateTutorial("2");
                    canvas.GetComponent<Canvas>().enabled = true;
                }
            }

            if (gasState == false && electricityState == false && phaseOne == true && phaseTwo == false) // Level 1
            {
                phaseTwo = true;
                deactivateTutorial("3");
                canvas.GetComponent<Canvas>().enabled = true;
            }
            
            if (safeZone == true && phaseOne == true && phaseTwo == true && phaseThree == false) // Level 1
            {
                phaseThree = true;
            }
            if (phaseOne == true && phaseTwo == true && phaseThree == true)
            {
                SceneManager.LoadScene("Level02", LoadSceneMode.Single);
            }
        }
        
        if (level == 2)
        {
            if (safeZone == true && phaseOne==false) // Level 2
            {
                phaseOne = true;
                canvas.GetComponent<Canvas>().enabled = true;
                deactivateTutorial("1");
                deactivateTutorial("2");
            }
            if (phaseOne == true && phaseTwo == false && personSaved == true)
            {
                phaseTwo = true;
                canvas.GetComponent<Canvas>().enabled = true;
                deactivateTutorial("3");
            }
            if (phaseOne == true && phaseTwo == true && phoneAcquired == true)
            {
                phaseThree = true;
                deactivateTutorial("4");
                canvas.GetComponent<Canvas>().enabled = true;
            }
            if (phaseOne == true && phaseTwo == true && phaseThree == true)
            {
                SceneManager.LoadScene("Level03", LoadSceneMode.Single);
            }
        }
        
        if (level == 3)
        {
            if (flashlightAcquired == true && phaseOne == false)
            {
                phaseOne = true;
                deactivateTutorial("1");
                canvas.GetComponent<Canvas>().enabled = true;
            }
            if (phaseOne == true && phaseTwo == false && earthquake == false)
            {
                phaseTwo = true;
                deactivateTutorial("2");
                canvas.GetComponent<Canvas>().enabled = true;
            }
            if (phaseOne == true && phaseTwo == true)
            {
                
            }
        }
    }
    public void setActivePanel(bool controller, bool level, bool map)
    {
        controllerTutorialPanel.SetActive(controller);
        levelTutorialPanel.SetActive(level);
        mapTutorialPanel.SetActive(map);
    }

    public void deactivateTutorial(string deactivateText)
    {
        Text[] UIText = canvas.GetComponentsInChildren<Text>();
        Text deletedText = null;

        foreach (Text text in UIText)
        {
            if (text.name == deactivateText)
            {
                deletedText = text;
            }
        }
        deletedText.text = "";

        
    }
}
