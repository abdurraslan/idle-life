#if UNITY_EDITOR
#define CHEATS_ENABLED
#endif

using System;
using System.Collections.Generic;
using Core.Model;
using Core.View;
using Core.ViewModel;
using Loxodon.Framework.Binding;
using Loxodon.Framework.Contexts;
using UnityEngine;


namespace Core.Controller
{
    
    public class JobViewController : MonoBehaviour
    {
        /// <summary>
        /// Jobs will be added from inspector.
        /// (TODO: Need XML-JSON database)
        /// </summary>
        public List<JobViewModel> JobDatabase = new List<JobViewModel>();
        
        /// <summary>
        /// We need player reference for our Jobs.
        /// </summary>
        [NonSerialized] public PlayerModel player;

        /// <summary>
        /// Content of Jobs. ItemTemplate will be instantiate on this transform
        /// </summary>
        public Transform Content;
        
        /// <summary>
        /// Item template for list. This can be a prefab or a scene object.
        /// </summary>
        public GameObject ItemTemplate;
        
        
        void Awake()
        {
            //Register this to databinding service.
            ApplicationContext context = Context.GetApplicationContext();
            BindingServiceBundle bindingService = new BindingServiceBundle(context.GetContainer());
            bindingService.Start();
        }
        
        private void Start()
        {
            if (player == null)
            {
                Debug.LogWarning("Player reference for JobViewController is null. Trying to find a reference!");
                PlayerViewModel pwm = FindObjectOfType<PlayerViewModel>();
                if (pwm == null)
                {
                    Debug.LogError("Can't find any PlayerViewModel on scene. Can't continue!");
                    return;
                }
                player = pwm.Player;
            }
            
            //Initialize and create jobs.
            foreach (var jobModel in JobDatabase)
            {
                jobModel.Initialize(this.player);
                InstantiateJobPrefabWithDataContext(jobModel);
            }
        }


#if CHEATS_ENABLED
        /// <summary>
        /// Cheats.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.Money += player.Money * 10;
            }
        }
#endif
        
        /// <summary>
        /// JobModelView does not inherit Monobehaviour, so we call all of them in every fixed update.
        /// Fixed update is reduced for sake of performance.
        /// </summary>
        private void FixedUpdate()
        {
            for (var index = 0; index < JobDatabase.Count; index++)
            {
                var jobModel = JobDatabase[index];
                jobModel.OnFixedUpdate();
            }
        }


        /// <summary>
        /// Instantiate the ViewElements with Data
        /// </summary>
        /// <param name="jobModelView"></param>
        public void InstantiateJobPrefabWithDataContext(JobViewModel jobModelView)
        {
            //instantiate prefab
            var itemViewGo = Instantiate(this.ItemTemplate, this.Content, false);
            //get jobview
            JobView jobView = itemViewGo.GetComponent<JobView>();
            //set jobview data
            jobView.SetDataContext(jobModelView);
            //Set active will check if its active or not. We don't need to do that here.
            itemViewGo.SetActive(true);
            
            
        }
    }
}