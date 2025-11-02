using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyManager : MonoBehaviour
{
    private Coroutine stageRoutine;

    [Header("Prefabs & Spawn")]
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<Rect> spawnAreas;
    [SerializeField] private Color gizmoColor = new Color(1, 0, 0, 0.3f);

    [Header("Spawn Tuning")]
    [Tooltip("스폰 수 비율(1=그대로, 0.5=절반, 2=두 배)")]
    [Range(0f, 2f)][SerializeField] private float spawnCountScale = 0.5f;

    [Header("Spawn Mix (when name not matched)")]
    [Range(0f, 1f)]
    [SerializeField] private float meleeSpawnRatio = 0.5f; // 이름 매칭 안될 때만 사용

    [Header("Visibility Safeguards")]
    [SerializeField] private bool forceVisibilityFix = true;
    [SerializeField] private string characterSortingLayerName = "Characters";
    [SerializeField] private int characterSortingOrder = 50;

    [Header("Debug")]
    [SerializeField] private bool verboseLogs = true;

    public List<EnemyController> activeEnemies = new List<EnemyController>();
    private bool enemySpawnComplite;
    private GameManager gameManager;

    public void Init(GameManager gameManager) => this.gameManager = gameManager;

    public void StartStage(int stageCount)
    {
        if (stageCount <= 0)
        {
            gameManager.EndOfStage();
            return;
        }

        if (stageRoutine != null) StopCoroutine(stageRoutine);
        stageRoutine = StartCoroutine(SpawnStage(stageCount));
    }

    public void StopStage() => StopAllCoroutines();

    private IEnumerator SpawnStage(int stageCount)
    {
        enemySpawnComplite = false;

        // 요청: 스폰 수를 절반으로 → 비율로 스케일링
        int spawnTotal = Mathf.Max(1, Mathf.RoundToInt(stageCount * Mathf.Max(0f, spawnCountScale)));

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < spawnTotal; i++)
        {
            SpawnRandomEnemy();
            yield return null;
        }

        enemySpawnComplite = true;
    }

    private void SpawnRandomEnemy()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0 || spawnAreas == null || spawnAreas.Count == 0)
        {
            Debug.LogWarning("[EnemyManager] Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        SpawnInternal(prefab, logTag: "[Spawn]");
    }

    private void SpawnInternal(GameObject prefab, string logTag)
    {
        if (prefab == null)
        {
            Debug.LogWarning($"{logTag} prefab=null");
            return;
        }
        if (spawnAreas == null || spawnAreas.Count == 0)
        {
            Debug.LogWarning($"{logTag} Spawn Areas 미설정");
            return;
        }

        Rect area = spawnAreas[Random.Range(0, spawnAreas.Count)];
        Vector2 pos = new Vector2(Random.Range(area.xMin, area.xMax), Random.Range(area.yMin, area.yMax));

        GameObject go = Instantiate(prefab, (Vector3)pos, Quaternion.identity);
        if (!go.activeSelf)
        {
            if (verboseLogs) Debug.LogWarning($"{logTag} Spawned inactive -> SetActive(true): {prefab.name}");
            go.SetActive(true);
        }

        if (verboseLogs) Debug.Log($"{logTag} {prefab.name} @ {pos}");

        var ec = go.GetComponent<EnemyController>();
        if (ec == null)
        {
            Debug.LogError($"{logTag} EnemyController 없음: {prefab.name}");
            return;
        }

        // === 타입 자동 설정 (프리팹 이름으로 분기) ===
        var wh = go.GetComponent<WeaponHandler>();
        string nameLow = prefab.name.ToLower();

        if (nameLow.Contains("heart"))
        {
            // Heart = 돌진만
            SafeConfigureCharger(ec, true);
            SafeConfigureContact(ec, false);
            SafeConfigureChaseOnly(ec, false);
            if (wh != null) wh.enabled = false;
        }
        else if (nameLow.Contains("mushroom"))
        {
            // Mushroom = 추격만
            SafeConfigureCharger(ec, false);
            SafeConfigureContact(ec, false);
            SafeConfigureChaseOnly(ec, true);
            if (wh != null) wh.enabled = false;
        }
        else if (nameLow.Contains("snake"))
        {
            // Snake = 원거리만
            SafeConfigureCharger(ec, false);
            SafeConfigureContact(ec, false);
            SafeConfigureChaseOnly(ec, false);
            if (wh != null) wh.enabled = true;
        }
        else
        {
            // 기타 이름: 원거리/근접 섞기(원하면 사용)
            bool spawnAsMelee = Random.value < meleeSpawnRatio;
            SafeConfigureCharger(ec, false);
            SafeConfigureContact(ec, spawnAsMelee);
            SafeConfigureChaseOnly(ec, false);
            if (wh != null) wh.enabled = !spawnAsMelee;
        }

        // 타깃/매니저 주입
        if (gameManager == null || gameManager.player == null)
        {
            Debug.LogError($"{logTag} gameManager.player가 없습니다.");
            return;
        }

        ec.Init(this, gameManager.player.transform);
        activeEnemies.Add(ec);

        // 가시성 보정(정렬/알파/Z/스케일)
        if (forceVisibilityFix) ApplyVisibilityFix(go, logTag);
    }

    // ===== 안전 호출(구버전 EnemyController라도 크래시 없이 무시) =====
    private void SafeConfigureContact(EnemyController ec, bool enable)
    {
        try { ec.ConfigureContactAttack(enable); } catch { if (verboseLogs) Debug.Log("[EnemyManager] ConfigureContactAttack API 없음(무시)."); }
    }
    private void SafeConfigureCharger(EnemyController ec, bool enable)
    {
        try { ec.ConfigureCharger(enable); } catch { if (verboseLogs) Debug.Log("[EnemyManager] ConfigureCharger API 없음(무시)."); }
    }
    private void SafeConfigureChaseOnly(EnemyController ec, bool enable)
    {
        try { ec.ConfigureChaseOnly(enable); } catch { if (verboseLogs) Debug.Log("[EnemyManager] ConfigureChaseOnly API 없음(무시)."); }
    }

    // ===== 가시성 보정 =====
    private void ApplyVisibilityFix(GameObject go, string logTag)
    {
        // 위치 Z=0, 스케일=1
        var p = go.transform.position;
        go.transform.position = new Vector3(p.x, p.y, 0f);
        go.transform.localScale = Vector3.one;

        // 모든 SpriteRenderer 보이게 + 레이어/오더 상향 + 알파=1
        var srs = go.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var sr in srs)
        {
            sr.enabled = true;
            var c = sr.color; c.a = 1f; sr.color = c;

            if (!string.IsNullOrEmpty(characterSortingLayerName))
                sr.sortingLayerName = characterSortingLayerName;

            sr.sortingOrder = Mathf.Max(sr.sortingOrder, characterSortingOrder);
        }

        // SortingGroup도 보정
        var sg = go.GetComponentInChildren<SortingGroup>(true);
        if (sg != null)
        {
            sg.enabled = true;
            if (!string.IsNullOrEmpty(characterSortingLayerName))
                sg.sortingLayerName = characterSortingLayerName;
            sg.sortingOrder = Mathf.Max(sg.sortingOrder, characterSortingOrder);
        }

        // 화면 안에 있는지 로그
        var cam = Camera.main;
        if (cam != null && verboseLogs)
        {
            var vp = cam.WorldToViewportPoint(go.transform.position);
            Debug.Log($"{logTag} Viewport {vp}");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemySpawnComplite && activeEnemies.Count == 0)
            gameManager.EndOfStage();
    }
}
