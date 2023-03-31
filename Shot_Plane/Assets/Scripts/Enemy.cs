using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int _live = 5;
    private Player _player;
    [SerializeField]
    private GameObject _bulletPrefab;
    [SerializeField]
    private float _spawnTimer;
    private float _spawnMax = 10.0f;
    private float _spawnMin = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = Random.Range(_spawnMin, _spawnMax);
        _player = GameObject.Find("player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            Fire();
        }
    }
    void Fire()
    {
        Instantiate(_bulletPrefab, transform.position + new Vector3(0, -0.05f, 0), Quaternion.identity);
        _spawnTimer = Random.Range(_spawnMin, _spawnMax);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                _player.AddScore(1);
            }
            Destroy(this.gameObject);
        }
        if(collision.tag == "Bullet1")
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
            Destroy(this.gameObject);
            if (_player != null)
            {
                _player.AddScore(1);
            }
        }
    }
}
