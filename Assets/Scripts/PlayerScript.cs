using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text winText;
    public Text livesText;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;    
    private int livesValue = 3;
    private int scoreValue = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + livesValue.ToString();
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        transform.position = new Vector3(0f, 0f, 49.36484f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector3(56.2f, 0.3871047f, 49.36484f);
                {
                    if (livesValue <= 2)
                    {
                        livesValue = 3;
                        livesText.text = "Lives: " + livesValue.ToString();
                    }
                }
            }
            else if (scoreValue ==8)
            {
                winText.text = "You Win! Game by Syuen Tong.";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
                musicSource.loop = true;
            }
        }
        else if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            livesText.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            if (livesValue <= 0)
            {
                winText.text = "You lose! Game by Syuen Tong.";
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}
