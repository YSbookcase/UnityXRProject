using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [SerializeField] private BaseDoor _door;

    private int _objectOnButton = 0;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _objectOnButton++;
            _door.Open();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            _objectOnButton--;
            if(_objectOnButton <= 0)
            _door.Close();

        }
    }
}
