using System;
using System.Runtime.CompilerServices;
using Loxodon.Framework.ViewModels;
using UnityEngine;

namespace Core.Model
{
    [System.Serializable]
    public class JobModel : ViewModelBase
    {
        /// <summary>
        /// Name of this job. This will not shown to the player
        /// </summary>
        public string Name;
        /// <summary>
        /// Description of this job. This will shown to player.
        /// </summary>
        public string Description;
        /// <summary>
        /// Icon of this job. This will be in our UI.
        /// </summary>
        public Sprite Icon;
        /// <summary>
        /// This is a base value for job. All calculations will be calculated via this value.
        /// 1 is a starter value.
        /// </summary>
        public int Value = 1;
        
        [SerializeField] private bool sold;
        [SerializeField] private int upgradeCount;
        [SerializeField] private int workerCount;
        [SerializeField] private int exp;
        [SerializeField] private int level;
        [SerializeField] private float workProgress;

        public float WorkProgress
        {
            get => workProgress;
            set => Set(ref workProgress, value, nameof(WorkProgress));
        }

        public int Level
        {
            get => level;
            set => Set(ref level, value, nameof(Level));
        }


        public int Exp
        {
            get => exp;
            set => Set(ref exp, value, nameof(Exp));
        }

        public int WorkerCount
        {
            get => workerCount;
            set => Set(ref workerCount, value, "WorkerCount");
        }
        
        public int UpgradeCount
        {
            get => upgradeCount;
            set => Set(ref upgradeCount, value, "UpgradeCount");
        }

        public bool IsSold
        {
            get => sold;
            set => Set(ref sold, value, "IsSold");
        }
    }
}