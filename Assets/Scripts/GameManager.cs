using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    #region UI event
    public delegate void HaveChanged(string s);
    public event HaveChanged WhenChanging;
    #endregion

    private void OnEnable()
    {
        manager = this;
    }
    int score;
    public int GetScore() { return score; }
    public void SetScore(int plus)
    {
        score += plus;
        WhenChanging?.Invoke("ssv");
    }
    int health = 100;
    public int GetHealth() { return health; }
    public void SetHealth(int minus)
    {
        if (health - minus == 0)
        {
            WhenChanging?.Invoke("nj");
            WhenChanging?.Invoke("YOU LOST");
        }
        health -= minus;
        WhenChanging?.Invoke("shv");
    }
    int enemyHealth = 100;
    public int GetEnemyHealth() { return enemyHealth; }
    public void SetEnemyHealth(int minus)
    {
        if (score == 30)
        {
            if (enemyHealth - minus == 0)
            {
                WhenChanging?.Invoke("td");
                WhenChanging?.Invoke("YOU WON");
            }
            enemyHealth -= minus;
            WhenChanging?.Invoke("sehv");
        }
    }
    public bool ninjaAttackFlag;

}
