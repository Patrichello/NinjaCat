using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject shurikenPrefab;
    private Animator anim;
    public PlayerController playerController;
    private HealthController healthController;
    private PlayerDash playerDash;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    private bool canAttackShuriken = true;

    public AudioClip shurikenSound;
    private AudioSource audioSource;

    public int shurikenAmount;
    private int shurikenAmountLeft;
    public TMP_Text shurikenAmountDisplay;
    public Image shurikenImage;

    void Start()
    {
        shurikenAmountLeft = shurikenAmount;
        shurikenImage.gameObject.SetActive(true);

        anim = GetComponent<Animator>();
        healthController = GetComponent<HealthController>();
        playerDash = GetComponent<PlayerDash>();
        playerController = GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (CanAttackShuriken())
        {

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.L) && playerController.canJump && shurikenAmountLeft > 0)
                {
                    Shoot();
                    nextAttackTime = Time.time + 1f / attackRate;
                    shurikenAmountLeft--;
                }
            }
        }
            shurikenAmountDisplay.text = shurikenAmountLeft.ToString();

    }

    private bool CanAttackShuriken()
    {
        return canAttackShuriken && !healthController.playerDead && !playerDash.isDashing;
    }

    void Shoot()
    {
      
        anim.SetTrigger("AttackShuriken");
        StartCoroutine(ShurikenAttackAnim());
        
    }
    private IEnumerator ShurikenAttackAnim()
    {
        yield return new WaitForSeconds(0.25f);
        Instantiate(shurikenPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(shurikenSound);
    }
}
