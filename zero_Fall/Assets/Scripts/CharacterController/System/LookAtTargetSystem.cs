using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public class LookAtTargetSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithAll<TargetData>()
            .ForEach((ref RotateData rotateData, in Translation translation, in TargetData target) => {
                ComponentDataFromEntity<Translation> translationsArray = GetComponentDataFromEntity<Translation>(true);
                if (!translationsArray.HasComponent(target.lookAtEntity)) { return; }

                Translation targetPosition = translationsArray[target.lookAtEntity];

                rotateData.rotateTargetPosition = targetPosition.Value - translation.Value;

        }).Schedule();
    }
}
