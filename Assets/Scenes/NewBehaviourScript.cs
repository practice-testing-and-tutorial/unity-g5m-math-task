using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private Transform _target;

    [SerializeField] private float _rectWidth = 6f;
    [SerializeField] private float _rectHeight = 8f;

    [Header("Debug")]
    private float realAngleInDegree;

    private float rectDiagonalAngleInDegree;
    private float cosGuideAngle;
    private float sinGuideAngle;
    private float cosRealAngle;
    private float sinRealAngle;
    private float lineLength;

    private void OnDrawGizmos()
    {
        var center = transform.position;
        var xEdge = center + (Vector3.right * _rectWidth / 2);
        var yEdge = center + (Vector3.up * _rectHeight / 2);

        Gizmos.DrawWireCube(center, new Vector3(_rectWidth, _rectHeight, 1f));

        var targetPosition = _target.position;
        var targetNormalized = targetPosition.normalized;

        var xGuideVec = targetPosition - xEdge;

        Gizmos.DrawLine(xEdge, (xGuideVec.normalized + xEdge));

        var height = targetPosition.y;

        var targetWidth = targetPosition.x;
        var guideWidth = targetPosition.x - xEdge.x;

        var guideAngleInRad = Mathf.Atan2(height, guideWidth);

        var realAngleInRad = Mathf.Atan2(Mathf.Abs(height), Mathf.Abs(targetWidth));

        cosGuideAngle = Mathf.Cos(guideAngleInRad);
        sinGuideAngle = Mathf.Sin(guideAngleInRad);

        realAngleInDegree = realAngleInRad * 180f / Mathf.PI;

        var tanDiagonalAngle = (_rectHeight * .5f) / (_rectWidth * .5f);
        rectDiagonalAngleInDegree = Mathf.Atan(tanDiagonalAngle) * 180f / Mathf.PI;

        cosRealAngle = Mathf.Cos(realAngleInRad);
        sinRealAngle = Mathf.Sin(realAngleInRad);

        if (realAngleInDegree < rectDiagonalAngleInDegree)
        {
            lineLength = _rectWidth / 2 / cosRealAngle;
        }
        else
        {
            lineLength = _rectHeight / 2 / sinRealAngle;
        }

        Gizmos.DrawLine(center, targetNormalized * lineLength);
    }
}
