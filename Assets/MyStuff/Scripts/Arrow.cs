using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IUpdate
{
    [SerializeField]

    private Transform carTarget;   

    public Player pl;
    GameManager _gm;

    private void Awake()
    {
        pl = FindObjectOfType<Player>();
        _gm = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }


    public void OnUpdate()
    {        
        if (pl.holdingTank == false && _gm.tankList.Count >= 1)
        {
            Vector3 targetPos = _gm.tankList[0].transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(_gm.tankList[0].transform.position);
        }
        else
        {
            Vector3 targetPos = carTarget.transform.position;
            targetPos.y = transform.position.y;
            transform.LookAt(carTarget);
        }
    }
}
