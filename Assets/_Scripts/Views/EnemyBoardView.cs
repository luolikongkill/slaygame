using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class EnemyBoardView : MonoBehaviour
{
    [SerializeField] private List<Transform> slots;
    [SerializeField] private Transform Slot;

    private Vector3 Defaultoffset = new Vector3(0,0,0);
    private Vector3 Defaultlocalscale = new Vector3(1,1,1);

    [SerializeField]private EnemyViewCreator enemyViewCreator;
    public List<EnemyView> EnemyViews { get; private set; } = new List<EnemyView>();
    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = slots[EnemyViews.Count];
        EnemyView enemyView = enemyViewCreator.CreateEnemyView(enemyData, slot.position, slot.rotation);
        enemyView.transform.parent = slot;
        EnemyViews.Add(enemyView);
    }

    public void AddEnemy(EnemyGroup group)
    {
        int offsetCount = group.offsets.Count();
        int scaleCount = group.localscales.Count();
        
        for (int index = 0; index < group.enemydatas.Count; index++)
        {
            if(index > offsetCount - 1)
            group.offsets[index] = Defaultoffset;
            if(index > scaleCount - 1)
            group.localscales[index] = Defaultlocalscale;
            
            EnemyView enemyView = enemyViewCreator.CreateEnemyView(group.enemydatas[index], Slot.position + group.offsets[index],Slot.rotation);
            enemyView.transform.parent = Slot;
            enemyView.transform.localScale = group.localscales[index];

            EnemyViews.Add(enemyView);

        }


    }


    public IEnumerator RemoveEnemy(EnemyView enemyView)
    {
        Tween tween = enemyView.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        EnemyViews.Remove(enemyView);
        yield return new WaitForSeconds(.35f);//等到下一帧再销毁，确保动画完成
        Destroy(enemyView.gameObject);
        Debug.Log(enemyView.name + "Enemy destroyed");
        

    }
    public void RemoveAllEnemyView(List<EnemyView> enemyViews)
    {
        for(int i = enemyViews.Count - 1; i >= 0; i--)
        {
            Destroy(enemyViews[i].gameObject);
            EnemyViews.Remove(enemyViews[i]);
        

        }
    }
}
