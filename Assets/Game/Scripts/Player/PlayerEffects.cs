using System.Collections;
using Additions.Enums;
using Additions.Utils;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    #region Fields

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float motionRotationAngle = 30f;

    private Vector3 _targetRotation;
    private Coroutine _rotationCoroutine;


    #endregion

    #region Unity Methods

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetRotation), Time.deltaTime);
    }

    #endregion

    #region Public Methods

    public void StartMoveEffect(Direction direction)
    {
        float koef = DirectionAxis.GetAxis(direction).x;
        
        if (_rotationCoroutine != null)
            StopCoroutine(_rotationCoroutine);

        _rotationCoroutine = StartCoroutine(Rotation(new Vector3(0, 0, motionRotationAngle * koef)));
    }

    public void SetTargetRotation(Vector3 target)
    {
        _targetRotation = target;
    }

    private IEnumerator Rotation(Vector3 targetAngles)
    {
        Vector3 initialRotation = transform.eulerAngles;

        while(Vector3.Distance(transform.eulerAngles, targetAngles) > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngles), Time.deltaTime * rotationSpeed);
            yield return null;
        }

        yield return null;
    }

    #endregion
}
