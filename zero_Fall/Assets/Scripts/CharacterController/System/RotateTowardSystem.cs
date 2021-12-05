using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotateTowardSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation, in RotateData rotationData) =>
        {
            // check if input is zero
            if (!rotationData.rotateTargetPosition.Equals(float3.zero))
            {
                // lerp current rotation towards new input direction
                quaternion targetRotation = quaternion.LookRotationSafe(rotationData.rotateTargetPosition, math.up());
                rotation.Value = math.slerp(rotation.Value, targetRotation, rotationData.rotateSpeed);
            }
        }).Schedule();
    }
}
