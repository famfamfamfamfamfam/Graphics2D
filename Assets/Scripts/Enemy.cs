using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Animator enm;
    public static Enemy instance;
    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        enm = GetComponent<Animator>();
        Environment.NinjaIsInTheArea += ToRun;
        GameManager.manager.WhenChanging += ToDie;
    }
    [SerializeField]
    float speed;
    public bool isInTheArea;
    Vector3 dir;
    void ToRun()
    {
        if (isInTheArea && !enm.GetBool("attackLoop"))
        {
            dir = (Player.instance.transform.position - transform.position).normalized;
            transform.right = new Vector3 (dir.x, 0 ,0);
            enm.SetBool("toTarget", true);
            enm.SetBool("reBackRun", false);
            if (transform.position.x < 110 && transform.position.x > 60)
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
        }
        else
        {
            enm.SetBool("toTarget", false);
            enm.SetBool("reBackRun", true);
        }
    }

    void ToDie(string td)
    {
        if (td == "td")
        {
            enm.SetBool("hurtToDeath", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enm.SetTrigger("attack");
            enm.SetBool("attackLoop", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enm.SetBool("attackLoop", false);
            enm.SetTrigger("reBackAttack");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.manager.ninjaAttackFlag)
        {
            enm.SetTrigger("hurt");
            GameManager.manager.SetEnemyHealth(10);
            GameManager.manager.ninjaAttackFlag = false;
        }
    }
    private void OnDisable()
    {
        Environment.NinjaIsInTheArea -= ToRun;
        GameManager.manager.WhenChanging -= ToDie;
    }

    public void Damage()
    {
        Environment.HurtPlayer();
    }
    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
