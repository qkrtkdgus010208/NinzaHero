using UnityEngine;
using Cinemachine;

public class CameraConfinerSetter : MonoBehaviour
{
    [SerializeField] private CinemachineConfiner2D cinemachineConfiner2D;

    [SerializeField] private GameObject[] boundingAreaObjects;

    public void SetConfinerBoundingShape()
    {
        if (cinemachineConfiner2D == null)
        {
            Debug.LogError("cinemachine Confiner 2D가 존재하지 않습니다.");
            return;
        }

        if (boundingAreaObjects == null)
        {
            Debug.LogError("Bounding Area Object가 존재하지 않습니다.");
            return;
        }   

        int stageIndex = GameManager.Instance.stageIndex;
        int index;

        if (stageIndex >= 0 && stageIndex <= 2)
            index = 0;
        else if (stageIndex == 3)
            index = 1;
        else if (stageIndex == 4)
            index = 2;
        else 
            index = 3;

        PolygonCollider2D boundaryCollider = boundingAreaObjects[index].GetComponent<PolygonCollider2D>();

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