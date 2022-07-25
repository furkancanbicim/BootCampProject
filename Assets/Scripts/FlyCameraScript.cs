using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCameraScript : MonoBehaviour
{

    //Variables for the rotational speed of the planets and the rotational speed of the sun.
    [SerializeField] float rotateSpeed;
    //Game manager script for get management.
    [SerializeField] GameManager gameManager;
    Transform targetPlanet;
    [Header("Constants")]

    //unity controls and constants input
    public float AccelerationMod;
    public float XAxisSensitivity;
    public float YAxisSensitivity;
    public float DecelerationMod;

    float _time;

    [Space]

    [Range(0, 89)] public float MaxXAngle = 60f;

    [Space]

    public float MaximumMovementSpeed = 1f;

    [Header("Controls")]

    public KeyCode Forwards = KeyCode.W;
    public KeyCode Backwards = KeyCode.S;
    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Up = KeyCode.Q;
    public KeyCode Down = KeyCode.E;

    private Vector3 _moveSpeed;
    Vector3 _position;
    Vector3 distanceFromPlanet;
    bool isIdle;

    private void Start()
    {
        targetPlanet = null;
        _position = transform.position;
        _time = 0f;
        _moveSpeed = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnEnable()
    {
        EventManager.planetSelected += PlanetSelected;
        EventManager.planetUnSelected += PlanetUnSelected;
    }
    void OnDisable()
    {
        EventManager.planetSelected -= PlanetSelected;
        EventManager.planetUnSelected -= PlanetUnSelected;
    }

    void PlanetSelected(Transform planetTransform,Vector3 distance)
    {
        GetComponent<Camera>().enabled = false;
        Debug.Log(transform.GetChild(0).GetComponentInParent<Camera>().name);
        transform.GetChild(0).GetComponentInParent<Camera>().enabled = true;
        targetPlanet = planetTransform;
        distanceFromPlanet = distance;
        Cursor.lockState = CursorLockMode.None;
    }
    void PlanetUnSelected()
    {
        transform.GetChild(0).GetComponentInParent<Camera>().enabled = false;
        GetComponent<Camera>().enabled = true;
        
        targetPlanet = null;
    }

    bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }
    private void Update()
    {
        
       if(targetPlanet!=null)
       {
            
            transform.LookAt(targetPlanet);
            transform.position = targetPlanet.position + distanceFromPlanet;
        }
            if (_time <= 0 && targetPlanet==null)
            {
                transform.LookAt(gameManager.sun.transform.position);
                if (_time > -1)
                {
                    transform.position = _position;
                    _time = -1;
                }
                transform.RotateAround(gameManager.sun.transform.position, Vector3.up, rotateSpeed * Time.deltaTime);
            }
            if (!HasMouseMoved() && _time > 0)
            {
                _time -= Time.deltaTime;
            }
            else if (_time <= 0 && (HasMouseMoved() || HandleKeyInput() != Vector3.zero))
            {
                _time = 3f;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
            if(targetPlanet!=null)
            Cursor.lockState = CursorLockMode.Locked;
            EventManager.planetUnSelected?.Invoke();
            }
            if ((HasMouseMoved() || HandleKeyInput() != Vector3.zero)&&targetPlanet==null)
            {
                HandleMouseRotation();

                var acceleration = HandleKeyInput();

                _moveSpeed += acceleration;

                HandleDeceleration(acceleration);

                // clamp the move speed
                if (_moveSpeed.magnitude > MaximumMovementSpeed)
                {
                    _moveSpeed = _moveSpeed.normalized * MaximumMovementSpeed;
                }

                transform.Translate(_moveSpeed);
            }
        
       
       
    }

    private Vector3 HandleKeyInput()
    {

        var acceleration = Vector3.zero;

        //key input detection
        if (Input.GetKey(Forwards))
        {
            acceleration.z += 1;
        }

        if (Input.GetKey(Backwards))
        {
            acceleration.z -= 1;
        }

        if (Input.GetKey(Left))
        {
            acceleration.x -= 1;
        }

        if (Input.GetKey(Right))
        {
            acceleration.x += 1;
        }

        if (Input.GetKey(Up))
        {
            acceleration.y += 1;
        }

        if (Input.GetKey(Down))
        {
            acceleration.y -= 1;
        }

        return acceleration.normalized * AccelerationMod;
    }

    private float _rotationX;

    private void HandleMouseRotation()
    {
        //mouse input
        var rotationHorizontal = XAxisSensitivity * Input.GetAxis("Mouse X");
        var rotationVertical = YAxisSensitivity * Input.GetAxis("Mouse Y");

        //applying mouse rotation
        // always rotate Y in global world space to avoid gimbal lock
        transform.Rotate(Vector3.up * rotationHorizontal, Space.World);

        var rotationY = transform.localEulerAngles.y;

        _rotationX += rotationVertical;
        _rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

        transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
    }

    private void HandleDeceleration(Vector3 acceleration)
    {
        //deceleration functionality
        if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0))
        {
            if (Mathf.Abs(_moveSpeed.x) < DecelerationMod)
            {
                _moveSpeed.x = 0;
            }
            else
            {
                _moveSpeed.x -= DecelerationMod * Mathf.Sign(_moveSpeed.x);
            }
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0))
        {
            if (Mathf.Abs(_moveSpeed.y) < DecelerationMod)
            {
                _moveSpeed.y = 0;
            }
            else
            {
                _moveSpeed.y -= DecelerationMod * Mathf.Sign(_moveSpeed.y);
            }
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0))
        {
            if (Mathf.Abs(_moveSpeed.z) < DecelerationMod)
            {
                _moveSpeed.z = 0;
            }
            else
            {
                _moveSpeed.z -= DecelerationMod * Mathf.Sign(_moveSpeed.z);
            }
        }
    }
}
