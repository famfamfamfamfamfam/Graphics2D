using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI gameOver;
    void REWrite(string stt)
    {
        if (stt == "YOU LOST" || stt == "YOU WON")
            DontDestroy.instance.SetNoti(stt);
    }

    [SerializeField]
    Slider coinCount;
    void SetScoreValue(string ssv)
    {
        if (ssv == "ssv")
            coinCount.value = GameManager.manager.GetScore();
    }

    [SerializeField]
    Slider ninjaHealth;
    void SetHealthValue(string shv)
    {
        if (shv == "shv")
            ninjaHealth.value = GameManager.manager.GetHealth();
    }

    [SerializeField]
    Slider enemyHealth;
    void SetEnemyHealthyValue(string sehv)
    {
        if (sehv == "sehv")
            enemyHealth.value = GameManager.manager.GetEnemyHealth();
    }

    private void Start()
    {
        GameManager.manager.WhenChanging += REWrite;
        GameManager.manager.WhenChanging += SetScoreValue;
        GameManager.manager.WhenChanging += SetHealthValue;
        GameManager.manager.WhenChanging += SetEnemyHealthyValue;
        gameOver.text = DontDestroy.instance.GetNoti();
        StartCoroutine(DisplayNoti());
        SetScoreValue("ssv");
        SetHealthValue("shv");
        SetEnemyHealthyValue("sehv");
    }

    IEnumerator DisplayNoti()
    {
        gameOver.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        gameOver.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        GameManager.manager.WhenChanging -= REWrite;
        GameManager.manager.WhenChanging -= SetScoreValue;
        GameManager.manager.WhenChanging -= SetHealthValue;
        GameManager.manager.WhenChanging -= SetEnemyHealthyValue;

    }
}
