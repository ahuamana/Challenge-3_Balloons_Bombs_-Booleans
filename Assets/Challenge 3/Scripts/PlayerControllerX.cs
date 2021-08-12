using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver = false;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip BoingSound;

    Vector3 maxHigh = new Vector3(0, 14.4f, 0);
    public bool isMaxHigh = false;


    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;

        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.y < maxHigh.y)
        {
            isMaxHigh = false;
        }

        if (gameObject.transform.position.y > maxHigh.y)
        {
            isMaxHigh = true;
        }

        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && !isMaxHigh)
        {

            playerRb.AddForce(Vector3.up * floatForce);
            Debug.Log("Presionaste Enter");


        }

      
    }


    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        }

        // if player collides with money, fireworks
        else 
        { if (other.gameObject.CompareTag("Money"))
            {
                fireworksParticle.Play();
                playerAudio.PlayOneShot(moneySound, 1.0f);
                Destroy(other.gameObject);

            }
            else
            {
                if (other.gameObject.CompareTag("Ground"))
                {
                    Debug.Log("Aplicando fuerza");
                    playerRb.AddForce(Vector3.up * 8f, ForceMode.Impulse);
                    playerAudio.PlayOneShot(BoingSound, 1.0f);
                }
            
            }

        }
    }

}
