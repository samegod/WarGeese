using Additions.Enums;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
    #region Fields

    [SerializeField] private float speed;
    [SerializeField] private float offsetFromBorder = 0.5f;
    [SerializeField] private float defaultMoveAngle;

    private float currentMoveDirection = 0;

    #endregion

    #region Unity Methods

    private void Update()
    {
        if (currentMoveDirection != 0)
        {
                Vector2 direction = new Vector2(currentMoveDirection, 0);
                transform.Translate(direction * (speed * Time.deltaTime));
        }
    }

    #endregion

    #region Public Methods

    public void MoveToPoint(Vector2 position)
    {
        transform.position = Vector2.Lerp(transform.position, position, Time.deltaTime * speed);
    }

    public void Move(Direction direction)
    {
        currentMoveDirection = NormalizeDirection(direction);
    }

    public void StopMotion()
    {
        currentMoveDirection = 0;
    }

    #endregion

    #region Private Methods

    private int NormalizeDirection(Direction direction)
    {
        if (direction == Direction.Left)
            return -1;
        else if (direction == Direction.Right)
            return 1;

        return 0;
    }

    #endregion
}
