using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class MathExtension
{
    public static float3 Project(this float3 vector, float3 normal)
    {
        normal = math.normalize(normal);
        float dist = math.dot(vector, normal);
        float3 projected = dist * normal;
        return projected;
    }

    public static float3 ProjectOnPlane(this float3 vector, float3 normal)
    {
        float3 projected = vector - vector.Project(normal);
        return projected;
    }

    public static float AngleFrom(this float3 vector1, float3 vector2)
    {
        return math.acos(math.dot(math.normalize(vector1), math.normalize(vector2)));
    }

    public static quaternion ProjectOnPlane(this quaternion rotation, float3 vector)
    {
        float3 flatRotatedVector = math.mul(rotation, new float3(0, 0, 1)).ProjectOnPlane(vector);
        float angle = 0;
        if (math.dot(flatRotatedVector, new float3(1, 0, 0)) > 0)
        {
            angle = new float3(0, 0, 1).AngleFrom(flatRotatedVector);
        }
        else
        {
            angle = 2 * math.PI - new float3(0, 0, 1).AngleFrom(flatRotatedVector);
        }

        if (!float.IsNaN(angle))
        {
            return quaternion.AxisAngle(vector, angle);
        }
        else
        {
            return quaternion.identity;
        }
    }
}
