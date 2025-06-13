using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorController : BaseDoor
{

    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private Vector3 leftOpenOffset;
    [SerializeField] private Vector3 rightOpenOffset;
    [SerializeField] private float speed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;
    private bool _isOpening = false;

    private void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
    }

    private void Update()
    {
        Vector3 leftTarget = _isOpening ? leftClosedPos + leftOpenOffset : leftClosedPos;
        Vector3 rightTarget = _isOpening ? rightClosedPos + rightOpenOffset : rightClosedPos;

        leftDoor.position = Vector3.Lerp(leftDoor.position, leftTarget, Time.deltaTime * speed);
        rightDoor.position = Vector3.Lerp(rightDoor.position, rightTarget, Time.deltaTime * speed);
    }

    public override void Open()
    {
        _isOpening = true;
    }

    public override void Close()
    {
        _isOpening = false;
    }

}
