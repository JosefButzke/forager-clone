using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public Item item;
    private float startYposition;
    private Rigidbody2D rigidyBody;
    private bool isActive;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive && transform.position.y < startYposition - (Random.Range(0.2f, 0.6f)))
        {
            rigidyBody.gravityScale = 0;
            rigidyBody.velocity = Vector2.zero;
            col.enabled = true;
        }
    }

    void Active(int dir)
    {
        rigidyBody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        startYposition = transform.position.y;
        rigidyBody.gravityScale = 1.8f;
        rigidyBody.AddForce(Vector2.up * 200 + Vector2.right * (Random.Range(20,35) * dir));
        isActive = true;
    }
}
