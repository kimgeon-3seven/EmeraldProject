using UnityEngine;
using System.Collections.Generic;

public class PlayerCombat : MonoBehaviour
{
    [Header("Components")]
    public InputReader inputReader;
    public AnimationController animationController;

    [Header("Combat Settings")]
    public float attackCooldown = 0.5f;
    public float attackRange = 2.5f;
    public float attackRadius = 0.6f;
    public float attackDamage = 20f;
    public LayerMask enemyMask;

    private float lastAttackTime;
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;

    private void Update()
    {
        if (inputReader == null) return;

        if (inputReader.AttackPressed && Time.time >= lastAttackTime + attackCooldown && !isAttacking)
        {
            ProcessAttack();
        }
    }

    private void ProcessAttack()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        if (animationController != null)
        {
            animationController.PlayAttackAnimation();
        }
        Debug.Log("[전투] 플레이어 근접 공격 동작 시작");
    }

    // 애니메이션 이벤트 전용 타격 메서드 
    public void ExecuteRaycastAttack()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, attackRadius, direction, attackRange, enemyMask);

        
        HashSet<HpController> damagedTargets = new HashSet<HpController>();

        foreach (RaycastHit hit in hits)
        {
            
            HpController enemyHP = hit.collider.GetComponent<HpController>();

           
            if (enemyHP != null && !damagedTargets.Contains(enemyHP))
            {
                damagedTargets.Add(enemyHP);

               
                enemyHP.TakeDamage(attackDamage);

              
                Debug.Log($"<color=orange>[타격 성공]</color> {hit.collider.name} 적중! (지점: {hit.point})");
            }
        }
    }

    // 애니메이션 이벤트 전용 종료 메서드
    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.up * 1f;
        Gizmos.DrawLine(origin, origin + transform.forward * attackRange);
        Gizmos.DrawWireSphere(origin + transform.forward * attackRange, attackRadius);
    }
}
