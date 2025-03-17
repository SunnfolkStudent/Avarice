using Scripts.Enemies;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TreasureManager : MonoBehaviour
{
   [SerializeField] public int treasureValue;
   [SerializeField] private TreasureState state;
   private Animator _animator;

   private enum TreasureState
   {
      Empty = 0,
      Small = 100,
      Medium = 200,
      Full = 300,
   }

   private void Awake()
   {
      state = TreasureState.Full;
      treasureValue = (int)TreasureState.Full;
   }

   private void Start()
   {
      _animator = GetComponent<Animator>();
   }

   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.gameObject.CompareTag("Enemy"))
      {
         other.transform.GetComponent<CollectTreasure>().PickupTreasure(ref treasureValue, this);
      }
   }
}
