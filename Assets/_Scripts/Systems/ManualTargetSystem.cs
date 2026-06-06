using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTargetSystem : Singleton<ManualTargetSystem>
{
    [SerializeField] private ArrowView arrowView;
    [SerializeField] private LayerMask targetLayerMask;
    public void StartTargeting(Vector3 startPosition)
    {
        arrowView.gameObject.SetActive(true);
        arrowView.SetupArrow(startPosition);
    }
    public EnemyView EndTargeting(Vector3 endPosition)
    {
        arrowView.gameObject.SetActive(false);
        Debug.Log("End Targeting");
        if(Physics.Raycast(endPosition, Vector3.forward,
         out RaycastHit hit, 100f, targetLayerMask)&& hit.collider != null
         && hit.transform.TryGetComponent<EnemyView>(out EnemyView enemyView))
        {
            Debug.Log("Targeted enemy: " );
            return enemyView;
        }
        Debug.DrawLine(endPosition, endPosition + Vector3.forward * 10f, Color.red, 2f);
        Debug.Log("Raycast from " + endPosition + " did not hit a valid target.");
        return null;
    }
}
