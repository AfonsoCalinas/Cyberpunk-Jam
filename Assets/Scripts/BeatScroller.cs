    using UnityEngine;

    public class ArrowMovement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [SerializeField] private float _speed = 50f; // Units per second

        void Update()
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.up, Space.World);
        }
    }
