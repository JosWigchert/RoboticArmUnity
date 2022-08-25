using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Quaternion BaseRotation;
    public Quaternion LowerRotation;
    public Quaternion UpperRotation;

    private InverseKinematics ik;

    // Start is called before the first frame update
    void Start()
    {
        ik = new InverseKinematics();

        Move(new Vector3 { x = 20, y = 20, z = 20 });
    }

    void Move(Vector3 point)
    {
        ik.Calculate(point, out float baseRotationOut, out float lowerArmRotationOut, out float upperArmRotationOut);

        BaseRotation.x = baseRotationOut;
        LowerRotation.x = lowerArmRotationOut;
        UpperRotation.x = upperArmRotationOut;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
