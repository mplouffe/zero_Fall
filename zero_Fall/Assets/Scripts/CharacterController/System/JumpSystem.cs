using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class JumpSystem : SystemBase
{
    protected override void OnUpdate()
    {      
        Entities.ForEach((ref PhysicsVelocity velocity, in JumpData jumpData, in PlayerInputData playerInput) => {
            if (playerInput.jumping && jumpData.onGround)
            {
                velocity.Linear += jumpData.jumpDirection * jumpData.jumpForce;
            }
        }).Schedule();
    }
}
