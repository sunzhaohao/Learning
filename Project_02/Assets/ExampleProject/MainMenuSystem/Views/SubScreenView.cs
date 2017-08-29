using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using uFrame.Kernel;
using uFrame.MVVM;
using uFrame.MVVM.Services;
using uFrame.MVVM.Bindings;
using uFrame.Serialization;
using UniRx;
using UnityEngine;


namespace uFrame.ExampleProject
{
    /*
     * This view serves as a base class for all the SubScreen views
     * It handles screen activation/deactivation.
     * It also handles binding for Close command. You can configure it using the inspector.
     */

    public class SubScreenView : SubScreenViewBase
    {

        public GameObject ScreenUIContainer;
        public CanvasGroup screenUIContainerCanvasGroup;

        public void Awake()
        {
            if(ScreenUIContainer!=null)
            {
                screenUIContainerCanvasGroup = ScreenUIContainer.GetComponent<CanvasGroup>();

                if (screenUIContainerCanvasGroup == null)
                    screenUIContainerCanvasGroup = ScreenUIContainer.AddComponent<CanvasGroup>();

            }
        }

        protected override void InitializeViewModel(uFrame.MVVM.ViewModel model)
        {
            base.InitializeViewModel(model);
        }

        public override void Bind()
        {
            base.Bind();
        }

        public override void IsActiveChanged(Boolean active)
        {
            /* 
             * Always make sure, that you cache components used in the binding handlers BEFORE you actually bind.
             * This is important, since when Binding to the viewmodel, Handlers are invoked immidiately
             * with the current values, to ensure that view state is consistant.
             * For example, you can do this in Awake or in KernelLoading/KernelLoaded.
             * However, in this example we simply use public property to get a reference to ScreenUIContainer.
             * So we do not have to cache anything.
             */
            var targetAlpha = active ? 1 : 0;
            var time = 0.2f;
            var delay = active ? time : 0;
            Debug.Log(active);

            if (active)
            {
                // ͸fade in
                ScreenUIContainer.SetActive(active);
                //screenUIContainerCanvasGroup.alpha = 0f;
                FadeAlpha(screenUIContainerCanvasGroup, targetAlpha, time, null, delay);
            }
            else
            {   //  fade out 
                FadeAlpha(screenUIContainerCanvasGroup, targetAlpha, time,
                    () =>
                    {
                        ScreenUIContainer.SetActive(SubScreen.IsActive);
                    },delay);
            }
        }


        void FadeAlpha(CanvasGroup target, float alpha, float time, Action onComplete, float delay)
        {
            StopCoroutine("Fade");
            StartCoroutine(Fade(target, alpha, time, onComplete, delay));
        }

        IEnumerator Fade(CanvasGroup target,float alpha,float time,Action onComplete,float delay)
        {
            target.interactable = false;
            if (delay > 0) yield return new WaitForSeconds(delay);
            var startTime = Time.time;
            while (Mathf.Abs(target.alpha - alpha) > 0.01f)
            {
                var elapsed = Time.time - startTime;
                var normalizedTime = Mathf.Clamp(elapsed / time, 0f, 1f);
                target.alpha = Mathf.Lerp(target.alpha, alpha, normalizedTime);

                yield return null;
            }

            if (onComplete != null)
                onComplete();
            target.interactable = true;

        }


    }
}