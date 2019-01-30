using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
 public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour {

    public float speed;
    private Rigidbody rb;
    public float tilt;
    public Boundary boundary;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private AudioSource audioSource;

    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (/*Input.GetButton("Fire1") || */ Input.GetKeyDown("z" ) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
            
        }
        //Instantiate(object,postion,rotation)
        

    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0.0f, v);
        rb.velocity = movement * speed;

        //if (h != 0 || v != 0)
        //{
        //    //transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        //    GetComponent<Rigidbody>().AddForce(new Vector3(h, 0, v) * speed);
        //    transform.eulerAngles = new Vector3(0, 15 * h, 0);


        //}
        rb.position = new Vector3
            (Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
	}
   
 }

