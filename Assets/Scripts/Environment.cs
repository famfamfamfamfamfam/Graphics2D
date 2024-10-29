using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Environment : MonoBehaviour, IOderDeactive
{
    #region world-build event
    public delegate void ForChar();
    public static event ForChar NinjaIsInTheArea;
    public static event ForChar PushNinjaUp;
    public static event ForChar Elevator;
    public static event ForChar HurtNinja;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Coin"))
        {
            GameManager.manager.SetScore(1);
            gameObject.SetActive(false);
        }
        else if (gameObject.CompareTag("Fire") || gameObject.CompareTag("Bullet"))
        {
            HurtPlayer();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Motor"))
        {
            Elevator?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Fire"))
        {
            DontDestroy.instance.SetNoti("YOU LOST");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    [SerializeField]
    Animator pop;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Fire"))
        {
            if (collision.gameObject.transform.position.x > 45)
            {
                HurtPlayer();
            }
            else
            {
                pop.SetTrigger("oneTime");
                PushNinjaUp?.Invoke();
            }
        }
        if (gameObject.CompareTag("Reach") && collision.gameObject.transform.position.y < 18)
        {
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, 20, 0);
        }
        if (gameObject.CompareTag("Motor"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
        }

    }

    int warLandID;
    private void OnEnable()
    {
        warLandID = LayerMask.NameToLayer("Area");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (gameObject.layer == warLandID)
        {
            Enemy.instance.isInTheArea = true;
            NinjaIsInTheArea?.Invoke();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.layer == warLandID)
        {
            Enemy.instance.isInTheArea = false;
            NinjaIsInTheArea?.Invoke();
        }
        if (gameObject.CompareTag("Motor"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
    public void _OnDisable()
    {
        Player.instance.gameObject.transform.SetParent(null);
    }

    public static void HurtPlayer()
    {
        HurtNinja.Invoke();
        GameManager.manager.SetHealth(10);
    }
}
