using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IEnumGameState<EnemyState>
{
    [SerializeField] private Animator animator;
    [SerializeField] private MaskTypeData maskTypeData;
    [SerializeField] private SubMask maskEquipped;
    [SerializeField] private SpriteRenderer sprMask;
    [SerializeField] private SpriteRenderer sprCharacter;
    [SerializeField] private Transform transformFlip;
    [SerializeField] private Transform transformScale;
    //[SerializeField] private SpriteRenderer sprCharacter;
    private EnemyState enemyState = EnemyState.MOVE;
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private TimerCheck frustrationTimer;
    [Header("Reaction")]
    [SerializeField] private Image imgReaction;
    [SerializeField] private Image imgFrustrationTimer;

    private Vector3 posMove= new Vector3(-3, 0, 0);
    [SerializeField] private Vector3 posDone = new Vector3(-6,6,0);
    RaycastHit2D hit2d;
    private void Start()
    {
        //Initialize(maskTypeData);
    }
    private void Update()
    {
        switch (enemyState)
        {
            case EnemyState.MOVE:
                if (transform.position.x > posMove.x)
                {
                    transform.position += (posMove - Vector3.right * transform.position.x).normalized * moveSpeed * Time.deltaTime;
                    hit2d = Physics2D.Raycast(transform.position + Vector3.left * 1.1f, Vector3.left, 0.2f);
                    if (hit2d)
                    {
                        Enemy enemyOther = hit2d.collider.GetComponent<Enemy>();
                        if (enemyOther != null)
                        {
                            if(transform.position.x > 8.5f)
                            {
                                ChangeState(EnemyState.PAUSE_CHECK);
                            }
                            else
                            {
                                ChangeState(EnemyState.IDLE);
                            }
                        }
                    }
                    animator.SetBool("Move",true);
                }
                else
                {
                    ChangeState(EnemyState.IDLE);
                }
                break;
            case EnemyState.PAUSE_CHECK:
                animator.SetBool("Move", false);
                hit2d = Physics2D.Raycast(transform.position + Vector3.left * 1.1f, Vector3.left, 0.2f);
                if (hit2d)
                {
                    Enemy enemyOther = hit2d.collider.GetComponent<Enemy>();
                    if (enemyOther == null)
                    {
                        ChangeState(EnemyState.MOVE);
                    }
                }
                break;
            case EnemyState.IDLE:
                animator.SetBool("Move", false);
                imgFrustrationTimer.fillAmount = 1-frustrationTimer.GetPercentage();
                if (frustrationTimer.UpdateTimer())
                {
                    ChangeState(EnemyState.ANGRY);
                }
                break;
            case EnemyState.ANGRY:
                if (Vector3.Distance(new Vector3(0, -8, 0), transform.position) > 2f)
                {
                    transform.position += (new Vector3(0,-8,0) - transform.position).normalized * moveSpeed * Time.deltaTime;
                    animator.SetBool("Move", true);
                }
                else
                {
                    animator.SetBool("Move", false);
                    gameObject.SetActive(false);
                }
                break;
            case EnemyState.DONE:
                if(transform.position.y < posDone.y)
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
        if (data == null) return;
        sprMask.sprite = null;
        maskTypeData = data;
        if (maskTypeData.animatorOverride != null)
            animator.runtimeAnimatorController = maskTypeData.animatorOverride;

        //sprCharacter.sprite = maskTypeData.maskSprite
        transformFlip.localScale = new Vector3(1 * (maskTypeData.flipCharacter ? -1 : 1), 1, 1);
        transformScale.localScale = Vector3.one * maskTypeData.scaleCharacter;
        transformScale.localPosition = maskTypeData.offsetPosCharacter;

        //sprMask.transform.localScale = new Vector3(1 * (maskTypeData.flipMask ? -1 : 1), 1, 1);

        sprMask.transform.localScale = Vector3.one * maskTypeData.scaleMask;
        sprMask.transform.localPosition = maskTypeData.offsetPosMask;



    }
    public void EquipMask(SubMask mask)
    {
        if (mask == null || maskEquipped!=null) return;
        maskEquipped = mask;
        SpriteMaskOffset spriteMaskOffset = maskTypeData.spriteMaskOffsets.Find(x => x.maskTarget.maskType == maskEquipped.MaskType.maskType);

        if (spriteMaskOffset != null)
        {
            sprMask.transform.localScale = Vector3.one * spriteMaskOffset.scaleMask;
            sprMask.transform.localPosition = spriteMaskOffset.offsetPosMask;
        }
        //sprMask.transform.localScale = Vector3.one * maskEquipped.MaskType.scaleMask;
        //if(maskEquipped.MaskType.flipCharacter != maskTypeData.flipCharacter)
        //{
        //    sprMask.transform.localScale = new Vector3(-sprMask.transform.localScale.x,
        //     sprMask.transform.localScale.y,
        //     sprMask.transform.localScale.z);
        //}

        //sprMask.transform.localPosition = maskEquipped.MaskType.offsetPosMask;


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
        switch (newState)
        {
            case EnemyState.MOVE:
                break;
            case EnemyState.IDLE:
                imgFrustrationTimer.gameObject.SetActive(true);
                break;
            case EnemyState.ANGRY:
                imgReaction.gameObject.SetActive(true);
                GetComponent<CircleCollider2D>().enabled = false;
                GameManager.instance.DeductHealth(5);
                break;
            case EnemyState.DONE:
                imgFrustrationTimer.gameObject.SetActive(false);
                GetComponent<CircleCollider2D>().enabled = false;
                break;
            default:
                break;
        }
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
    PAUSE_CHECK,
    IDLE,
    ANGRY,
    DONE,
}