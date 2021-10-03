using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TailGeneration))]
[RequireComponent(typeof(SnakeInput))]

public class Snake : MonoBehaviour
{
    private List<Segment> _tail;
    private TailGeneration _tailGeneration;

    [SerializeField] private SnakeHead _head;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpeed;
    private SnakeInput _input;

    private void Awake()
    {
        _tailGeneration = GetComponent <TailGeneration>();
        _input = GetComponent<SnakeInput>();
        _tail = _tailGeneration.Generate();
    }

    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.deltaTime);

        _head.transform.up = _input.GetDistanceToClick(_head.transform.position);
    }

    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;
        foreach (var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpeed * Time.fixedDeltaTime);
            previousPosition = tempPosition;
        }
        _head.Move(nextPosition);
        
    }
}
