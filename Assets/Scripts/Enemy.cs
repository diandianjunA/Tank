using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����
    public float moveSpeed = 3F;
    private Vector3 bulletEulerAngles;
    private float v = -1;
    private float h;
    private System.Random random=new System.Random();
    private bool isStatic;
    
    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//��������
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    //��ʱ��
    private float timeVal;
    private float timeValChangeDirection = 4;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isStatic == false)
        {
            //����CD
            if (timeVal > 1f)
            {
                Attack();
            }
            else
            {
                timeVal += Time.deltaTime;
            }
        }
    }
    private void FixedUpdate()
    {
        if (isStatic == false)
        {
            Move();
        }
    }
    //̹�˵��ƶ�����
    private void Move()
    {
        if (timeValChangeDirection > 2)
        {
            int v1 = random.Next(0, 16);
            if (v1 > 8)
            {
                v = -1;
                h = 0;
            }
            else
            {
                switch (v1)
                {
                    case 0:
                        v = 1;
                        h = 0;
                        break;
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        h = 1;
                        v = 0;
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        h = -1;
                        v = 0;
                        break;
                }
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }
    }
    //̹�˵Ĺ�������
    private void Attack()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        timeVal = 0;
    }
    //̹�˵���������
    private void Die()
    {
        PlayerManager.Instance.playerScore++;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            timeValChangeDirection = 5;
        }
    }

    public void KeepStatic()
    {
        isStatic = true;
        Invoke("wait", 3f);
    }
    public void KeepStaticForever()
    {
        isStatic = true;
    }
    public void wait()
    {
        isStatic = false;
    }
}
