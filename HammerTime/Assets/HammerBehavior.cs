using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
public class HammerBehavior : MonoBehaviour {

    Rigidbody rb;
    public float pushSpeed = 50f;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            rb.AddForce(transform.forward * pushSpeed, ForceMode.Impulse);
            print("Pressed");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            rb.AddForce(-transform.forward * pushSpeed, ForceMode.Impulse);
        }
        transform.localRotation = Quaternion.identity;
    }

    IEnumerator slowDownAndUp() {

        while (Time.timeScale > 0.01f) {
            Time.timeScale *= .2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
        while (Time.timeScale < 1f) {
            Time.timeScale *= 1.75f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            yield return null;
        }
        Time.timeScale = 1;
        CameraShaker.Instance.ShakeOnce(10f, 10f, 0.0f, 0.5f);
        yield break;
    }
    private void OnCollisionEnter(Collision collision) {
        print(collision.relativeVelocity.magnitude);
        if(collision.gameObject.tag == "Enemy") { 
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-collision.impulse.normalized * collision.relativeVelocity.magnitude * 2, ForceMode.Impulse);
            collision.gameObject.GetComponent<enemy>().DecreaseHealth(collision.relativeVelocity.magnitude);
            StartCoroutine(slowDownAndUp());
        }
    }
}
