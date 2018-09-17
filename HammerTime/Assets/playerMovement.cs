using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {
    public GameObject wastedText;
    Rigidbody rb;
    public float speed = 10f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}


    IEnumerator slowDown() {

        wastedText.SetActive(true);
        while(Time.timeScale > 0.01f) { 
            Time.timeScale *= .994f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
        Time.timeScale = 0;
        yield break;
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Enemy" && collision.impulse.magnitude > 50f) {
            print(collision.gameObject.name);
            rb.AddForce(collision.impulse * 10f, ForceMode.Impulse);
            StartCoroutine(slowDown());
        }
            
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.W)) {
            rb.AddForce(Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            rb.AddForce(-Vector3.forward * speed);
        }

        if (Input.GetKey(KeyCode.A)) {
            rb.AddForce(-Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.D)) {
            rb.AddForce(Vector3.right * speed);
        }
    }
}
