using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : BallBase
{

    #region Initialization
    private void Start()
    {
        SetParent();
        rb.AddForce(new Vector2(xSpeed, 0));
    }

    public override void Init(int startNumber, int remainingNumber, ISpawner spawner, int startLevel = -1)
    {
        this.ballSpawner = spawner;

        //Setting Level for ball
        this.level = startLevel;
        if (startLevel < 0)
        {
            this.level = UnityEngine.Random.Range(Config.MinBallLevel, Config.MaxBallLevel);
        }

        float num = (startNumber - remainingNumber) / level;
        number = (int)num;
        if(num % 1 != 0)
        {
            number = (int)num + 1;
        }

        this.remainingNumber = startNumber - number;

        factor = (float)BallSpawner.MaxBallNumber / colors.Length;
        CheckColorChange();
        ChangeScale();
    }
    #endregion

    #region Methods
    public override void onDamageRecieved(float damage)
    {
        number -= (int)damage;
        if(number < 0)
        {
            onObjectDestroy();
            return;
        }

        CheckColorChange();
    }

    public override void onObjectDestroy()
    {
        Destroy(parent);

        if(level != Config.MinBallLevel)
        {
            float nextLevelnum = this.remainingNumber / 2;
            int leftNum = (int)nextLevelnum, rightNum = 0;
            if(nextLevelnum % 1 != 0)
            {
                leftNum = (int)nextLevelnum + 1;
            }

            rightNum = this.remainingNumber - leftNum;

            if(leftNum % 2 != 0 && rightNum % 2 != 0)  //if both are odd Converting both odd numbers to even
            {
                leftNum--;
                rightNum++;
            }

            this.ballSpawner.SpawnBall(this.remainingNumber, leftNum, this.level--);
            this.ballSpawner.SpawnBall(this.remainingNumber, rightNum, this.level--);
        }
    }

    void CheckColorChange()
    {
        numberText.SetText(number + "");
        int colorIndex = (int)(number / factor);
        if(colorIndex != currentColorIndex)
        {
            string log = $"number: {number} factor: {factor} colorIndex: {colorIndex}";
            currentColorIndex = colorIndex;
            Debug.Log(log);
            ballSpriteRenderer.color = colors[currentColorIndex];
        }
    }

    void ChangeScale()
    {
        float factor = Config.scaleFactor;
        float currentScale = Config.maxBallScale - (factor * (Config.MaxBallLevel - level));
        parent.transform.localScale = new Vector3(currentScale, currentScale, 1);
    }

    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == 8) //Ground
        {
            AddUpwardsForce();
        }
        else if (collision.collider.gameObject.layer == 9)  //Walls
        {
            AddXForce();
        }
    }

}
