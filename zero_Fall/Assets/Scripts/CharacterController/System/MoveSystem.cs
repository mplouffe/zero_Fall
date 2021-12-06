using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.ForEach((ref PhysicsVelocity velocity, in MovementData movement) =>
        {
            velocity.Linear -=  movement.target * movement.movementSpeed * deltaTime;
        }).Run();
    }
}
