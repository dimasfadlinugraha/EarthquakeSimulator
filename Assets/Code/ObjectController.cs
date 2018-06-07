using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public GameObject player;
    public GameObject gameController;

    public void getBelowTable()
    {
        GameController gameControllerComponent = gameController.GetComponent<GameController>();
        player.GetComponent<PlayerMovement>().recordPlayerLastPosition();

        Collider colliderPlayer;
        colliderPlayer = player.GetComponent<Collider>();
        colliderPlayer.enabled = false;

        Vector3 belowTablePosition = new Vector3(23f, -0.8f, 6);
        player.transform.position = belowTablePosition;

        gameControllerComponent.belowTable = true;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<AudioSource>().Play();
    }
    public void openDoor()
    {
        Vector3 rotate1 = new Vector3(0, 90, 0);
        Vector3 rotate2 = new Vector3(0, 0, 0);
        Transform door = GetComponent<Transform>();

        if (door.transform.eulerAngles.y == 0)
        {
            door.transform.eulerAngles = rotate1;
        }
        else if (door.transform.eulerAngles.y == 90)
        {
            door.transform.eulerAngles = rotate2;
        }
        GetComponent<AudioSource>().Play();
    }
    public void openDoor2()
    {
        Vector3 rotate1 = new Vector3(0, 90, 0);
        Vector3 rotate2 = new Vector3(0, 180, 0);
        Transform door = GetComponent<Transform>();

        if (door.transform.eulerAngles.y == 180)
        {
            door.transform.eulerAngles = rotate1;
        }
        else if (door.transform.eulerAngles.y == 90)
        {
            door.transform.eulerAngles = rotate2;
        }
        GetComponent<AudioSource>().Play();
    }
    public void turnOffGas()
    {
        gameController.GetComponent<GameController>().gasState = false;
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<AudioSource>().Play();
    }

    public void turnOffElectricity()
    {
        gameController.GetComponent<GameController>().electricityState = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<AudioSource>().Play();
    }

    public void flashlightAcquired()
    {
        gameController.GetComponent<GameController>().flashlightAcquired = true;
        Destroy(gameObject);
        gameController.GetComponent<GameController>().homeLights.SetActive(false);
        GetComponent<AudioSource>().Play();
    }
    public void phoneAcquired()
    {
        gameController.GetComponent<GameController>().phoneAcquired = true;
        Destroy(gameObject);
        GetComponent<AudioSource>().Play();
    }
    public void savePerson()
    {
        Destroy(gameObject);
        gameController.GetComponent<GameController>().personSaved = true;
        GetComponent<AudioSource>().Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.name == "Safe Zone")
        {
            gameController.GetComponent<GameController>().safeZone = true;
        }  
    }
    private void OnTriggerExit(Collider other)
    {
     
    }
}
