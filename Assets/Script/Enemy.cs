using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("PlayerのTransform")]
    [SerializeField]
    Transform _player;

    [Header("追跡を開始するPlayerとの距離")]
    [SerializeField]
    float _startFollowDis = 5;

    FollowSystem _followSystem;
    // Start is called before the first frame update
    void Start()
    {
        _followSystem = GetComponent<FollowSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //追従してなくPlayerが近づいたら
        if(!_followSystem.IsFollow && Vector3.Distance(transform.position, _player.position) < _startFollowDis)
        {
            //追従開始
            _followSystem.FollowStart();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //追従停止
            _followSystem.FollowStop();
            //ここにゲームオーバー判定を書く
            Debug.Log("Player died");
        }
    }

    private void OnDrawGizmos()
    {
        //追跡開始時のPlayerとの距離
        Gizmos.DrawWireSphere(transform.position, _startFollowDis);
    }
}
