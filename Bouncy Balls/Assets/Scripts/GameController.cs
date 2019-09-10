using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject _point;
    public GameObject _10points;
    public GameObject _20points;
    public Text _scoreText;
    public Text _timeText;
    public Text _gameOverText;
    public int _totalScore;
    public bool _gameOver;
    private int _randomGenerator;
    private int _pointsAmount;
    private float _positionX;
    private float _positionY;
    private float _time;

    void Start()
    {
        _time = 60;
        _pointsAmount = 0;
        //Update the score text to zero points
        _totalScore = 0;
        UpdateTexts(_scoreText, "Score: " + _totalScore);
        //Spawn 20 random points Game Objects
        int i;
        for(i = 0; i < 20; i++)
        {
            //Set the random value to determine which point game object will be spawned
            _randomGenerator = Random.Range(1,5);
            //Set the starting positions of the point game object
            _positionX = Random.Range(-7, 7);
            _positionY = Random.Range(-3.2f, 3.2f);
            GameObject _instatiatedPoint;
            //Instanciate the game object and attach this script to the points controller script
            if(_randomGenerator == 1)
            {
                _instatiatedPoint = Instantiate(_20points, new Vector2(_positionX, _positionY), Quaternion.identity);
            }    
            else if(_randomGenerator == 2)
            {
                _instatiatedPoint = Instantiate(_10points, new Vector2(_positionX, _positionY), Quaternion.identity);
            }  
            else
            {
                _instatiatedPoint = Instantiate(_point, new Vector2(_positionX, _positionY), Quaternion.identity);
            }
            _instatiatedPoint.GetComponent<PointsController>()._gameController = this;
            _pointsAmount++;
        }
    }

    void Update()
    {
        //Update Time
        if(_time > 0 && !_gameOver)
        {
            UpdateTime();
        }
        //If time's out or player hit all point set the game over screen
        if(_pointsAmount <= 0 || _time <= 0)
        {
            SetGameOver(true);
        }
        //If game over text is active the player can press R button to restart game
        if (_gameOverText.IsActive() && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Player can quit whenever it wants to
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }

    void SetGameOver(bool _gameOver)
    {
        _gameOverText.gameObject.SetActive(_gameOver);
        this._gameOver = _gameOver;
    }

    void UpdateTime()
    {
        _time -= Time.deltaTime;
        UpdateTexts(_timeText, "Time: " + (int)_time);
    }

    public void UpdateTexts(Text _textComponent, string _text)
    {
        _textComponent.text = _text;
    }

    public void UpdatePointsAmount()
    {
        _pointsAmount--;
    }
}
