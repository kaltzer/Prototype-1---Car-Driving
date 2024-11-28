using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 5, -7);
    public Transform firstPersonOffset;
    private bool firstPersonMode = false;
    public InputActionAsset playerInputActionAsset;
    private InputAction cameraToggleAction;

    private void OnEnable()
    {
        if(player.CompareTag("Player1"))
        {
            cameraToggleAction = playerInputActionAsset.FindActionMap("Player1").FindAction("CameraToggle");
        }
        else if(player.CompareTag("Player2"))
        {

            cameraToggleAction = playerInputActionAsset.FindActionMap("Player2").FindAction("CameraToggle");
        }
        
        cameraToggleAction.Enable(); // Enable the jump action
    }
    private void OnDisable()
    {
        cameraToggleAction.Disable(); // Disable the jump action
    }

    // Update is called once per frame
    private void Update()
    {

        if(cameraToggleAction.triggered)
        {
            firstPersonMode = !firstPersonMode;
        }
       
    }
    void LateUpdate()
    {
        
        if (firstPersonMode)
        {
            transform.position = firstPersonOffset.position;
            transform.rotation = firstPersonOffset.rotation;
        }
        else
        {
            transform.position = player.transform.position + offset;
        }
    }
}
