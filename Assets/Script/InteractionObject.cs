using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [Header("Heal Settings")]
    [SerializeField] private float healAmount = 30f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            HpController playerHP = other.GetComponent<HpController>();

            if (playerHP != null && !playerHP.IsDead)
            {
               
                playerHP.Heal(healAmount);

              
                Debug.Log($"<color=cyan>[상호작용]</color> 회복 아이템 '{gameObject.name}'을(를) 획득하여 플레이어의 HP를 {healAmount}만큼 회복했습니다.");

                
                Destroy(gameObject);
            }
        }
    }
}
