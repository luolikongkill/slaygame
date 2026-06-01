using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetRewardUI : MonoBehaviour
{
    [SerializeField] private GameObject rewardPanel;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private CardView CardPrefab;
    [SerializeField] private Button skipButton;

    private System.Action<CardData> onCardChosen;
    private List<CardView> rewardCards = new ();
    private System.Action onSkip;

    private void Awake()
    {
        skipButton.onClick.AddListener(OnSkipClicked);
    }

    // 打开奖励UI并生成卡牌
    public void ShowRewards(List<CardData> GeneratedCards, System.Action<CardData> onChosen, System.Action onSkipCallback)
    {
        onCardChosen = onChosen;
        onSkip = onSkipCallback;

        // 先清空之前生成的卡牌
        ClearCards();

        // 实例化3张奖励卡
        foreach (var card in GeneratedCards)
        {
            var cardview = Instantiate(CardPrefab, cardContainer);
            rewardCards.Add(cardview);
        }

        rewardPanel.SetActive(true);
    }

    // 玩家选了一张卡
    private void OnCardClicked(CardData chosenCard)
    {
        onCardChosen?.Invoke(chosenCard);
        HeroData.AllDeck.Add(chosenCard);

        CloseUI();
    }

    // 玩家点击跳过
    private void OnSkipClicked()
    {
        onSkip?.Invoke();
        CloseUI();
    }

    // 关闭UI并清理
    private void CloseUI()
    {
        rewardPanel.SetActive(false);
        ClearCards();
    }

    private void ClearCards()
    {
        foreach (var card in rewardCards)
        {
            Destroy(card.gameObject);
        }
        rewardCards.Clear();
    }
}
