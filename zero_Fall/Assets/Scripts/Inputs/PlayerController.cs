using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

// IGameplayActions is an interface generated from the "gameplay" action map
// we added (note that if you called the action map differently, the name of
// the interface will be different). This was triggered by the "Generate Interfaces"
// checkbox.
[DefaultExecutionOrder(-1)]
public class PlayerController : Singleton<PlayerController>
{

    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion

    // MyPlayerControls is the C# class that Unity generated.
    // It encapsulates the data from the .inputactions asset we created
    // and automatically looks up all the maps and actions for us.
    InputActions controls;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new InputActions();
            // Tell the "touch" action map that we want to get told about
            // when actions get triggered.
            // controls.Touch.SetCallbacks(this);
            // Not doing this cause manually assigning touch input below
        }
        controls.Touch.Enable();
    }

    public void OnDisable()
    {
        controls.Touch.Disable();
    }

    void Start()
    {
        controls.Touch.TouchInput.started += ctx => StartTouchInput(ctx);
        controls.Touch.TouchInput.canceled += ctx => EndTouchInput(ctx);
    }

    private void StartTouchInput(InputAction.CallbackContext context)
    {
        OnStartTouch?.Invoke(controls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
    }

    private void EndTouchInput(InputAction.CallbackContext context)
    {
        OnEndTouch?.Invoke(controls.Touch.TouchPosition.ReadValue<Vector2>(), (float)context.time);
    }

    public Vector2 TouchPosition()
    {
        return Utils.ScreenToWorld(mainCamera, controls.Touch.TouchPosition.ReadValue<Vector2>());
    }
}

