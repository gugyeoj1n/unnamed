using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public float rotSpeed;
    private Rigidbody rigid;
    private Animator animator;
    private Transform character;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>( );
        animator = transform.GetChild( 0 ).GetComponent<Animator>( );
        character = transform.GetChild( 0 );
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>( );
        SetAnimation(  );
    }

    private void SetAnimation( )
    {
        if(inputVec.x != 0 || inputVec.y != 0)
            animator.SetBool( "isWalking", true );
        else
            animator.SetBool( "isWalking", false );
    }

    private void FixedUpdate()
    {
        if(inputVec.x == 0 && inputVec.y == 0)
            return;
        
        Vector3 input = new Vector3( inputVec.x, 0, inputVec.y );
        Vector3 moveDirection = RotateVector( input, 45 );
        Vector3 nextVec = moveDirection * speed * Time.deltaTime;

        character.forward = Vector3.Lerp( character.forward, moveDirection, rotSpeed * Time.deltaTime );
        rigid.MovePosition( rigid.position + nextVec );
    }

    private Vector3 RotateVector(Vector3 vector, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad;
        float sin = Mathf.Sin(radians);
        float cos = Mathf.Cos(radians);

        float tx = vector.x;
        float tz = vector.z;

        vector.x = (cos * tx) - (sin * tz);
        vector.z = (sin * tx) + (cos * tz);

        return vector;
    }
}