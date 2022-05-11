using Additions.Enums;
using Additions.Utils;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _currentMoveDirection;
    public Vector3 CurrentMoveDirection => _currentMoveDirection;

    private void Update()
    {
        if (CurrentMoveDirection != Vector3.zero)
        {
                transform.Translate(CurrentMoveDirection * (speed * Time.deltaTime));
        }
    }
    
    public void MoveToPoint(Vector3 position)
    {
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
    }

    public void Move(Direction direction)
    {
        AddDirection(direction);
    }

    public void StopMotion()
    {
        _currentMoveDirection = Vector3.zero;
    }

    private void AddDirection(Direction direction)
    {
        _currentMoveDirection += DirectionAxis.GetAxis(direction);
    }
}
