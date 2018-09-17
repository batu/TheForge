using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakZone : MonoBehaviour {


    public GameObject floor;
    public GameObject glass;
    public GameObject replacementFloor;
    float replacementFloorOffset = 30f;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            Instantiate(glass, transform.position, transform.rotation);

            GameObject floor_1 = Instantiate(replacementFloor, transform.position, transform.rotation);
            floor_1.transform.position += new Vector3(transform.lossyScale.x / 2f + replacementFloorOffset, 0f, 0f);

            GameObject floor_2 = Instantiate(replacementFloor, transform.position, transform.rotation);
            floor_2.transform.position += new Vector3(-transform.lossyScale.x / 2f - replacementFloorOffset, 0f, 0f);

            GameObject floor_3 = Instantiate(replacementFloor, transform.position, transform.rotation);
            floor_3.transform.position += new Vector3(0f, 0f, transform.lossyScale.z / 2f + replacementFloorOffset);

            GameObject floor_4 = Instantiate(replacementFloor, transform.position, transform.rotation);
            floor_4.transform.position += new Vector3(0f, 0f, -transform.lossyScale.z / 2f - replacementFloorOffset);

            Destroy(floor);
            Destroy(gameObject, 10f);
            StartCoroutine(MoveCam());
        }
    }

    IEnumerator MoveCam() {
        print("Started!");
        float startTime = Time.time;
        float journeyTime = 2.0f;
        Vector3 startPosition = Camera.main.transform.position;
        Vector3 stopPosition = startPosition - new Vector3(0f, 100f, 0f);
        float fracComplete = 0f;
        while (fracComplete < 1f) {
            fracComplete = (Time.time - startTime) / journeyTime;
            Camera.main.transform.position = Vector3.Slerp(startPosition, stopPosition, fracComplete);
            yield return null;
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
