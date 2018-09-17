using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {

    GameObject player;
    public float speed = 2.5f;
    public float maxSpeed = 100f;
    public float health = 500;
    bool alive = true;

    float armor = 50f;
    float maxHealth;
    Rigidbody rb;
    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        maxHealth = health;
    }
	
    public void DecreaseHealth(float decreaseAmount) {
        if(decreaseAmount > armor)
            health -= decreaseAmount;
        if (health < 0)
            alive = false;
        float ratio = (maxHealth - health) / maxHealth;
        Color newColor = Color.Lerp(Color.white, Color.red, ratio);
        GetComponent<Renderer>().material.color = newColor;
    }
	// Update is called once per frame
	void Update () {
        if (alive) { 
            Vector3 direction = player.transform.position - transform.position;
            rb.AddForce(direction * speed);
            if (rb.velocity.magnitude > maxSpeed) {
                rb.velocity = rb.velocity * 0.99f;
            }
        }
    }
}
