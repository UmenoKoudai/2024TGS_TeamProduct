using UnityEngine;
using UnityEngine.UI;
using VTNConnect;

namespace GameLoopTest
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : MonoBehaviour
    {
        [SerializeField] float _speed;
        Rigidbody _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                _rigidbody.AddForce(new Vector3(-10, 0, 0));
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _rigidbody.AddForce(new Vector3(10, 0, 0));
            }
        }
    }
}