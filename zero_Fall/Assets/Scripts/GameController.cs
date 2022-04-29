using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public InputActionAsset input;
    public static PilotInput pilotInput;

    void Start()
    {
        if (instance && instance != this)
        {
            Destroy(this);
        }
        instance = this;

        foreach(InputActionMap map in input.actionMaps)
        {
            map.Enable();
            switch (map.name)
            {
                case "Touch":
                    map["Move"].performed += (InputAction.CallbackContext context) =>
                    {
                        pilotInput.Movement = context.ReadValue<Vector2>();
                    };
                    break;
            }

        }
    }

    private void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) { return; }

        pilotInput.Boost = gamepad.bButton.isPressed;
    }


    public struct PilotInput
    {
        public float2 Movement;
        public bool Boost;
    }
}
