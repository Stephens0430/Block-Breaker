using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretAimAtMouse : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    private Vector3 mousePosition;
    private Vector3 mouseWorldPosition;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        FlattenMousePosition();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        var targetPosition = mousePosition - this.transform.position;
        transform.forward = Vector3.RotateTowards(transform.forward, targetPosition, turnSpeed, 0f);
    }

    private void FlattenMousePosition()
    {
        //Flatten the mouse to world so that it aligns with the Z of the playing field
        var currentMousePositionValue = Mouse.current.position.ReadValue();
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(currentMousePositionValue);
        mousePosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, this.transform.position.z);
    }
}
