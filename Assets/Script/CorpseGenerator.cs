using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _corpsePrefab;
    [SerializeField]
    private GameObject[] _roomLayout;
    private BoxCollider2D _createArea;
    private VanConeManager _manager;
    // Start is called before the first frame update
    void Start()
    {
        _manager = FindObjectOfType<VanConeManager>();
        _createArea = GetComponent<BoxCollider2D>();
        float sizeX = _createArea.size.x/2;
        float sizeY = _createArea.size.y/2;
        for (int i = 0; i < _manager.DeathCount; i++)
        {
            float randomX = Random.Range(-sizeX, sizeX);
            float randomY = Random.Range(-sizeY, sizeY);
            Instantiate(_corpsePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
        if(_manager.DeathCount > 5)
        {
            _roomLayout[0].SetActive(true);
        }
        else if(_manager.DeathCount > 10)
        {
            _roomLayout[1].SetActive(true);
        }
        else if(_manager.DeathCount > 15)
        {
            _roomLayout[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
