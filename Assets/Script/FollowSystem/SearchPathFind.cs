using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>目標地点までの経路を求めるクラス</summary>
public class SearchPathFind
{
    /// <summary>移動が可能であるマップ</summary>
    Tilemap _map;
    /// <summary>ゴール地点</summary>
    Vector3 _goalPos;
    /// <summary>調査中のマス</summary>
    List<CellInfo> _searchingCells = new List<CellInfo>();
    /// <summary>ゴール地点までの経路</summary>
    Stack<Vector3> _pathToGoal = new Stack<Vector3>();
    /// <summary>経路探索中かどうか</summary>
    bool _isExploring;

    /// <summary>ゴール地点までの経路(読み取りのみ)</summary>
    public Stack<Vector3> GoalPath => _pathToGoal;

    /// <summary>初期化</summary>
    public void Init(FollowSystem follow)
    {
        _map = follow.Map;
    }

    /// <summary>探索で使った経路データのリセット</summary>
    public void GoalPathClear()
    {
        _searchingCells.Clear();
        _pathToGoal.Clear();
    }

    /// <summary>スタート地点からゴール地点までの経路探索(Tilemap限定)</summary>
    /// <param name="startPos">スタート地点</param>
    /// <param name="goalPos">ゴール地点</param>
    public void AstarSearchPath(Vector3 startPos, Vector3 goalPos)
    {
        //ゴール地点設定
        _goalPos = goalPos;

        //スタートの設定
        CellInfo start = new CellInfo();
        start.MyPos = startPos;
        start.Cost = 0;
        start.EstimatedCost = Vector2.Distance(startPos, goalPos);
        start.IsOpen = true;
        start.ParentPos = new Vector3(-99999, -99999, 0);
        _searchingCells.Add(start);
        //探索開始
        _isExploring = true;

        while(_isExploring == true && _searchingCells.Where(cell => cell.IsOpen == true).Any())
        {
            CellInfo minSumCostCell = new CellInfo();
            float minSunCost = float.MaxValue;
            foreach(CellInfo searchingCell in _searchingCells)
            {
                if (!searchingCell.IsOpen) continue;

                if (searchingCell.SumCost < minSunCost)
                {
                    minSunCost = searchingCell.SumCost;
                    minSumCostCell = searchingCell;
                }
            }

            SroundCellOpen(minSumCostCell);

            minSumCostCell.IsOpen = false;
        }

        CellInfo preCell = _searchingCells[_searchingCells.Count - 1];
        _pathToGoal.Push(preCell.MyPos);
        while (preCell.ParentPos != new Vector3(-99999, -99999, 0))
        {
            _pathToGoal.Push(preCell.ParentPos);
            preCell = _searchingCells.Where(cell => cell.MyPos == preCell.ParentPos).First();
        }
    }

    /// <summary>周囲のマスの調査を行う</summary>
    /// <param name="center">中心となるマス</param>
    void SroundCellOpen(CellInfo center)
    {
        Vector3Int centerPos = _map.WorldToCell(center.MyPos);

        for(int i = -1; i < 2; i++)
        {
            for(int j = -1; j < 2; j++)
            {
                //上下左右以外のCellは開かないためとばす
                if(i == j || i == -j) continue;

                Vector3Int posInt = new Vector3Int(centerPos.x + i, centerPos.y + j, centerPos.z);
                //指定の場所がマップ上であれば
                if(_map.HasTile(posInt))
                {
                    Vector3 pos = _map.CellToWorld(posInt);
                    pos = new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z);

                    //新たに調査するマスだったら
                    if (!_searchingCells.Where(cell => cell.MyPos == pos).Any())
                    {
                        //マスの設定
                        CellInfo cell = new CellInfo();
                        cell.MyPos = pos;
                        cell.Cost = center.Cost + 1;
                        cell.EstimatedCost = Vector2.Distance(pos, _goalPos);
                        cell.SumCost = cell.Cost + cell.EstimatedCost;
                        cell.ParentPos = center.MyPos;
                        cell.IsOpen = true;
                        _searchingCells.Add(cell);
                    }

                    //ゴールの位置と一致したら
                    if (_map.WorldToCell(_goalPos) == _map.WorldToCell(pos))
                    {
                        //探索終了
                        _isExploring = false;
                        //２重ループからぬける
                        goto SEARCHEND;
                    }
                }
            }
        }
    SEARCHEND:;
    }
}

public class CellInfo
{
    /// <summary>自分の位置</summary>
    Vector3 myPos;
    /// <summary>親の位置</summary>
    Vector3 parentPos;
    /// <summary>今までの歩いた歩数(実コスト)</summary>
    float cost;
    /// <summary>ゴールまでの距離(推定コスト)</summary>
    float estimatedCost;
    /// <summary>実コスト + 推定コスト</summary>
    float sumCost;
    /// <summary>調査対象かどうか・Trueは調査対象である</summary>
    bool isOpen;

    public Vector3 MyPos { get { return myPos; } set { myPos = value; } }
    public Vector3 ParentPos { get { return parentPos; } set { parentPos = value; } }
    public float Cost { get { return cost; } set { cost = value; } }
    public float EstimatedCost { get { return estimatedCost; } set { estimatedCost = value; } }
    public float SumCost { get { return sumCost; } set { sumCost = value; } }
    public bool IsOpen { get { return isOpen; } set { isOpen = value; } }
}
