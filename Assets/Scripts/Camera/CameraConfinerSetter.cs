using UnityEngine;
using Cinemachine;

public class CameraConfinerSetter : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner2D;

    [SerializeField] private GameObject boundingAreaObject;

    void Start()
    {
        SetConfinerBoundingShape();
    }

    private void SetConfinerBoundingShape()
    {
        if (cinemachineConfiner2D == null)
        {
            Debug.LogError("cinemachine Confiner 2D가 존재하지 않습니다.");
            return;
        }

        if (boundingAreaObject == null)
        {
            Debug.LogError("Bounding Area Object가 존재하지 않습니다.");
            return;
        }
        
        PolygonCollider2D boundaryCollider = boundingAreaObject.GetComponent<PolygonCollider2D>();

        if (boundaryCollider == null)
        {
            Debug.LogError("Bounding Area Object가 PolygonCollider2D component를 가지고 있지 않습니다.");
            return;
        }

        cinemachineConfiner2D.m_BoundingShape2D = boundaryCollider;

        cinemachineConfiner2D.InvalidateCache();

        Debug.Log("Cinemachine Confiner 할당 완료!");
    }
}