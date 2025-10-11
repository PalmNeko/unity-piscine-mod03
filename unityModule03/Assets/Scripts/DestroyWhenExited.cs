using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DestroyWhenExited : MonoBehaviour
{
    
    void OnTriggerExit2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
