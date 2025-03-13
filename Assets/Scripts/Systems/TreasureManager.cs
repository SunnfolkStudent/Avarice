using UnityEngine;
public class TreasureManager : MonoBehaviour
{
   [SerializeField] private int treasureValue;

   [SerializeField] private TreasureState state;

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
   }
}
