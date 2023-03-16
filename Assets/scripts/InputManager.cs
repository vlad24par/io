using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Pleyer _pleyer;

    private bool _isMovingByMouse = false;
    private Vector3 _startPosition;
    
    private void Update()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetMouseButton(0))
        {
            direction = MoveByMouse();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMovingByMouse = false;
            _startPosition = Vector3.zero;
        }
        else
        {
            direction = MoveByKeyboard();
        }

        _pleyer.Move(direction);
    }

    private Vector3 MoveByKeyboard()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W))
        {
            direction.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y -= 1;
        }

        return direction;
    }

    private Vector3 MoveByMouse()
    {
        if (!_isMovingByMouse)
        {
            _isMovingByMouse = true;
            _startPosition = Input.mousePosition;
        }

        var direction = Vector3.Normalize(Input.mousePosition - _startPosition);
        return direction;
    }
}
