using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>追従機能クラス</summary>

[RequireComponent(typeof(Rigidbody2D))]
public class FollowSystem : MonoBehaviour
{
    [Header("設定(値)")]

    [Header("追従時に保つ対象との距離")]
    [SerializeField]
    float _followingStopDistance = 1f;

    [Header("追従歩行速度")]
    [SerializeField]
    float _followSpeed = 80f;



    [Space]

    [Header("設定(わりあて)")]

    [Tooltip("追従する対象物")]
    [SerializeField]
    Transform _target;

    [Tooltip("TileMap")]
    [SerializeField]
    Tilemap _map;

    [Space]

    [Header("確認用")]

    [Header("現在動いている方向(上下左右をXとY軸で表現)")]
    [Tooltip("-1, 0, 1の値で返す")]
    [SerializeField, ReadOnly]
    int _moveX = 0;

    [Tooltip("-1, 0, 1の値で返す")]
    [SerializeField, ReadOnly]
    int _moveY = 0;

    [Header("現在向いている方向(上下左右をXとY軸で表現)")]
    [Tooltip("-1, 0, 1の値で返す")]
    [SerializeField, ReadOnly]
    int _forwardX = 0;

    [Tooltip("-1, 0, 1の値で返す")]
    [SerializeField, ReadOnly]
    int _forwardY = 0;


    /// <summary>追従する自分のRigidbody2D</summary>
    Rigidbody2D _rb;
    /// <summary>Gizmo用追従経路の各地点のデータ配列</summary>
    Vector3[] _goalPathGizmo;
    /// <summary>追従時の次に向かうべき地点</summary>
    Vector3 _nextPoint;
    /// <summary>今対象を追いかけているかどうか</summary>
    bool _isFollowMoving = false;

    Vector3 _pastPoint = Vector3.zero;

    SearchPathFind _followPathFind = new();

    bool _isFollow = false;

    float _count = 0;

    bool _isStopMove = false;

    [Header("追従時に止まる中間のマス数")]
    [SerializeField]
    int _stopDistanceSquare = 2;

    [Header("追従を再開する中間のマス数")]
    [SerializeField]
    int _restartDistanceSquare = 4;

    /// <summary>移動可能マップ</summary>
    public Tilemap Map => _map;

    /// <summary>現在動いている方向を示すもの(X軸)</summary>
    public int MovingX => _moveX;
    /// <summary>現在動いている方向を示すもの(Y軸)</summary>
    public int MovingY => _moveY;
    /// <summary>現在向いている方向を示すもの(X軸)</summary>
    public int ForwardX => _forwardX;
    /// <summary>現在向いている方向を示すもの(Y軸)</summary>
    public int ForwardY => _forwardY;
    /// <summary>今追従を起動しているかどうか</summary>
    public bool IsFollow => _isFollow;

    public Transform Target => _target;

    Vector3 _moveVec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        //初期化
        _followPathFind.Init(this);

        _rb.velocity = Vector3.zero;

        //探索開始
        FollowStart();
    }

    // Update is called once per frame
    void Update()
    {
        DirMovementJudge();

        //追従システム作動中
        if(_isFollow)
        {
            //追従している
            if (_isFollowMoving)
            {

                //目的の経路地点についたら
                if (Vector3.Distance(_nextPoint, transform.position) < 0.1f)
                {
                    FollowPathUpdate();
                    if (_followPathFind.GoalPath.Count == _stopDistanceSquare) _isFollowMoving = false;
                    //次の目的となる経路地点に更新
                    else _nextPoint = _followPathFind.GoalPath.Pop();
                }
            }
            //対象に追いついて追従していない
            else
            {
            　　//対象から一定距離離れたら
                if (Vector3.Distance(_target.position, this.transform.position) > _followingStopDistance)
                {
                    FollowPathUpdate();
                    if (_followPathFind.GoalPath.Count >= _restartDistanceSquare)
                    {
                        _isFollowMoving = true;
                    }
                }

            }
        }
    }

    private void FixedUpdate()
    {
        //追従移動
        if (_isFollowMoving)
        {
            if (_pastPoint != _nextPoint)
            {
                _moveVec = _nextPoint - transform.position;
                _pastPoint = _nextPoint;
                _rb.velocity = _moveVec.normalized * _followSpeed * Time.fixedDeltaTime;
            }
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    /// <summary>追従を開始する</summary>
    public void FollowStart()
    {
        //追従経路探索
        NextFollowPathPrepare();
        //Gizmo
        _goalPathGizmo = new Vector3[_followPathFind.GoalPath.Count];
        _followPathFind.GoalPath.CopyTo(_goalPathGizmo, 0);
        //追従開始
        _isFollow = true;
        _isFollowMoving = true;
    }

    /// <summary>追従経路更新</summary>
    public void FollowPathUpdate()
    {
        NextFollowPathPrepare();
        Debug.Log("経路更新");
        //Gizmo用
        _goalPathGizmo = new Vector3[_followPathFind.GoalPath.Count];
        _followPathFind.GoalPath.CopyTo(_goalPathGizmo, 0);
    }

    /// <summary>追従を終了する</summary>
    public void FollowStop()
    {
        _isFollow = false;
        _rb.velocity = Vector3.zero;
    }

    /// <summary>次の追従経路の準備を行う</summary>
    private void NextFollowPathPrepare()
    {
        //探索経路のリセット
        _followPathFind.GoalPathClear();
        //探索
        _followPathFind.AstarSearchPath(this.transform.position, _target.position);
        //次の目的地の設定
        _nextPoint = _followPathFind.GoalPath.Pop();
    }

    /// <summary>左右上下どの方向に動いているのか計算</summary>
    private void DirMovementJudge()
    {
        Vector3 vec = _rb.velocity.normalized;
        if(vec.magnitude == 0)
        {
            _moveX = 0;
            _moveY = 0;
            return;
        }

        if(vec.x >= - Mathf.Cos(45) &&  vec.x <= Mathf.Cos(45))
        {
            if (vec.y >= 0)
            {
                _moveX = 0;
                _moveY = 1;
            }
            else
            {
                _moveX = 0;
                _moveY = -1;
            }
            _forwardX = _moveX;
            _forwardY = _moveY;
        }
        else
        {
            if (vec.x >= 0)
            {
                _moveX = 1;
                _moveY = 0;
            }
            else
            {
                _moveX = -1;
                _moveY = 0;
            }
            _forwardX = _moveX;
            _forwardY = _moveY;
        }
    }

    private void OnDrawGizmos()
    {
        if(_goalPathGizmo != null)
        {
            for (int i = 0; i < _goalPathGizmo.Length - 1; i++)
            {
                Gizmos.DrawLine(_goalPathGizmo[i], _goalPathGizmo[i + 1]);
            }
        } 
    }
}
