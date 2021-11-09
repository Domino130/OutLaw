using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffects;
    private Material matWhite;
    private Material matDefault;
    private Object enemyRef;
    SpriteRenderer sr;
    

    public void Start()
    {
        enemyRef = Resources.Load("Enemy");
        sr = GetComponent<SpriteRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material;
        matDefault = sr.material;       
    }
    public void TakeDamage (int damage)
    {
        health -= damage;

            if (health <= 0)
        {
            Die();
        }
        
    }
    void Die()
    {
        Instantiate(deathEffects, transform.position, Quaternion.identity);
        sr.enabled = false;
        gameObject.SetActive(false);
        Invoke("Respawn", 5);
    }

    void ResetMaterial()
    {
        sr.material = matDefault;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            sr.material = matWhite;
            Invoke("ResetMaterial", .1f);
        }
    }

    void Respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef);
        enemyClone.transform.position = transform.position;


        Destroy(gameObject);
    }
}
