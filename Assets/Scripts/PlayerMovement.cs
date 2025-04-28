using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float strafeSpeed = 5f;
    [SerializeField] private GameObject baseballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 700f;
    [SerializeField] private Camera mainCamera;

    void Update()
    {
        MovePlayer();
        HandleAttack();
    }

    private void MovePlayer()
    {
        // Movimento lateral no PC (WASD)
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 strafe = new Vector3(horizontal, 0f, 0f);
        transform.Translate(strafe * strafeSpeed * Time.deltaTime, Space.World);

        // Movimento lateral no Mobile (arrastar o dedo)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float moveX = touch.deltaPosition.x * strafeSpeed * 0.001f;
                transform.Translate(moveX, 0f, 0f);
            }
        }
    }

    private void HandleAttack()
    {
        // Mobile: toca na tela para atirar
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchPosition = Input.GetTouch(0).position;
            ShootToPosition(touchPosition);
        }

        // PC: clica botão esquerdo para atirar
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Input.mousePosition;
            ShootToPosition(clickPosition);
        }
    }

private void ShootToPosition(Vector3 screenPosition)
{
    Ray ray = mainCamera.ScreenPointToRay(screenPosition);
    if (Physics.Raycast(ray, out RaycastHit hit))
    {
        Vector3 direction = (hit.point - throwPoint.position).normalized;

        GameObject baseball = Instantiate(baseballPrefab, throwPoint.position, Quaternion.identity);
        Rigidbody rb = baseball.GetComponent<Rigidbody>();
        rb.AddForce(direction * throwForce);

        Destroy(baseball, 3f); 
    }
}
}
