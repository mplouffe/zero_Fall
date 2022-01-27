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
            var currentSpeed = Vector3.Magnitude(velocity.Linear);
            var diffToMax = movement.maxSpeed - currentSpeed;

            if (diffToMax > 0)
            {
                var accelerationToAdd = Mathf.Min(movement.acceleration, diffToMax);
                velocity.Linear += movement.target * accelerationToAdd;
            }
        }).Run();
    }
}
