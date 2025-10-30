using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 2000)][SerializeField] private int health = 500;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 1000);
    }

    [Range(100f, 1000f)][SerializeField] private float attack = 100;
    public float Attack
    {
        get => attack;
        set => attack = Mathf.Clamp(value, 0, 1000);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }

    [Range(1f, 5f)][SerializeField] private float projectileCount = 1;
    public float ProjectileCount
    {
        get => projectileCount;
        set => projectileCount = Mathf.Clamp(value, 0, 5);
    }
}
