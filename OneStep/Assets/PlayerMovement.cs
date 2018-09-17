using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Day_1{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour {

        public float speed = 5f;
        public float jumpSpeed = 20f;

        Rigidbody rb;

        float chargeSpeed = 100f;
        float chargeMeter = 0;

        public enum JumpState { Grounded, InAir, Charging, Dashing}
        public JumpState jumpState = JumpState.Grounded;

        // Use this for initialization
	    void Start () {
            rb = GetComponent<Rigidbody>();
	    }

        private void OnCollisionEnter(Collision collision) {
            if (collision.transform.tag == "Floor") {
                jumpState = JumpState.Grounded;
            }
        }

        IEnumerator Charging() {
            while (jumpState == JumpState.Charging) {
                chargeMeter += Time.deltaTime * chargeSpeed;
                yield return null;
            }
        }

        // Update is called once per frame

        float timeDeltaTimeNormalizer = 1000f;
        void Update () {

            if (Input.GetKey(KeyCode.W) && jumpState != JumpState.Charging) {
                rb.AddForce(Vector3.forward * speed * Time.deltaTime * timeDeltaTimeNormalizer);
            }
            if (Input.GetKey(KeyCode.S) && jumpState != JumpState.Charging) {
                rb.AddForce(-Vector3.forward * speed * Time.deltaTime * timeDeltaTimeNormalizer);
            }
            if (Input.GetKey(KeyCode.A) && jumpState != JumpState.Charging) {
                rb.AddForce(-Vector3.right * speed * Time.deltaTime * timeDeltaTimeNormalizer);
            }
            if (Input.GetKey(KeyCode.D) && jumpState != JumpState.Charging) {
                rb.AddForce(Vector3.right * speed * Time.deltaTime * timeDeltaTimeNormalizer);
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpState == JumpState.Grounded) {
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                jumpState = JumpState.InAir;
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpState == JumpState.InAir) {
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
                jumpState = JumpState.Charging;
                StartCoroutine(Charging());
                return;
            }

            if (Input.GetKeyUp(KeyCode.Space) && jumpState == JumpState.Charging) {
                rb.useGravity = true;
                jumpState = JumpState.Dashing;
                rb.AddForce(Vector3.down * chargeMeter, ForceMode.Impulse);
                chargeMeter = 0f;
                StopCoroutine(Charging());
                return;
            }

        }
    }
}

