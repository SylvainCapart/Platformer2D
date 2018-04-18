using System.Collections;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
public class Player : MonoBehaviour
{
    private PlayerStats stats;

    AudioManager audioManager;

    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    [SerializeField]
    private StatusIndicator statusIndicator;

    private float pastRegenHealth;

    private Vector3 lastPlatformPosition = Vector3.zero;

    private void Start()
    {
        stats = PlayerStats.instance;

        stats.CurrentHealth = stats.maxHealth;
        pastRegenHealth = stats.healthRegenRate;
        if (GameObject.FindObjectsOfType<Player>().Length > 1)
        {
            Debug.Log("Only one player can be instatiated");
            Destroy(this.transform.gameObject);
            return;
        }

        if (statusIndicator == null)
        {
            Debug.Log("No status indicator on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.CurrentHealth, stats.maxHealth);
        }

        GameMaster.gm.onToggleUpgrademenu += OnUpgradeMenuToggle;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in Player");
        }

        InvokeRepeating("RegenHealth", 1f / stats.healthRegenRate, 1f / stats.healthRegenRate);

    }

    public void DamagePlayer(int damageReceived)
    {
        stats.CurrentHealth -= damageReceived;
        if (stats.CurrentHealth <= 0)
        {
            audioManager.PlaySound(deathSoundName);
            GameMaster.KillPlayer(this);
        }
        else
        {
            audioManager.PlaySound(damageSoundName);
        }

        statusIndicator.SetHealth(stats.CurrentHealth, stats.maxHealth);


    }

    public void UpdateRegen()
    {
        if (pastRegenHealth != stats.healthRegenRate)
        {
            CancelInvoke("RegenHealth");
            InvokeRepeating("RegenHealth", 1f / stats.healthRegenRate, 1f / stats.healthRegenRate);

            pastRegenHealth = stats.healthRegenRate;
        }
    }

    private void Update()
    {
        if (transform.position.y <= -20)
        {
            DamagePlayer(10 ^ 6);
        }

        statusIndicator.SetHealth(stats.CurrentHealth, stats.maxHealth);

        UpdateRegen();
       

    }

    void RegenHealth()
    {
        stats.CurrentHealth += 1;
        statusIndicator.SetHealth(stats.CurrentHealth, stats.maxHealth);
    }

    void OnUpgradeMenuToggle(bool active)
    {
        //handle what happens when the upgrade menu is toggled
        Platformer2DUserControl _platControl = GetComponent<Platformer2DUserControl>();
        if (_platControl != null)
            _platControl.enabled = !active;

        Weapon _weapon = GetComponentInChildren<Weapon>();
        if (_weapon != null)
            _weapon.enabled = !active;

        Rigidbody2D _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
            _rb.gameObject.SetActive(!active);

    }

    private void OnDestroy()
    {
        GameMaster.gm.onToggleUpgrademenu -= OnUpgradeMenuToggle;
    }


}
