using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

// IGameplayActions is an interface generated from the "gameplay" action map
// we added (note that if you called the action map differently, the name of
// the interface will be different). This was triggered by the "Generate Interfaces"
// checkbox.
public class PlayerController : MonoBehaviour, IDefaultActions
{
    // MyPlayerControls is the C# class that Unity generated.
    // It encapsulates the data from the .inputactions asset we created
    // and automatically looks up all the maps and actions for us.
    InputActions controls;

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new InputActions();
            // Tell the "gameplay" action map that we want to get told about
            // when actions get triggered.
            controls.Default.SetCallbacks(this);
        }
        controls.Default.Enable();
    }

    public void OnDisable()
    {
        controls.Default.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump Triggered");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Move Triggered");
    }
}

