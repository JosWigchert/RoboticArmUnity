using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Transform BaseRotation;
    public Transform LowerRotation;
    public Transform UpperRotation;

    public TMP_InputField xtext; 
    public TMP_InputField ytext; 
    public TMP_InputField ztext;


    private InverseKinematics ik;

    // Start is called before the first frame update
    void Start()
    {
        ik = new InverseKinematics();

        Move(new Vector3 { x = 20, y = 20, z = 20 });
    }

    public void Move()
    {
        Move(new Vector3 { x = float.Parse(xtext.text), y = float.Parse(ytext.text), z = float.Parse(ztext.text) });
    }

    private void Move(Vector3 point)
    {
        ik.Calculate(point, out float baseRotationOut, out float lowerArmRotationOut, out float upperArmRotationOut);

        Debug.Log($"Base: {baseRotationOut}, Lower: {lowerArmRotationOut}, Upper: {upperArmRotationOut}");

        BaseRotation.localRotation = Quaternion.Euler(0, baseRotationOut, 0);
        LowerRotation.localRotation = Quaternion.Euler(0, 0, -lowerArmRotationOut);
        UpperRotation.localRotation = Quaternion.Euler(0, 0, -upperArmRotationOut);
        //BaseRotation.RotateTo(Vector3.up, baseRotationOut);
        //LowerRotation.Rotate(Vector3.back, lowerArmRotationOut);
        //UpperRotation.Rotate(Vector3.back, upperArmRotationOut);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
