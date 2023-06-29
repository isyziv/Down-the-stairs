using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float x_speed = 5f;
    [SerializeField] float y_speed = 5f;
    [SerializeField] GameObject currentFloor;
    [SerializeField] int hp;
    [SerializeField] GameObject HpBar;
    [SerializeField] Text score;
    int score_value;
    float score_time;
    AudioSource DeathSound;
    [SerializeField] GameObject button;
    void Start()
    {
        hp = 10;
        score_value = 0;
        score_time = 0f;
        DeathSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, y_speed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, y_speed * -1 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(x_speed * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = false;
            GetComponent<Animator>().SetBool("run", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(x_speed * -1 * Time.deltaTime, 0, 0);
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<Animator>().SetBool("run", true);
        }
        else
            GetComponent<Animator>().SetBool("run", false);
        updateScore();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "normal")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = collision.gameObject;
                Debug.Log("normal");
                ModdifyHP(1);
                collision.gameObject.GetComponent<AudioSource>().Play();
            }
            GetComponent<Animator>().SetBool("hurt", false);
        }
        else if (collision.gameObject.tag == "nails")
        {
            if (collision.contacts[0].normal == new Vector2(0f, 1f))
            {
                currentFloor = collision.gameObject;
                Debug.Log("nails");
                ModdifyHP(-3);
                GetComponent<Animator>().SetBool("hurt", true);
                if (hp != 0)
                    collision.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else if (collision.gameObject.tag == "DeathLine")
        {
            Debug.Log("DeathLine");
            GetComponent<Animator>().SetBool("hurt", false);
            ModdifyHP(-10);
        }
        else if (collision.gameObject.tag == "top")
        {
            Debug.Log("top");
            Debug.Log(currentFloor.tag);
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModdifyHP(-3);
            GetComponent<Animator>().SetBool("hurt", true);
            if (hp != 0)
                collision.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<Animator>().SetBool("hurt", false);
        }
        if (hp == 0)
        {
            Die();
        }
    }
    void ModdifyHP(int num)
    {
        hp += num;
        hp = Math.Max(hp, 0);
        hp = Math.Min(hp, 10);
        UpdateHpBar();

    }
    void UpdateHpBar()
    {
        for (int i = 0; i < HpBar.transform.childCount; i++)
        {
            if (hp > i)
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                HpBar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    void updateScore()
    {
        score_time += Time.deltaTime;
        if (score_time > 2f)
        {
            score_value++;
            score_time = 0f;
            score.text = "¦a¤U" + score_value.ToString() + "¼h";
        }

    }
    void Die()
    {
        DeathSound.Play();
        Time.timeScale = 0f;
        button.SetActive(true);
    }
    public void RePlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
