using UnityEngine;
using System.Collections.Generic;

public class ZoneTrigger : MonoBehaviour
{
    [Header("Zone Settings")]
    [SerializeField] private float damagePerSecond = 10f; 
    [SerializeField] private float tickInterval = 1.0f;    

    
    private Dictionary<HpController, Coroutine> activeCoroutines = new Dictionary<HpController, Coroutine>();

    private void OnTriggerEnter(Collider other)
    {
     
        HpController targetHP = other.GetComponent<HpController>();

     
        if (targetHP != null && !activeCoroutines.ContainsKey(targetHP))
        {
            Debug.Log($"<color=purple>[지속 구역 진입]</color> {other.name}이(가) 위험 구역({gameObject.name})에 들어섰습니다.");

            
            Coroutine zoneCoroutine = StartCoroutine(DamageTickRoutine(targetHP));
            activeCoroutines.Add(targetHP, zoneCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HpController targetHP = other.GetComponent<HpController>();

        // 구역을 벗어나는 순간 해당 대상의 지속 루프를 즉시 중단
        if (targetHP != null && activeCoroutines.ContainsKey(targetHP))
        {
            StopCoroutine(activeCoroutines[targetHP]);
            activeCoroutines.Remove(targetHP);

            Debug.Log($"<color=orange>[지속 구역 탈출]</color> {other.name}이(가) 위험 구역({gameObject.name})에서 벗어났습니다.");
        }
    }

   
    /// 지정된 주기(tickInterval)마다 주기적으로 체력을 깎는 코루틴 함수
    private System.Collections.IEnumerator DamageTickRoutine(HpController targetHP)
    {
        while (targetHP != null && !targetHP.IsDead)
        {
            
            targetHP.TakeDamage(damagePerSecond);

            
            yield return new WaitForSeconds(tickInterval);
        }

        
        if (activeCoroutines.ContainsKey(targetHP))
        {
            activeCoroutines.Remove(targetHP);
        }
    }

    
    private void OnDisable()
    {
        StopAllCoroutines();
        activeCoroutines.Clear();
    }
}
