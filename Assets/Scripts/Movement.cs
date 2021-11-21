using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float shotCooldown = 3f;
    [SerializeField] float shotVelocity = 500f;
    float lastShotTime;
    float NextFire;
    [SerializeField] GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // Input.GetAxis refers to Input Manager in Unity (Edit > Project Settings > Input Manager)
        // Time.deltaTime gives period of each frame (1/Fs)
        // moveSpeed is multiplier to speed up or slow down movement

        float xValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float yValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        bool fire = Input.GetButton("Jump");


        transform.Rotate(0f, yValue, 0f, Space.World); // if turning, move entire cannon
        transform.Find("Barrel").Rotate(xValue, 0f, 0f, Space.World); // if changing angle, only move barrel

        if (fire && Time.time > NextFire)
        {
            NextFire = Time.time + shotCooldown;
            Fire();
        }
        
    }
    
    void Fire()
    {
        GameObject clone = Instantiate(projectile, transform.Find("Barrel").position, transform.Find("Barrel").rotation);

        Debug.Log("Fire! " + transform.rotation.x);
        clone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * shotVelocity * clone.GetComponent<Rigidbody>().mass);
        //clone.GetComponent<Rigidbody>().AddForce(0f, shotVelocity * (1-transform.rotation.x), shotVelocity * transform.rotation.x);
        //clone.GetComponent<Rigidbody>().AddForce(transform.up * shotVelocity);
    }
}
