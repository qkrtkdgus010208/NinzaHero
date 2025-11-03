using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public class SkillData : ScriptableObject
{
    public enum SkillType { MoveSpeed, AttackSpeed, Power, Range, Count, Heal }

    [Header("# Main Info")]
    public SkillType skillType;
    public int skillId;
    public string skillName;
    [TextArea]
    public string skillDesc;
    public Sprite skillIcon;

    [Header("# Level Data")]
    public float baseStat;
    public float[] upgradeStats;
}
