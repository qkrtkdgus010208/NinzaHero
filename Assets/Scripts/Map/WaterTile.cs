using UnityEngine;

public class WaterTile : MonoBehaviour
{
    StatHandler statHandler;

    [SerializeField] float slowMultiplier = 0.5f; // 50% 감소, 수치로 빼도 됨
    [SerializeField] float restoreDelay = 0.2f;   // 빠져나간 뒤 속도 복원 딜레이


    private float originalSpeed;


    void OnTriggerEnter2D(Collider2D other)
    {
        statHandler = other.GetComponent<StatHandler>();  //플레이어 이속 가져옴

        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        originalSpeed = statHandler.Speed;
        float changedSpeed = statHandler.Speed;

        if (statHandler != null)
        {
            changedSpeed *= slowMultiplier;
        }
        Debug.Log("물에 닿았습니다.");
        statHandler.Speed = changedSpeed;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        statHandler = other.GetComponent<StatHandler>();  //플레이어 체력 가져옴

        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        if (statHandler != null)
        {
            StartCoroutine(RestoreSpeed(statHandler));   // 약간의 딜레이 후 속도 복원 함수
        }
        Debug.Log("물에서 벗어났습니다.");
    }

    System.Collections.IEnumerator RestoreSpeed(StatHandler statHandler)       //PlayerMovement -> 플레이어 이동속도 가져올 것
    {
        yield return new WaitForSeconds(restoreDelay);  //딜레이 시간
        statHandler.Speed = originalSpeed;  //이동속도 회복
    }
}