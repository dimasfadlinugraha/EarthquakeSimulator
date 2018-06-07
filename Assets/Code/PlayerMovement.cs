using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

	public Camera camera;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 lastPlayerPosition;
	public Quaternion lastPlayerRotation;
	public CharacterController characterController;
    public GameObject gameController;

    public float playerSpeed = 6.0F;
    public float runSpeed = 50.0f;
    public float jumpSpeed = 4.0F;
    public bool pauseMenu = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
	void Update()
    {
        if (Input.GetButton("Run"))
            playerSpeed = 12.0f;

        if (characterController.isGrounded) {
			Vector3 forward = camera.transform.TransformDirection (Vector3.forward);
			forward.y = 0;
			forward = forward.normalized;
			Vector3 right = new Vector3 (forward.z, 0, -forward.x);
			float h = Input.GetAxis ("Horizontal");
			float v = Input.GetAxis ("Vertical");
			moveDirection = (h*right + v*forward);
			moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= playerSpeed;
            
		}

        if (Input.GetButtonDown("Esc") && (pauseMenu == false)) {
			lastPlayerPosition = characterController.transform.position;
			lastPlayerRotation = characterController.transform.rotation;
            pauseMenu = true;
		}

		if (Input.GetButtonUp ("Esc")) {
			characterController.transform.position = new Vector3 (0, 499, 0);
		}

        if (Input.GetButtonDown("Flashlight") && gameController.GetComponent<GameController>().flashlightAcquired == true)
        {
            gameController.GetComponent<GameController>().flashlight = !gameController.GetComponent<GameController>().flashlight;
            GetComponentInChildren<Light>().enabled = gameController.GetComponent<GameController>().flashlight;
        }
        if (Input.GetButtonDown("Map"))
        {
            GameController gameControllerComponent = gameController.GetComponent<GameController>();
            gameControllerComponent.setActivePanel(false, false, true);
            gameControllerComponent.canvas.GetComponent<Canvas>().enabled = true;
        }
        
        transform.Rotate(0, transform.localEulerAngles.y, 0);
        moveDirection.y -= gameController.GetComponent<GameController>().gravity * Time.deltaTime;

        if (gameController.GetComponent<GameController>().belowTable == false)
            characterController.Move(moveDirection * Time.deltaTime);

        playerSpeed = 6.0f;
    }

    public void recordPlayerLastPosition()
    {
        lastPlayerPosition = characterController.transform.position;
    }

    public void getOutFromTable()
    {
        characterController.transform.position = lastPlayerPosition;
        Collider collider;
        collider = GetComponent<Collider>();
        collider.enabled = true;
    }
}
