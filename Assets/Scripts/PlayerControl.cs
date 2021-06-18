using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody2D PlayerRigid;
    [SerializeField] private Transform ArrowTransform;
    [SerializeField] private GameObject Arrow;

    [SerializeField] private float JumpHeight = 5.0f;

    private void Start()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        ArrowTransform = Arrow.GetComponent<Transform>();
    }

    public void PlayerJump()
    {
        PlayerRigid.AddForce(ArrowTransform.transform.up * JumpHeight, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Equals("RightWall"))
            PlayerRigid.AddForce(Vector2.left * 3.0f, ForceMode2D.Impulse);
        else if (collision.gameObject.name.Equals("LeftWall"))
            PlayerRigid.AddForce(Vector2.right * 3.0f, ForceMode2D.Impulse);
    }
}
