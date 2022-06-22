using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //����
    public float moveSpeed = 3F;
    private Vector3 bulletEulerAngles;
    private float timeVal;
    private float defendTimeVal = 5;
    private bool isDefended = true;
    //����
    private SpriteRenderer sr;
    public Sprite[] tankSprite;//��������
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendPrefab;
    public AudioClip[] tankAudio=new AudioClip[2];
    public AudioSource moveAudio;
    private void Awake()
    {
        sr= GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        moveAudio.clip = tankAudio[1];
    }

    // Update is called once per frame
    void Update()
    {
        //�޵�״̬
        if (isDefended)
        {
            defendPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended= false;
                defendPrefab.SetActive(false);
            }
        }
        //����CD
        if(timeVal > 0.4f)
        {
            Attack();
        }
        else
        {
            timeVal += Time.deltaTime;
        }
        if (PlayerManager.Instance.isDefeated == true)
        {
            Destroy(gameObject);
            PlayerManager.Instance.gameOver.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (Move() != 0)
        {
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.Stop();
        }
    }
    //̹�˵��ƶ�����
    private float Move()
    {
        float v = Input.GetAxisRaw("Vertical");//��ֱ����
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
        if (v != 0)
        {
            return v;
        }
        float h = Input.GetAxisRaw("Horizontal");//ˮƽ����
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
        if (h != 0)
        {
            return h;
        }
        else
        {
            return 0;
        }
    }
    //̹�˵Ĺ�������
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles+bulletEulerAngles));
            timeVal = 0;
        }
    }
    //̹�˵���������
    private void Die()
    {
        if (isDefended)
        {
            return;
        }
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
        PlayerManager.Instance.isDeath = true;
    }
    private void StartDefend()
    {
        isDefended = true;
        defendTimeVal = 5;
    }
    private void Accelerate()
    {
        moveSpeed = 10;
        Invoke("normalize", 5);
    }
    private void normalize()
    {
        moveSpeed = 3;
    }
}
