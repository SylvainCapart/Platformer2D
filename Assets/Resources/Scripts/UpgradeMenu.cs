using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private Text speedText;

    [SerializeField]
    private Text damageText;

    private PlayerStats stats;

    [SerializeField]
    private float healthModifier = 10f;

    [SerializeField]
    private float speedMovModifier = 1f;

    [SerializeField]
    private float damageModifier = 2f;

    [SerializeField]
    private float healthLimit = 1000f;

    [SerializeField]
    private float speedLimit = 15f;

    [SerializeField]
    private float damageLimit = 50f;

    [SerializeField]
    private int upgradeCost = 50;

    void UpdateValues()
    {
        healthText.text = "HEALTH : " + stats.maxHealth.ToString();
        speedText.text = "SPEED : " + stats.movementSpeed.ToString();
        damageText.text = "DAMAGE : " + stats.damage.ToString();
    }

    private void OnEnable()
    {
        stats = PlayerStats.instance;
        UpdateValues();
    }

    public void UpgradeHealth()
    {
        if (GameMaster.Money < upgradeCost || stats.maxHealth >= healthLimit)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.maxHealth = (int)(stats.maxHealth + healthModifier);
        stats.CurrentHealth += (int)healthModifier;
        //stats.healthRegenRate = (float)stats.maxHealth / 50f; // if we want a health regeneration propotional to the max health

        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMaster.Money < upgradeCost || stats.movementSpeed >= speedLimit)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.movementSpeed = Mathf.Round(stats.movementSpeed + speedMovModifier);
        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();
    }

    public void UpgradeDamage()
    {
        if (GameMaster.Money < upgradeCost || stats.damage >= damageLimit)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }
        stats.damage += (int)damageModifier;
        GameMaster.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");
        UpdateValues();

    }

}
