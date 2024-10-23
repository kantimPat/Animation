using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GatherInput : MonoBehaviour
{
    public Controls controls;
    public float value_x;
    public bool jump_input;
    public float jump_force = 5;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Player.Move.performed += StartMove;
        controls.Player.Move.canceled += StopMove;
        controls.Player.Jump.performed += JumpStart;
        controls.Player.Jump.canceled += JumpStop;
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= StartMove;
        controls.Player.Move.canceled -= StopMove;
        controls.Player.Jump.performed -= JumpStart;
        controls.Player.Jump.canceled -= JumpStop;
        controls.Player.Disable();
    }
    private void StartMove(InputAction.CallbackContext contxt)
    {
        value_x = contxt.ReadValue<float>();
    }
    
    private void StopMove(InputAction.CallbackContext contxt)
    {
        value_x = 0;
    }

    private void JumpStart(InputAction.CallbackContext contxt)
    {
        jump_input = true;
    }
    
    private void JumpStop(InputAction.CallbackContext contxt)
    {
        jump_input = false;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
