using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _enemyPrefab = new GameObject[16];
    [SerializeField]
    private GameObject _enemyContainer;
    public int a = 4;
    public int lengths = 7;
    public int widths = 3;
    public int heights = 5;
    public int n = 5;
    [SerializeField]
    private float _speed = 33.0f;
    [SerializeField]
    private float formationSpacing = 0.5f;
    Vector3 initialPos = new Vector3(0, 10, 0);

    Vector3[] newPos = new Vector3[16];
    Vector3[] newPos1 = new Vector3[16];
    Vector3[] newPos2 = new Vector3[16];
    Vector3[] newPos3 = new Vector3[16];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnEnemySquare());
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnEnemyDimond());
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnEnemyTriangle());
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(SpawnEnemyRectangle());
    }
    IEnumerator SpawnEnemySquare()
    {
        int index = 0;
        for (int row = 0; row < a; row++)
        {
            for (int col = 0; col < a; col++)
            {
                if (_enemyPrefab[index] != null)
                {
                    Vector3 posToSpawn = new Vector3((row + (-1.5f)) * formationSpacing, (col + 2.0f) * formationSpacing, 0);
                    newPos[index] = posToSpawn;
                    _enemyPrefab[index] = Instantiate(_enemyPrefab[index], initialPos, Quaternion.identity);
                    _enemyPrefab[index].transform.parent = _enemyContainer.transform;
                    StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                    index++;
                }
                else
                {
                    index++;
                }
            }
        }
        yield return null;
    }
    IEnumerator SpawnEnemyDimond()
    {
        int index = 0;
        for (int height = 0; height < heights; height++)
        {
            for (int length = 0; length < lengths; length++)
            {
                if ((height == 0) && (length == (lengths - 1) / 2) || (height == heights - 1) && (length == (lengths - 1) / 2))
                {
                    if (_enemyPrefab[index] != null)
                    {
                        Vector3 posToSpawn = new Vector3(((length - 3.0f) ) * formationSpacing, (height+2.0f) * formationSpacing, 0);
                        newPos1[index] = posToSpawn;
                        _enemyPrefab[index].transform.position = newPos[index];
                        StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
                if (height % 2 == 1 && (length != 0) && (length != lengths - 1) && (length != (lengths - 1) / 2))
                {
                    if (_enemyPrefab[index] != null)
                    {
                        Vector3 posToSpawn = new Vector3((length - 3.0f) * formationSpacing, (height+2.0f) * formationSpacing, 0);
                        newPos1[index] = posToSpawn;
                        _enemyPrefab[index].transform.position = newPos[index];
                        StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
                if ((height == (heights - 1) / 2) && (length != (lengths - 1) / 2))
                {
                    if (_enemyPrefab[index] != null)
                    {
                        Vector3 posToSpawn = new Vector3((length -3.0f) * formationSpacing, (height+2.0f) * formationSpacing, 0);
                        newPos1[index] = posToSpawn;
                        _enemyPrefab[index].transform.position = newPos[index];
                        StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
        yield return null;
    }
    IEnumerator SpawnEnemyTriangle()
    {
        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = n - 1 - i; j <= n - 1 + i; j++)
            {
                if ((i == n - 1) || (j == n + i - 1) || (j == n - i - 1))
                {
                    if (_enemyPrefab[index] != null)
                    {
                        Vector3 posToSpawn = new Vector3(((j - 0.5f) + (-3.5f)) * formationSpacing, (6.5f - i) * formationSpacing, 0);
                        newPos2[index] = posToSpawn;
                        _enemyPrefab[index].transform.position = newPos1[index];
                        StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
        yield return null;
    }
    IEnumerator SpawnEnemyRectangle()
    {
        int index = 0;
        for (int width = 0; width < widths; width++)
        {
            for (int lenght = 0; lenght < lengths; lenght++)
            {
                if ((width == widths - 1) || (lenght == lengths - 1) || (width == 0) || (lenght == 0))
                {
                    if (_enemyPrefab[index] != null)
                    {
                        Vector3 posToSpawn = new Vector3((lenght + (-3.0f)) * formationSpacing, (width + 3.0f) * formationSpacing, 0);
                        newPos3[index] = posToSpawn;
                        _enemyPrefab[index].transform.position = newPos2[index];
                        StartCoroutine(MoveEnemy(_enemyPrefab[index], posToSpawn));
                        index++;
                    }
                    else
                    {
                        index++;
                    }
                }
            }
        }
        yield return null;
    }
    private IEnumerator MoveEnemy(GameObject enemy, Vector3 targetPos)
    {
        while (enemy.transform.position != targetPos && enemy)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, _speed * Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        var obj = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in obj)
        {
            Destroy(enemy);
        }
    }
}
