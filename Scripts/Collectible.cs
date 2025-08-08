using UnityEngine;

/// <summary>
/// Collectible item (coin, health pack, relic). Attach to pickup prefabs.
/// </summary>
public class Collectible : MonoBehaviour
{
    public enum Type { Coin, Health, Relic }
    public Type itemType;
    public int value = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (itemType)
            {
                case Type.Coin:
                    GameManager.Instance.AddCoin();
                    break;
                case Type.Health:
                    other.GetComponent<PlayerController>().Heal(value);
                    break;
                case Type.Relic:
                    GameManager.Instance.CollectRelic();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
