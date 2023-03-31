using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _fireRate = 0.1f;
    private float _canFire = -1f;
    [SerializeField]
    private int _live = 1;
    [SerializeField]
    private int _score = 0;
    private UIManager _uIManager;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uIManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetMouseButtonDown(0) && Time.time > _canFire)
        {
            Fire();
        }
    }
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.6f, 2.6f), Mathf.Clamp(transform.position.y, -4.4f, 0), 0);
    }
    void Fire()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_bulletPrefab, transform.position + new Vector3(0,0.05f,0), Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet2")
        {
            Destroy(collision.gameObject);
            Damage();
        }
    }
    public void Damage()
    {
        _live--;
        if (_live == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uIManager.GameOver();
        }
    }
    public void AddScore(int points)
    {
        _score += points;
        _uIManager.UpdateScore(_score);
    }
}
