using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_Movement : MonoBehaviour
{
    public Player_Movement move;
    public Rigidbody rb;
    public Transform player;
    public Text score;
    int scene = SceneManager.GetActiveScene().buildIndex;

    public float speed = 4000f;
    public float moveSpeed = 4000f;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    [Header("Jump Settings")]
    public float jumpForce;
    public float jumpCool;
    bool canJump;
    bool onGround;

    private void Awake()
    {
        canJump = true;
    }

    public static void task ()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = true;
        else if (collision.collider.tag == "Obs" || player.position.y < -10)
        {
            move.enabled = false;
            Thread.Sleep(2000);
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            onGround = false;
    }

    void PlayerJump(int mult)
    {
        if (onGround)
        {
            rb.AddForce(0, jumpForce*mult, 0);
            Debug.Log("Jump");
            canJump = false;
            Invoke(nameof(ResetJumpCool), jumpCool);
        }
    }
    void ResetJumpCool()
    {
        canJump = true;
    }

    private void Update()
    {
        if (canJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerJump(1);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerJump(2);
            }

        }
        if (player.position.z > 1500)
        {
            if (scene == 0)
            {
                FindObjectOfType<GameManager>().WinGame();
                Thread.Sleep(3000);
                SceneManager.LoadScene(scene + 1);
            }
        }
        if (player.position.y < -10)
        {
            move.enabled = false;
            Thread.Sleep(2000);
            FindObjectOfType<GameManager>().EndGame();
        }

    }
    void FixedUpdate()
    {
        rb.AddForce(0, 0, speed * Time.deltaTime);
        rb.AddForce(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey("a"))
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -1, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey("d"))
        {
            rb.AddForce(moveSpeed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey("s"))
        {
            rb.AddForce(0, 0, -0.2f * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey("w"))
        {
            rb.AddForce(0, 0, 0.2f * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        score.text = Convert.ToInt32(player.position.z + 50).ToString();
    }
}