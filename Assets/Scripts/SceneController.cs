using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Transform[] _spawnPoints;
    [SerializeField]
    private int maxEnemiesCount;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private Text _score;
    private int _scoreCounter = 0;
    
    
    private List<GameObject> _enemies = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if(_enemies.Count < maxEnemiesCount)
        {
            GameObject enemy;
            Transform point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            enemy = Instantiate(enemyPrefab) as GameObject;
            enemy.transform.position = point.position;
            enemy.transform.forward = point.forward;
            enemy.transform.Rotate(0, Random.Range(-50,50), 0);
            _enemies.Add(enemy);
            enemy.GetComponent<ReactiveTarget>().controller = this;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnemyDie(GameObject enemy)
    {
        _enemies.Remove(enemy);
        maxEnemiesCount++;
        _scoreCounter++;
        _score.text = _scoreCounter.ToString();
    }
}
