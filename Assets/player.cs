using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    public GameObject[] Prefabs;
    public Light Sun;
    public UnityEngine.UI.Text CurrentItemText;
    //j
    private GameObject currentPrefabObject;
    private DigitalRuby.PyroParticles.FireBaseScript currentPrefabScript;
    private int currentPrefabIndex;


    public float playerHealth;
    public float playerHealthBar;
    private float sensitivityX = 1.1F;
    private float sensitivityY = 1.1F;
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private int num = 0;
    private Vector3 velocity;
    private Quaternion originalRotation;
    // Use this for initialization
    void Start()
    {
        originalRotation = transform.localRotation;
        playerHealth = 100f;
        playerHealthBar = 100f;
        Input.gyro.enabled = true;
        Input.gyro.updateInterval = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player_life");
        if (playerHealth>=0)
        {
            float x = ((playerHealthBar - playerHealth) * 0.01f);
            playerHealthBar = playerHealth;
            player.transform.localScale -= new Vector3(x, 0, 0);
        }
        print(playerHealth);
    }

    public void damage(float dmg) {
        if (playerHealth > 0)
        {
            playerHealth -= dmg;
            if (playerHealth < 0) playerHealth = 0;
        }
        
    }

    

    void FixedUpdate()
    {
        UpdateCameraLook();
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 110, 100, 90), "ataque"))
        {
            BeginEffect(num);
 //           GameObject skeleton = GameObject.Find("skeleton");
 //           skeleton.SendMessage("decreasingHealth");

        }
        if (GUI.Button(new Rect(100, 210, 100, 90), "Change Attack" )) {
            if (num < 2) {
                num++;
            }
            else
            {
                num = 0;
            }

        }
        if (playerHealth <= 0)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 50;
            GUILayout.BeginArea(new Rect(Screen.width / 2, Screen.height / 2, 800, 300));
            GUILayout.FlexibleSpace();
            GUILayout.Label("GameOver", style);
            GUILayout.FlexibleSpace();
            GUILayout.EndArea();

        }
    }

    private void BeginEffect(int i)
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[i]);
        currentPrefabScript = currentPrefabObject.GetComponent<DigitalRuby.PyroParticles.FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<DigitalRuby.PyroParticles.FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        DigitalRuby.PyroParticles.FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<DigitalRuby.PyroParticles.FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other friendly layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FriendlyLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }



    private void UpdateCameraLook()
    {
//        velocity += (Input.acceleration*Time.fixedDeltaTime);
//        transform.Translate(velocity);
        transform.Rotate(Input.gyro.rotationRate);

    }


}
