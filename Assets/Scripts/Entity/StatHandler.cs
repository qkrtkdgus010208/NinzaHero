using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 2000)][SerializeField] private int health = 1000;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 2000);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3;
    public float Speed
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }
}
