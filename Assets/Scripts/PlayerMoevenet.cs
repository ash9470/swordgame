using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Cinemachine;
using Photon.Realtime;

public class PlayerMoevenet : MonoBehaviour
{
    public static PlayerMoevenet instance;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private CinemachineFreeLook cinemachineFreeLook;

    private Playercontroll playerinput;
    private PhotonView photonView;


    private void Awake()
    {
        playerinput = new Playercontroll();
        cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        photonView = GetComponent<PhotonView>();
        instance = this;
        if (!GetComponent<PhotonView>().IsMine) return;
        else
        cinemachineFreeLook.Follow = gameObject.transform;
        cinemachineFreeLook.LookAt = gameObject.transform;
    }

    private void OnEnable()
    {

        playerinput.Enable();

    }

    private void OnDisable()
    {
        playerinput.Disable();
    }

    void Update()
    {
        if (!GetComponent<PhotonView>().IsMine) return;
        
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movementinput = playerinput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementinput.x, 0f, movementinput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero && photonView.IsMine)
        {
            animator.SetBool("IsWalking", true);
            gameObject.transform.forward = move;
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    public void Attacks1()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
            animator.Play("Attack_01");
    }
    public void Attacks2()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
            animator.Play("Attack_02");
    }
    public void Defence()
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
            animator.Play("Cover");
    }
























































    //private CharacterController _controller;

    //[SerializeField]
    //private float _playerSpeed = 5f;

    //[SerializeField]
    //private float _rotationSpeed = 1f;

    //[SerializeField]
    //private Camera _followCamera;

    //private Vector3 _playerVelocity;
    //private bool _groundedPlayer;

    //[SerializeField]
    //private float _jumpHeight = 1.0f;
    //[SerializeField]
    //private float _gravityValue = -9.81f;

    //private void Start()
    //{
    //    _controller = GetComponent<CharacterController>();
    //    animator = GetComponent<Animator>();
    //}

    //private void FixedUpdate()
    //{
    //    _groundedPlayer = _controller.isGrounded;
    //    if (_groundedPlayer && _playerVelocity.y < 0)
    //    {
    //        _playerVelocity.y = 0f;
    //    }

    //    float horizontalInput = Input.GetAxis("Horizontal");
    //    float verticalInput = Input.GetAxis("Vertical");

    //    Vector3 movementInput = Quaternion.Euler(0, _followCamera.transform.eulerAngles.y, 0) * new Vector3(horizontalInput, 0, verticalInput);
    //    Vector3 movementDirection = movementInput.normalized;

    //    _controller.Move(movementDirection * _playerSpeed * Time.deltaTime);

    //    if (movementDirection != Vector3.zero)
    //    {
    //        animator.SetBool("IsWalking", true);
    //        //Debug.Log("iswalking");
    //        Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, _rotationSpeed * Time.deltaTime);

    //    }
    //    else
    //    {
    //        animator.SetBool("IsWalking", false);
    //    }

    //    if (Input.GetButtonDown("Jump") && _groundedPlayer)
    //    {
    //        _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
    //    }

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        animator.Play("Attack_01");
    //    }
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        animator.Play("Attack_02");
    //    }
    //    if (Input.GetKeyDown((KeyCode)32))
    //    {
    //        animator.Play("Cover");
    //    }



    //    _playerVelocity.y += _gravityValue * Time.deltaTime;
    //    _controller.Move(_playerVelocity * Time.deltaTime);

    //}
}
