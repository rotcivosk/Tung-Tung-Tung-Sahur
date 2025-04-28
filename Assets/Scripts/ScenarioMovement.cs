using UnityEngine;

public class CenárioMovimento : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}