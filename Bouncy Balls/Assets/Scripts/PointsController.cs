using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public int _score;
    public int _rangeValue;
    public GameController _gameController;
    private float _xDirection;
    private float _yDirection;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        //Determines the direction of the point and set the velocity that will takes
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
            _gameController._totalScore += _score;
            _gameController.UpdateTexts(_gameController._scoreText, "Score: " + _gameController._totalScore);
            _gameController.UpdatePointsAmount();
            Destroy(gameObject);
        }
    }
}
