using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnumGameState<EnemyState>
{
    [SerializeField] private MaskTypeData maskType;
    [SerializeField] private DraggableMask maskEquipped;
    private EnemyState enemyState = EnemyState.MOVE;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 posMove= new Vector3(-3, 0, 0);
    private Vector3 posDone = new Vector3(4,5,0);
    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.MOVE:
                transform.position += (posMove - transform.position).normalized * moveSpeed * Time.deltaTime;
                break;
            case EnemyState.DONE:
                transform.position += (posDone - transform.position).normalized * moveSpeed * Time.deltaTime;

                break;
            default:
                break;
        }
    }
    public void EquipMask(DraggableMask mask)
    {
        if (mask == null) return;
        maskEquipped = mask;
        if (maskEquipped.MaskType == maskType)
        {
            Debug.Log("Match");
        }
        else
        {
            Debug.Log("Not Match");
        }
        ChangeState(EnemyState.DONE);
    }

    #region STATES
    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
    }
    public bool CompareState(EnemyState checkState) => enemyState == checkState;
    #endregion
}
public enum EnemyState
{
    MOVE,
    DONE
}