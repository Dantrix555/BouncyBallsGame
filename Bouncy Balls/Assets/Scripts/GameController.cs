using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject _point;
    public Text _scoreText;
    public Text _timeText;
    public Text _gameOverText;
    public int _totalScore;
    public bool _gameOver;
    private int _randomGenerator;
    private int _pointsAmount;
    private List<int> _remainingPoints;
    private float _positionX;
    private float _positionY;
    private float _time;

    void Start()
    {
        _time = 60;
        _pointsAmount = 20;
        _remainingPoints = new List<int>();
        //Update the score text to zero points
        _totalScore = 0;
        UpdateTexts(_scoreText, "Score: " + _totalScore);
        //Spawn 20 random points Game Objects
        int i;
        for(i = 0; i < _pointsAmount; i++)
        {
            //Set the starting positions of the point game object
            _positionX = Random.Range(-7, 7);
            _positionY = Random.Range(-3.2f, 3.2f);
            GameObject _instatiatedPoint;
            _instatiatedPoint = Instantiate(_point, new Vector2(_positionX, _positionY), Quaternion.identity);
            _instatiatedPoint.GetComponent<PointsController>()._gameController = this;
            _instatiatedPoint.GetComponent<PointsController>()._maxScoreValuePosible = _pointsAmount;
            _remainingPoints.Add(i);
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

    public bool WasDestroyedPointBefore(int _score)
    {
        int i;
        for(i = 0; i < _pointsAmount; i++)
        {
            if(_score == _remainingPoints[i])
            {
                _remainingPoints.RemoveAt(i);
                return false;
            }
        }
        return true;
    }
}
