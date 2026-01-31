using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnumGameState<EnemyState>
{
    [SerializeField] private Animator animator;
    [SerializeField] private MaskTypeData maskTypeData;
    [SerializeField] private SubMask maskEquipped;
    [SerializeField] private SpriteRenderer sprMask;
    //[SerializeField] private SpriteRenderer sprCharacter;
    private EnemyState enemyState = EnemyState.MOVE;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 posMove= new Vector3(-3, 0, 0);
    private Vector3 posDone = new Vector3(-6,6,0);

    private void Start()
    {
        //Initialize(maskTypeData);
    }
    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.MOVE:
                if (Vector3.Distance(posMove, transform.position) > 2f)
                {
                    transform.position += (posMove - Vector3.right * transform.position.x).normalized * moveSpeed * Time.deltaTime;
                    animator.SetBool("Move",true);
                }
                else
                {
                    animator.SetBool("Move", false);
                }
                break;
            case EnemyState.DONE:
                if(Vector3.Distance(posDone,transform.position) > 2f)
                {
                    transform.position += (posDone - transform.position).normalized * moveSpeed * Time.deltaTime;
                    animator.SetBool("Move", true);
                }
                else
                {
                    animator.SetBool("Move", false);
                    gameObject.SetActive(false);
                }
                break;
            default:
                break;
        }
    }
    public void Initialize(MaskTypeData data)
    {
        maskTypeData = data;
        if (maskTypeData.animatorOverride != null)
            animator.runtimeAnimatorController = maskTypeData.animatorOverride;
    }
    public void EquipMask(SubMask mask)
    {
        if (mask == null || maskEquipped!=null) return;
        maskEquipped = mask;
        if (maskEquipped.MaskType == maskTypeData)
        {
            GameManager.instance.CorrectMask(this);
        }
        else
        {
            GameManager.instance.WrongMask(this);
        }
        sprMask.sprite = mask.MaskType.maskSprite;
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