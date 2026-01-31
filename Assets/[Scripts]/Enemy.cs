using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnumGameState<EnemyState>
{
    [SerializeField] private MaskTypeData maskType;
    [SerializeField] private SubMask maskEquipped;
    private EnemyState enemyState = EnemyState.MOVE;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 posMove= new Vector3(-3, 0, 0);
    private Vector3 posDone = new Vector3(-6,6,0);

    [Header("Test")]
    public SubMask testMask;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EquipMask(testMask);
        }

        switch (enemyState)
        {
            case EnemyState.MOVE:
                if (Vector3.Distance(posMove, transform.position) > 1f)
                    transform.position += (posMove - Vector3.right * transform.position.x).normalized * moveSpeed * Time.deltaTime;
                break;
            case EnemyState.DONE:
                if(Vector3.Distance(posDone,transform.position) > 1f)
                    transform.position += (posDone - transform.position).normalized * moveSpeed * Time.deltaTime;
                break;
            default:
                break;
        }
    }
    public void EquipMask(SubMask mask)
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

    // Update values
    public void UpdateDonePos(Vector3 newPos)
    {
        posDone = newPos;
    }
    public void UpdateMovePos(Vector3 newPos)
    {
        posMove = newPos;
    }
    #region STATES
    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
    }
    public bool CompareState(EnemyState checkState) => enemyState == checkState;
    #endregion

    //private void OnMouseOver()
    //{
    //    Debug.Log("Hover");
    //}
}
public enum EnemyState
{
    MOVE,
    DONE
}