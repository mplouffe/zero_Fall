using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class SimpleMoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;
        Entities.ForEach((ref Translation translation, in SimpleMoveData simpleMove, in PlayerInputData input) =>
        {
            translation.Value += new float3(simpleMove.speed * input.movement.x, 0, simpleMove.speed * input.movement.y) * deltaTime;
        }).Run();
    }
}
