using Core.Model;
using Core.ViewModel;
using UnityEngine;

namespace Core.Utils
{
    /// <summary>
    /// The calculation on this class are temporary.
    /// </summary>
    public class JobMathOperation
    {
        public static readonly float UpgradeMultiply = 4f;
        public static readonly float WorkerMultiply = 5f;
        public static readonly float BuyMultiply = 20f;

        private readonly JobModel job;
        private readonly PlayerModel player;

        public JobMathOperation(JobModel job, PlayerModel player)
        {
            this.job = job;
            this.player = player;
        }

        /// <summary>
        /// Get's the current calculated upgrading price
        /// </summary>
        /// <returns></returns>
        public float GetCurrentEarning()
        {
            return (GetJobInitialPrice() * 0.012f) +
                   ((job.Value * job.Value) * 0.54f * job.Level) * (job.UpgradeCount * 1.13f);
        }


        /// <summary>
        /// Get's the current calculated upgrading price
        /// </summary>
        /// <returns></returns>
        public float GetCurrentUpgradePrice()
        {
            return (job.Level * UpgradeMultiply) * (job.UpgradeCount * job.Level * job.Value);
        }

        /// <summary>
        /// Get's the current calculated worker price
        /// </summary>
        /// <returns></returns>
        public float GetCurrentWorkerPrice()
        {
            return ((job.Value * WorkerMultiply) * ((job.Value / 2f) * WorkerMultiply)) *
                   Mathf.Clamp(job.WorkerCount, 1, int.MaxValue);
        }

        /// <summary>
        /// Get's the current worker progress.
        /// This decide how much workers can progress in a fixed frame
        /// </summary>
        /// <returns></returns>
        public float GetCurrentWorkerProgress()
        {
            if (job.WorkerCount == 0)
                return 0f;

            return ((job.WorkerCount / 20f) / job.Value) * Time.deltaTime;
        }

        /// <summary>
        /// Job price for first buy.
        /// </summary>
        /// <returns></returns>
        public float GetJobInitialPrice()
        {
            return (((job.Value * job.Value) * BuyMultiply));
        }

        /// <summary>
        /// Get's max exp for job
        /// </summary>
        /// <returns></returns>
        public float GetMaxExp()
        {
            return (job.Level * 5) * (job.Value);
        }

        /// <summary>
        /// Can player buy this job
        /// </summary>
        /// <returns></returns>
        public bool CanPlayerBuy()
        {
            if (job.IsSold)
                return false;

            return player.Money >= GetJobInitialPrice();
        }

        public float GetPlayerWorkProgress(int clickCount = 1)
        {
            return ((3f / job.Value) / 7) * clickCount;
        }
    }
}