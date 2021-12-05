using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class FollowTargetSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref MovementData movement, in TargetData target, in Translation translation) => {
            ComponentDataFromEntity<Translation> translationsArray = GetComponentDataFromEntity<Translation>(true);
            if (!translationsArray.HasComponent(target.followEntity)) { return; }

            Translation targetPosition = translationsArray[target.followEntity];

            movement.target = targetPosition.Value - translation.Value;

        }).Schedule();
    }
}
