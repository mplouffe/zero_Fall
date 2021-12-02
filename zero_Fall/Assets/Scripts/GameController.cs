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
                case "Default":
                    map["Move"].performed += (InputAction.CallbackContext context) =>
                    {
                        pilotInput.Movement = context.ReadValue<Vector2>();
                    };
                    map["Jump"].performed += (InputAction.CallbackContext context) =>
                    {
                        Debug.Log("Jumped");
                        pilotInput.Jump = (context.ReadValue<float>() < 0.5f);
                    };
                    break;
            }

        }
    }

    public struct PilotInput
    {
        public float2 Movement;
        public bool Jump;
    }
}
