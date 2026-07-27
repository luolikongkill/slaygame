using TMPro;
using UnityEngine;

public class CardView : CardViewOfBase
{

    [SerializeField] private GameObject wrapper;//悬浮包装
    [SerializeField] private LayerMask dropLayer;//碰撞箱子


    private Vector3 dragStartPositon;
    private Quaternion dragStartRotation;


    public void OnMouseEnter()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        // Debug.Log("Mouse Enter");
        wrapper.SetActive(false);
        Vector3 pos = new (transform.position.x,-2,0);
        CardSystem.Instance.cardViewHoverSystem.Show(Card,pos);

    }

    public void OnMouseExit()
    {
        if(!Interactions.Instance.PlayerCanHover()) return;
        // Debug.Log("Mouse Exit");
        CardSystem.Instance.cardViewHoverSystem.Hide();
        wrapper.SetActive(true);
    }


    void OnMouseDown()
    {
        if(!Interactions.Instance.PlayerCanInteract()) return;
        // Debug.Log("Mouse Down");
        if(Card.ManualTargetEffect != null)
        {
            OnMouseExit();
            Interactions.Instance.PlayerIsDragging = true;
            CardSystem.Instance.manualTargetSystem.StartTargeting(transform.position);
            
        }
        else{
        Interactions.Instance.PlayerIsDragging = true;
        wrapper.SetActive(true);
        CardSystem.Instance.cardViewHoverSystem.Hide();
        dragStartPositon = transform.position;
        dragStartRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }
        OnMouseEnter();

    }
    private bool hasloggedDrag = false;

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
            }
            else
            {
                Debug.Log("no enough mana or invalid drop position");
                transform.position = dragStartPositon;
                transform.rotation = dragStartRotation;
            }

        }
         Interactions.Instance.PlayerIsDragging = false;
         OnMouseExit();
    }


}
