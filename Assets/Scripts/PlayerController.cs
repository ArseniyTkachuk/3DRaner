using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;


public class PlayerController : MonoBehaviour
{
    private Transform player;
    private Animator animator;
    private CharacterController controller;
    private CapsuleCollider capsuleCollider;
    private Vector3 dir;
    private int index;
    private int Coints;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private Text CointTexst;
    [SerializeField] private GameObject losePanel;

    private int lineToMove = 1;
    private float MaxSpeed = 100f;
    public float lineDistance = 4;
    private void Awake()
    {
        
    }

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("IsStart");
        losePanel.SetActive(false);
        controller = GetComponent<CharacterController>();
        StartCoroutine(SpeedIncrease());
        Coints = PlayerPrefs.GetInt("Coints");
        CointTexst.text = Coints.ToString();
    }
    private void Update()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
            {
                lineToMove++;
            }
        }
        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
            {
                lineToMove--;
            }
        }

        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }
        if (SwipeController.swipeDown)
        {
            StartCoroutine(Down());
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff); 
    }

    private void Jump()
    {
        animator.SetTrigger("IsJumping");
        dir.y = jumpForce;
    }

    private IEnumerator Down()
    {
        animator.SetTrigger("IsDown");
        Vector3 capsuleCenter = capsuleCollider.center;
        float centerHigh = capsuleCollider.height;
        capsuleCollider.center = new Vector3 (capsuleCenter.x, -0.5f, capsuleCenter.z);
        capsuleCollider.height = 1f;
        dir.y = -(jumpForce * 1.5f);
        yield return new WaitForSeconds(1f);
        capsuleCollider.center = capsuleCenter;
        capsuleCollider.height = centerHigh;


        //animator.SetTrigger("IsDown");
        //Vector3 capsuleCenter = capsuleCollider.center;
        //capsuleCenter.y = 0.5f;
        //float centerHigh = capsuleCollider.height;
        //controller.center = capsuleCenter;
        //controller.height = 1f;
        //dir.y = -jumpForce;
        //yield return new WaitForSeconds(1f);
        //controller.center = capsuleCenter;
        //controller.height = centerHigh;

    }
    void FixedUpdate()
        {
        animator.SetFloat("Speed", speed);
        dir.z = speed;
        dir.y += gravity * Time.deltaTime; 
        controller.Move(dir * Time.deltaTime);
        }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coint"))
        {
            Coints++;
            CointTexst.text = Coints.ToString();
            PlayerPrefs.SetInt("Coints", Coints);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpeedIncrease()
    {
        yield return new WaitForSeconds(5f);
        if (speed < MaxSpeed)
        {
            //speed += 3f;
            StartCoroutine(SpeedIncrease());
        }
    }

}
