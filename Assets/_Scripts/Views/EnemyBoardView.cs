using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyBoardView : MonoBehaviour
{
    [SerializeField] private List<Transform> slots;
    public List<EnemyView> EnemyViews { get; private set; } = new List<EnemyView>();
    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = slots[EnemyViews.Count];
        EnemyView enemyView = EnemyViewCreator.Instance.CreateEnemyView(enemyData, slot.position, slot.rotation);
        enemyView.transform.parent = slot;
        EnemyViews.Add(enemyView);
    }
    public IEnumerator RemoveEnemy(EnemyView enemyView)
    {
        Tween tween = enemyView.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        EnemyViews.Remove(enemyView);
        yield return null;//等到下一帧再销毁，确保动画完成
        Destroy(enemyView.gameObject);
        Debug.Log(enemyView.name + "Enemy destroyed");
        // ReWardSystem.Instance.ReWardSet(EnemySystem.Instance.Enemies.Count);
    }
    public void RemoveEnemy(List<EnemyView> enemyViews)
    {
        for(int i = enemyViews.Count - 1; i >= 0; i--)
        {
            Destroy(enemyViews[i].gameObject);
            EnemyViews.Remove(enemyViews[i]);
        // ReWardSystem.Instance.ReWardSet(EnemySystem.Instance.Enemies.Count);
        }
    }
}
