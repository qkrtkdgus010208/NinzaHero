using UnityEngine;

public class WaterTile : MonoBehaviour
{
    [SerializeField] float slowMultiplier = 0.5f; // 50% 감소, 수치로 빼도 됨
    [SerializeField] float restoreDelay = 0.2f;   // 빠져나간 뒤 속도 복원 딜레이

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        //var move = other.GetComponent<PlayerMovement>();  // PlayerMovement -> 플레이어 이동속도 가져올 것
        //if (move != null)
        //{
        //    move.MoveSpeed *= slowMultiplier;
        //}
        Debug.Log("물에 닿았습니다.");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        //var move = other.GetComponent<PlayerMovement>();                  //PlayerMovement -> 플레이어 이동속도 가져올 것
        //if (move != null)
        //{
        //    StartCoroutine(RestoreSpeed(move));   // 약간의 딜레이 후 속도 복원 함수
        //}
        Debug.Log("물에서 벗어났습니다.");
    }

    //System.Collections.IEnumerator RestoreSpeed(PlayerMovement move)       //PlayerMovement -> 플레이어 이동속도 가져올 것
    //{
    //    yield return new WaitForSeconds(restoreDelay);  //딜레이 시간
    //    move.ResetSpeed();  //이동속도 회복
    //}
}