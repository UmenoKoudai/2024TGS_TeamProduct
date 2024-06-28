﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    FollowSystem _followSystem;

    SpriteRenderer _spriteRenderer;

    [SerializeField]
    EnemyImageController _enemyImageController;

    [Header("ゲームオーバーシーン名")]
    [SerializeField]
    string _gameOverSceneName;

    public void Start()
    {
        //追跡開始
        if (_followSystem == null) _followSystem = GetComponent<FollowSystem>();
        if(_spriteRenderer == null) _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_enemyImageController == null) _enemyImageController = new EnemyImageController(_spriteRenderer);
        _followSystem.FollowStart();
    }

    void Update()
    {
        if(_followSystem.MovingX > 0)
        {
            _enemyImageController.SpriteChange(EnemyImageController.EnemyImagePattern.Left, true);
        }
        else if (_followSystem.MovingX < 0)
        {
            _enemyImageController.SpriteChange(EnemyImageController.EnemyImagePattern.Left);
        }

        if(_followSystem.MovingY < 0 || _followSystem.MovingY > 0)
        {
            _enemyImageController.SpriteChange(EnemyImageController.EnemyImagePattern.Forward);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //追従停止
            _followSystem.FollowStop();
            //GameOver演出
            StartCoroutine(GameOverStart());
        }
    }

    /// <summary>Enemyの食べる音の再生とGameOverシーンに遷移</summary>
    IEnumerator GameOverStart()
    {
        GameManager.Instance.StateChange(GameManager.SystemState.GameOver);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
        AudioManager.Instance.SeClass.Play(AudioManager.SE.SEClip.MonsterEating);
        yield return new WaitForSeconds(0.8f);
        FindObjectOfType<SceneChange>().ChangeScene(_gameOverSceneName);
    }
}

/// <summary>EnemyのSpriteを変えるクラス</summary>
[Serializable]
public class EnemyImageController
{
    /// <summary>EnemyのSpriteRenderer</summary>
    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    Sprite[] _enemySprites;

    public EnemyImageController(SpriteRenderer spriteRenderer)
    {
        _spriteRenderer = spriteRenderer;
    }

    public enum EnemyImagePattern
    {
        Forward,
        Left,
    }

    public void SpriteChange(EnemyImagePattern imagePattern, bool IsFlip = false)
    {
        int index = (int)imagePattern;
        _spriteRenderer.sprite = _enemySprites[index];
        _spriteRenderer.flipX = IsFlip;
    }
}

public enum EnemyType
{
    /// <summary>チョコタ</summary>
    Chocota,
    /// <summary>チョコタ</summary>
    Ghoul,
}