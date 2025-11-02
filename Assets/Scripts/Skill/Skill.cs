using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SkillData data;
    public int level;

    private StatHandler statHadler;
    private RangeWeaponHandler rangeWeaponHandler;
    private GameManager gameManager;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;

    private void Start()
    {
        gameManager = GameManager.Instance;

        statHadler = gameManager.player.GetComponent<StatHandler>();
        rangeWeaponHandler = gameManager.player.GetComponent<RangeWeaponHandler>();

        icon.sprite = data.skillIcon;
        textName.text = data.skillName;
        textDesc.text = data.skillDesc;
    }

    private void OnEnable()
    {
        textLevel.text = $"Lv.{level + 1}";
    }

    public void OnClick()
    {
        switch (data.skillType)
        {
            case SkillData.SkillType.MoveSpeed:
                statHadler.Speed = data.upgradeStats[level];
                break;
            case SkillData.SkillType.AttackSpeed:
                rangeWeaponHandler.Speed = data.upgradeStats[level];
                break;
            case SkillData.SkillType.Power:
                rangeWeaponHandler.Power = data.upgradeStats[level];
                break;
            case SkillData.SkillType.Range:
                rangeWeaponHandler.AttackRange = data.upgradeStats[level];
                break;
            case SkillData.SkillType.Count:
                rangeWeaponHandler.NumberofProjectilesPerShot = (int)data.upgradeStats[level];
                break;
            case SkillData.SkillType.Heal:
                statHadler.Health = (int)data.baseStat;
                break;
        }

        level++;
    }
}
