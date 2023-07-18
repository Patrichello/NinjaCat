using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool playerDead = false;

    private Animator anim;
    private GameProgress gameProgress;
    private GameOver gameOver;
    private AudioSource audioSource;
    public AudioClip gameOverSound;
    public AudioClip hurtSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameProgress = FindObjectOfType<GameProgress>();
        gameOver = FindObjectOfType<GameOver>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        HealthControl();

        if (playerDead)
        {
            audioSource.PlayOneShot(gameOverSound);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (!playerDead)
        {
            anim.SetTrigger("hurt");
            audioSource.PlayOneShot(hurtSound);

        }
        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            Debug.Log("PlayerDead");
            playerDead = true;

        }
    }

        public void HealthControl()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;

            }
            if (health < 1)
            {
                playerDead = true;

                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;

                StartCoroutine(DieAnimation());

                PlayerPrefs.DeleteKey("scoreKey");
                PlayerPrefs.DeleteKey("GetStar");
            }
            if (transform.position.y < -10)
            {
                GetComponent<Collider2D>().enabled = false;
                this.enabled = false;

                playerDead = true;
                SceneLoadDie();

                PlayerPrefs.DeleteKey("scoreKey");
                PlayerPrefs.DeleteKey("GetStar");

                ResetCoinsAfterDie();
            }


        }
    }

    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(3);
        SceneLoadDie();

        ResetCoinsAfterDie();
    }

    private void SceneLoadDie()
    {
        gameOver.GameOverMenu();
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetCoinsAfterDie()
    {
        gameProgress.ResetCollectedCoins(); // Reset index of save coins
        gameProgress.ActivateAllCoins(); // Active all prefabs coins
    }
}

