using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IOderDeactive
{
    public static Player instance;

    Animator ninja;
    Rigidbody2D ninRb;
    private void OnEnable()
    {
        instance = this;
    }
    private void Start()
    {
        ninja = GetComponent<Animator>();
        ninRb = GetComponent<Rigidbody2D>();
        Environment.PushNinjaUp += PushedUpInEnvironment;
        Environment.Elevator += Climb;
        Environment.HurtNinja += Hurt;
        GameManager.manager.WhenChanging += ToDie;
    }
    private void Update()
    {
        AttackCondition();
        JumpCondition();
        DashDownCodition();
        RunCondition();
        CamMove();
    }
    private void FixedUpdate()
    {
        Jump();
    }

    void AttackCondition()
    {
        if (Input.GetMouseButton(0))
        {
            ninja.SetTrigger("toAttack");
            GameManager.manager.ninjaAttackFlag = true;
        }
        if (Input.GetMouseButton(1))
        {
            ninja.SetTrigger("tooAttack");
            GameManager.manager.ninjaAttackFlag = true;
        }
    }
    bool canJump, isJumped;
    [SerializeField]
    float jumpHeight = 500;
    void JumpCondition()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumped)
            canJump = true;
    }
    void Jump()
    {
        if (canJump)
        {
            ninRb.AddForce(Vector2.up * jumpHeight);
            ninja.SetTrigger("toJump");
            ninja.SetBool("reBackJump", false);
            canJump = false;
            isJumped = true;
        }
    }
    float input;
    [SerializeField]
    float speed = 10;
    void RunCondition()
    {
        input = Input.GetAxis("Horizontal");
        if (!isJumped)
        {
            if (input == 0)
            {
                ninja.SetBool("toRun", false);
                ninja.SetBool("reBackRun", true);
            }
            else
            {
                if (input > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (input < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                ninja.SetBool("toRun", true);
                ninja.SetBool("reBackRun", false);
            }
        }
        transform.position += Vector3.right * input * Time.deltaTime * speed;
    }
    void DashDownCodition()
    {
        if (isJumped && Input.GetKeyDown(KeyCode.F))
        {
            ninja.SetTrigger("toDashDown");
            GameManager.manager.ninjaAttackFlag = true;
        }
    }

    void PushedUpInEnvironment()
    {
        ninRb.AddForce(Vector3.up * 1000);
        ninja.SetBool("toRun", false);
        ninja.SetBool("reBackRun", true);
        isJumped = true;
    }

    void Climb()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * 10 * Time.deltaTime;
        }
    }

    void Hurt()
    {
        ninRb.AddForce(Vector3.left * 200);
        ninja.SetTrigger("toHurt");
    }

    void ToDie(string nj)
    {
        if (nj == "nj")
            ninja.SetBool("toDie", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumped = false;
        ninja.SetBool("reBackJump", true);
    }
    [SerializeField]
    Transform checkPoint;
    void CamMove()
    {
        if (transform.position.x > checkPoint.position.x)
        {
            Camera.main.transform.position = new Vector3(73.92f, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
        if (transform.position.x < checkPoint.position.x)
        {
            Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }
    }

    public void ReLoadFromNinja()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void _OnDisable()
    {
        Environment.PushNinjaUp -= PushedUpInEnvironment;
        Environment.Elevator -= Climb;
        Environment.HurtNinja -= Hurt;
        GameManager.manager.WhenChanging -= ToDie;
    }
}
