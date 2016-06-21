using UnityEngine;
using System.Collections;

public class ToogleTest : MonoBehaviour {
	Vector3 acel;
    Vector3 original;
    GameObject enemy;
	// Use this for initialization
	void Start () {
        enemy = GameObject.Find("Cube");
        original = enemy.transform.position;
        acel = Input.acceleration;
	}
	// Update is called once per frame
	void Update () {
        float x = enemy.transform.position.x;
        float y = enemy.transform.position.y;
        float z = enemy.transform.position.z;
        if (acel.x!=Input.acceleration.x)
        {
            x += (Input.acceleration.x - acel.x)*5f;
            acel.x = Input.acceleration.x;

        }
        if (acel.y != Input.acceleration.y)
        {
            y += (Input.acceleration.y - acel.y)*5f;
            acel.y = Input.acceleration.y;
        }
        if (acel.z != Input.acceleration.z)
        {
            z += (Input.acceleration.z - acel.z) * 5f;
            acel.z = Input.acceleration.z;
        }
        Vector3 t = new Vector3(x, y, z);
		enemy.transform.position = t;

	}

    void OnGUI()
    {   
        if (GUI.Button(new Rect(490, 110, 600, 350), "reiniciar posi"))
        {
            enemy.transform.position = original;
        }
        GUIStyle a = new GUIStyle();
        a.fontSize = 50;
        a.normal.textColor = Color.white;
        GUILayout.Label("acel: "+Input.acceleration,a);
        GUILayout.Label("enemy pos: " + enemy.transform.position, a);
        GUILayout.Label("RRate " + Input.gyro.rotationRate, a);
        GUILayout.Label("RRU " + Input.gyro.rotationRateUnbiased, a);
        GUILayout.Label("userAcele " + Input.gyro.userAcceleration, a);
        GUILayout.Label("Intervalo " + Input.gyro.updateInterval, a);
        GUILayout.Label("G " + Input.gyro.gravity, a);
    }
}
