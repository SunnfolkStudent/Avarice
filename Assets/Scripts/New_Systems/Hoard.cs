using UnityEngine;

namespace New_Systems
{
    public class Hoard : MonoBehaviour
    {
        public int currentHoardValue;
        private Animator _animator;
        
        private static readonly int GoldpileXXL = Animator.StringToHash("GoldPileXXL");
        private static readonly int GoldpileXL = Animator.StringToHash("GoldPileXL");
        private static readonly int GoldpileL = Animator.StringToHash("GoldPileL");
        private static readonly int GoldpileM = Animator.StringToHash("GoldPileM");
        private static readonly int GoldpileS = Animator.StringToHash("GoldPileS");
        private static readonly int GoldpileXS = Animator.StringToHash("GoldPileXS");
        private static readonly int GoldpileXXS = Animator.StringToHash("GoldPileXXS");
        private static readonly int GoldpileEmpty = Animator.StringToHash("GoldPileEmpty");
        
        
        private int[] sizes =   { 1000, 750, 550, 300, 200, 100, 50, 0, -10};
        private static readonly int[] animations =  {GoldpileXXL, GoldpileXL, GoldpileL, GoldpileM, GoldpileS, GoldpileXS, GoldpileXXS, GoldpileEmpty};

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        private void Update()
        {
            for (var i = 0; i < animations.Length; i++)
            {
                if (currentHoardValue <= sizes[i] && currentHoardValue > sizes[i+1])
                {
                    _animator.Play(animations[i]);
                    break;
                }
            }
        }
    }
}

//TODO: Add featuyre that makes collectors get less and less value depending on the value of the hoard
