using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InverseKinematics
{
    private float deadspace = 15;

    private float a1 = 11;
    private float a2 = 30;
    private float a3 = 25;

    public void Calculate(Vector3 point, out float baseRotation, out float lowerArmRotation, out float upperArmRotation)
    { 
        point = ConstrainPoint(point);

        float r1 = (float)Math.Sqrt(point.x * point.x + point.y * point.y);
        float r2 = point.z - a1;
        float r3 = (float)Math.Sqrt(r1 * r1 + r2 * r2);

        float phi1 = (float)Math.Acos((a3 * a3 + a2 * a2 - r3 * r3) / (-2 * a2 * r3));
        float phi2 = (float)Math.Atan(r2 / r1);
        float phi3 = (float)Math.Acos((r3 * r3 + a2 * a2 - a3 * a3) / (-2 * a2 * a3));

        float rho1 = (float)Math.Atan(point.y / point.x);
        float rho2 = phi1 + phi2;
        float rho3 = (float)Math.PI - phi3;

        baseRotation = rho1 * 180 / (float)Math.PI;
        lowerArmRotation = rho2 * 180 / (float)Math.PI;
        upperArmRotation = rho3 * 180 / (float)Math.PI;
    }

    public Vector3 ConstrainPoint(Vector3 point)
    {
        Vector3 center = new Vector3 { x = 0, y = 0, z = a1 };
        float distance = Vector3.Distance(point, center);

        if (distance > (a2 + a3))
        {
            return MapPointOnCircle(point, center, a2 + a3);
        }
        else if (distance < deadspace)
        {
            return MapPointOnCircle(point, center, deadspace);
        }
        return point;
    }

    private Vector3 MapPointOnCircle(Vector3 point, Vector3 center, float diameter)
    {
        Vector3 toScale = point - center;
        toScale /= toScale.magnitude;
        toScale *= diameter;
        return toScale;
    }
}

