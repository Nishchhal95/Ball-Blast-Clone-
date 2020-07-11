using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BallBase : MonoBehaviour, IDamage
{
    public GameObject parent;
    public TextMeshProUGUI numberText;
    public SpriteRenderer ballSpriteRenderer;
    public int level;
    public Rigidbody2D rb;

    public bool isSpecialBall;

    public int number;
    public Color[] colors;

    public float xSpeed;

    //Color changing factor
    protected float factor;
    protected int currentColorIndex = -1;
    protected int remainingNumber;
    protected int consumedNumber;
    protected ISpawner ballSpawner;

    public abstract void Init(int startNumber, int remainingNumber, ISpawner spawner, int level);

    public virtual void onDamageRecieved(float damage)
    {
        throw new System.NotImplementedException();
    }

    //Can use this to trigger some other functions
    public virtual void onObjectDestroy()
    {
        throw new System.NotImplementedException();
    }

    protected void SetParent()
    {
        if (parent == null)
        {
            parent = this.gameObject;
        }
    }

    #region Force

    protected void AddUpwardsForce()
    {
        Vector2 force = rb.mass * Physics2D.gravity * -50;
        rb.AddForce(force);
    }

    protected void AddXForce()
    {
        xSpeed = xSpeed * -1;
        Vector2 force = new Vector2(xSpeed, 0);
        rb.AddForce(force);
    }

    #endregion
}
