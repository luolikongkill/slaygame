using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : CardViewOfBase
{

    [SerializeField] private GameObject wrapper;//悬浮包装
    [SerializeField] private LayerMask dropLayer;//碰撞箱子


    private Quaternion dragStartRotation;


    private bool isChangeform;
    private bool isShow;

    public Vector3 OriginalPosition;

    private Tween mytween = null;

    public void OnMouseEnter()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        // Debug.Log("Mouse Enter");

        if( mytween == null || !isShow && !mytween.IsPlaying() )
        {
            isShow = true;
            Vector3 pos = new (OriginalPosition.x,OriginalPosition.y+1,OriginalPosition.z);
            
            mytween = transform.DOMove(pos,0.02f).SetAutoKill(false);

        }


    }

    void OnMouseOver()
    {
        // ⭐ 悬停期间每帧检测右键点击
        if (Input.GetMouseButtonDown(1))
        {
            ChangeForm();  // 你的右键逻辑
        }
    }

    public void OnMouseExit()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        // Debug.Log("Mouse Exit");
        if(isShow )
        {
            isShow = false;
            mytween = transform.DOMove(OriginalPosition,0.1f).SetAutoKill(false);
        }
        

    }
    

    void OnMouseDown()
    {
        
        if(!Interactions.Instance.PlayerCanInteract()) return;
        // Debug.Log("Mouse Down");
        if(Card.ManualTargetEffect != null)
        {

            Interactions.Instance.PlayerIsDragging = true;
            CardSystem.Instance.manualTargetSystem.StartTargeting(transform.position);
            
        }

        else
        {
            Interactions.Instance.PlayerIsDragging = true;
            // wrapper.SetActive(true);
            // CardSystem.Instance.cardViewHoverSystem.Hide();
            dragStartRotation = transform.rotation;
            transform.rotation = Quaternion.Euler(0,0,0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }
        OnMouseEnter();

    }
    private bool hasloggedDrag = false;
    private bool IsPlaying = false;

    void OnMouseDrag()
    {
        if(!hasloggedDrag)
        {
            // Debug.Log("Mouse Dragging");
            hasloggedDrag = true;
        }
        if(!Interactions.Instance.PlayerCanInteract()) return;
        if(Card.ManualTargetEffect != null) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    void OnMouseUp()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * 10f, Color.red, 2f);
        hasloggedDrag = false;
        if(!Interactions.Instance.PlayerCanInteract()) return;

        if(Card.ManualTargetEffect != null)
        {
            EnemyView target=  CardSystem.Instance.manualTargetSystem.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(0));
            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new PlayCardGA(Card,target);
                ActionSystem.Instance.Perform(playCardGA);
                Debug.Log("Played card with manual target: " + Card.Title);
                IsPlaying = true;
            }
            else
            {
                if(target == null)
                {
                    Debug.Log("invalid target");
                }
                else
                {
                    Debug.Log("no enough mana");
                }
                Debug.Log("no enough mana or invalid target");
            }
        }
        else
        {
            if(ManaSystem.Instance.HasEnoughMana(Card.Mana) &&
            Physics.Raycast(transform.position,Vector3.forward,out RaycastHit hit,100f,dropLayer))
            {
                PlayCardGA playCardGA = new PlayCardGA(Card);
                ActionSystem.Instance.Perform(playCardGA);
                Debug.Log("Played card: " + Card.Title);
                IsPlaying = true;
            }
            else
            {
                Debug.Log("no enough mana or invalid drop position");
                transform.position = OriginalPosition;
                transform.rotation = dragStartRotation;
            }

        }
         Interactions.Instance.PlayerIsDragging = false;

         if(!IsPlaying)
         OnMouseExit();
    }




    public void ChangeForm()
    {
        if(Card.canchangeform)
        {
            Debug.Log("has 1");
            if(!isChangeform)
            {
                Cardbg.DOFade(1f,0.2f);
                isChangeform = true;
            }
            else
            {
                Cardbg.DOFade(0f,0.2f);
                isChangeform = false;
            }
            Card.CardChangeForm();
            Setup(Card);
        }
    }

}
