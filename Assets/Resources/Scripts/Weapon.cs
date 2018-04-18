using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float fireRate = 0;
    public int damage = 10;
    public LayerMask whatToHit;

    //camera shake effect
    public float camShakeAmount = 0.05f;
    public float camShakeDuration = 0.1f;
    CameraShake camShake;

    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public Transform HitPrefab;

    float timeToSpawnEffect = 0f;
    public float effectSpawnRate = 10f;

    float timeToFire = 0f;
    Transform firePoint;

    public string weaponShootSound = "DefaultShot";

    private AudioManager audioManager;


    private void Awake()
    {

        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firepoint found");
        }
    }

    // Use this for initialization
    void Start () {
        damage = PlayerStats.instance.damage;
        camShake = GameMaster.gm.GetComponent<CameraShake>();
        if (camShake == null)
        Debug.LogError("No CameraShake script found on GM object");

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audioManager found in weapon");
        }
    }
	
	// Update is called once per frame
	void Update () {
        damage = PlayerStats.instance.damage;
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}


    private void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100f, whatToHit);

        

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition)*100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
            }

        }

        if (Time.time >= timeToSpawnEffect)
        {
            Vector3 hitPos;
            Vector3 hitNormal;

            if (hit.collider == null)
            {
                hitPos = (mousePosition - firePointPosition) * 30;
                hitNormal = new Vector3(9999, 9999, 9999);
            }
            else
            {
                hitPos = hit.point;
                hitNormal = hit.normal;
            }

            Effect(hitPos, hitNormal);
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }
    }

    private void Effect(Vector3 hitPos, Vector3 hitNormal)
    {

        // Handle main trail for bullet
        Transform trail = Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        LineRenderer lr = trail.GetComponent<LineRenderer>();

        if (lr != null)
        {
            lr.SetPosition(0, firePoint.position);
            lr.SetPosition(1, hitPos);
        }
        Destroy(trail.gameObject, 0.03f);

        // Handle particle effect at impact
        if (hitNormal != new Vector3(9999,9999,9999))
        {
            Transform impactClone = Instantiate(HitPrefab, hitPos, Quaternion.FromToRotation(Vector3.right, hitNormal));
            Destroy(impactClone.gameObject, 1f);
        }

        // Handle shooting particle effect at fire point
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
        clone.SetParent(firePoint);
        float size = UnityEngine.Random.Range(0.6f, 0.9f);
        clone.localScale = new Vector3(size, size, size);

        // Handle shake camera
        camShake.Shake(camShakeAmount, camShakeDuration);

        //PlayShootSound
        audioManager.PlaySound(weaponShootSound);

        Destroy(clone.gameObject, 0.04f);
    }
}
