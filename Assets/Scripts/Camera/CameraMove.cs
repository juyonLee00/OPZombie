using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Transform _target;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _minX, _minY, _maxX, _maxY;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_target.position.x, _target.position.y, transform.position.z), 
                            _speed * Time.deltaTime);
        
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minX, _maxX), 
        //                                Mathf.Clamp(transform.position.y, _minY, _maxY),
        //                                transform.position.z);
    }
}
