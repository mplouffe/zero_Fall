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
        float2 movement = GameController.pilotInput.Movement;
        bool jumping = GameController.pilotInput.Jump;

        JobHandle inputJob = Entities.ForEach((ref PlayerInputData input) =>
        {
            input.movement = movement;
            input.jumping = jumping;
        }).Schedule(Dependency);
        inputJob.Complete();
    }
}
