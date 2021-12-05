using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float2 movementInput = GameController.pilotInput.Movement;
        bool jumping = GameController.pilotInput.Jump;

        Entities.ForEach((ref PlayerInputData input, ref MovementData movement, ref RotateData rotateData) =>
        {
            input.movement = movementInput;
            input.jumping = jumping;

            movement.target = new float3(movementInput.x, 0, movementInput.y);
            rotateData.rotateTargetPosition = movement.target;
        }).Run();
    }
}
