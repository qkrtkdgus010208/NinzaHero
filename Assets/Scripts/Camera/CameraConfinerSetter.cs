using UnityEngine;
using Cinemachine;

public class CameraConfinerSetter : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner2D;

    public void SetConfinerBoundingShape(PolygonCollider2D polygonCollider2D)
    {
        if (cinemachineConfiner2D == null)
        {
            Debug.LogError("cinemachine Confiner 2D가 존재하지 않습니다.");
            return;
        }

        cinemachineConfiner2D.m_BoundingShape2D = polygonCollider2D;

        cinemachineConfiner2D.InvalidateCache();

        Debug.Log("Cinemachine Confiner 할당 완료!");
    }
}