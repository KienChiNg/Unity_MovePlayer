using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private Animator animRun;
    Rigidbody2D m_rg;
    bool inGround = true;
    float horizontalMove = 0f;
    void Start()
    {
        m_rg = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Debug.Log(transform.localScale.x);
        horizontalMove = Input.GetAxisRaw("Horizontal")*runSpeed;
        animRun.SetFloat("speed",Mathf.Abs(horizontalMove));
        transform.position += Vector3.right * runSpeed * Time.deltaTime * horizontalMove;
        if ((Input.GetAxisRaw("Horizontal") > 0 && transform.localScale.x < 0)||
            Input.GetAxisRaw("Horizontal") < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y,transform.localScale.z);
        if (Input.GetAxisRaw("Vertical") > 0 && inGround)
		{
            if (transform.localScale.x < 0)
                m_rg.velocity = new Vector2(-5,5);
            else{
                m_rg.velocity = new Vector2(5,5);
            }
			animRun.SetBool("jump", true);
            inGround = false;
		}
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ground")){
            animRun.SetBool("jump", false);
            inGround = true;
        }
    }
}
