using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    public int _score;
    public int _maxScoreValuePosible;
    public GameController _gameController;
    public Text _scoreText;
    private int _rangeValue;
    private float _xDirection;
    private float _yDirection;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        //Set the number to the text component of the game object
        _score = Random.Range(0, _maxScoreValuePosible);
        _scoreText = GetComponentInChildren<Canvas>().GetComponentInChildren<Text>();
        _scoreText.text = _score.ToString();
        //Determines the direction of the point and set the velocity that will takes
        _rangeValue = Random.Range(20, 40);
        _xDirection = Random.Range(-_rangeValue, _rangeValue);
        _yDirection = Random.Range(-_rangeValue, _rangeValue);
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_xDirection, _yDirection);
    }

    void OnMouseOver()
    {
        //Detect if the point is clicked with the game active
        if (Input.GetMouseButtonDown(0) && !_gameController._gameOver)
        {
            //Set the score, update the text component and destroy the gameObject clicked
            if (_gameController.WasDestroyedPointBefore(_score))
                _gameController._totalScore -= 5;
            else
                _gameController._totalScore += 10;
            _gameController.UpdateTexts(_gameController._scoreText, "Score: " + _gameController._totalScore);
            _gameController.UpdatePointsAmount();
            Destroy(gameObject);
        }
    }
}
