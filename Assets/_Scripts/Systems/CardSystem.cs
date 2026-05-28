using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CardSystem : Singleton<CardSystem>
{
   [SerializeField] private HandView handView;
   [SerializeField] private Transform drawPilePoint;
   [SerializeField] private Transform discardPilePoint;
   private readonly List<Card> drawPile= new ();
   private readonly List<Card> discardPile= new ();
   private readonly List<Card> hand= new ();

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }
    void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);

        }

        public void Setup(List<CardData> deckData)
    {
        foreach (var cardData in deckData)
        {
            Card card = new (cardData);
            drawPile.Add(card);
        }
    }
    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int actualAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        int notDrwanAmount = drawCardsGA.Amount - actualAmount;
        for (int i = 0; i < actualAmount; i++)        {
            yield return DrawCard();
        }
        if(notDrwanAmount > 0)
        {
            RefillDeck();
            for (int i = 0; i < notDrwanAmount; i++)
            {
                yield return DrawCard();
            }
            
        }
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
         List<Card> currentHand = new List<Card>(hand);
    Debug.Log("实际要处理的卡牌数量：" + currentHand.Count);

    foreach (var card in currentHand)
    {
        Debug.Log("正在处理卡牌：" );
        discardPile.Add(card);
        
        CardView cardView = handView.RemoveCard(card);
        Debug.Log("获取到的 cardView: " + (cardView != null ? cardView.name : "空！"));
        
        // 关键：加日志确认是否进入 DiscardCard
        Debug.Log("准备调用 DiscardCard");
        yield return DiscardCard(cardView);
        Debug.Log("DiscardCard 执行完毕！");
    }
    
    hand.Clear();
    Debug.Log("丢弃完成");
        // foreach (var card in hand)
        // {
        //     Debug.Log("正在处理卡牌：");
        //     discardPile.Add(card);
        //     CardView cardView = handView.RemoveCard(card);
        //     Debug.Log("获取到的 cardView：" + (cardView != null ? cardView.name : "空！"));
        //      yield return DiscardCard(cardView);
        // }
        // hand.Clear();
        // Debug.Log("丢弃完成");
    }

    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        Card card = playCardGA.Card;
        hand.Remove(card);
        CardView cardView = handView.RemoveCard(card);
        yield return DiscardCard(cardView);

        SpendManaGA spendManaGA = new SpendManaGA(card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);

        if(playCardGA.ManualTarget != null)
        {
            Debug.Log("卡牌有手动目标，执行手动目标效果");
            PerformEffectGA performEffectGA = new (card.ManualTargetEffect, new List<CombatantView>{playCardGA.ManualTarget});
            ActionSystem.Instance.AddReaction(performEffectGA);
        }

        discardPile.Add(card);
        foreach (var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
            PerformEffectGA performEffectGA = new (effectWrapper.Effect, targets);
            ActionSystem.Instance.AddReaction(performEffectGA);
        }
        //执行卡牌效果
        //Player.Instance.DiscardCard(this);
    }

    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        DiscardAllCardsGA discardAllCardsGA = new DiscardAllCardsGA();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
        //敌人行动
    }
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new DrawCardsGA(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
        //敌人行动结束
    }
    private IEnumerator DrawCard()
    { 
        if (drawPile.Count == 0)
        {
            Debug.Log("牌堆空了，无法抽牌");
            yield break;
        }
        else
        {Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
        }

    }
    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
        Debug.Log("牌堆已重新洗牌");
    }
    private IEnumerator DiscardCard(CardView cardView)
    {
        // SceneManager.MoveGameObjectToScene(cardView.gameObject, SceneManager.GetActiveScene());
        cardView.transform.DOScale(Vector3.zero, 0.15f);

        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject);
        // Debug.Log("丢弃一张牌2");
    }
}
