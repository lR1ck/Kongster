using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 2f; // Qué tan lejos llega el golpe
    public float attackDamage = 25f; // Cuánto daño hace
    public LayerMask enemyLayer; // Qué objetos son enemigos
    
    public Transform attackPoint; // Desde dónde sale el golpe
    
    private float attackCooldown = 0.5f; // Tiempo entre golpes
    private float nextAttackTime = 0f;

    void Update()
    {
        // Detectar clic izquierdo
        if (Input.GetMouseButtonDown(0) && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Debug.Log("¡GOLPE!");
        
        // Detectar enemigos en rango
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);
        
        // Hacer daño a cada enemigo golpeado
        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Le diste a: " + enemy.name);
            
            // Aquí después agregaremos el sistema de vida
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }
        }
    }
    
    // Para visualizar el rango de ataque en el editor
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
            
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}