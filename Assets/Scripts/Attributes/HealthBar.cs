using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health healthComponent;
        [SerializeField] RectTransform foreground;
        [SerializeField] Canvas rootCanvas;

        private void Start() 
        {
            rootCanvas.enabled = false;    
        }

        void Update()
        {
            if (Mathf.Approximately(healthComponent.GetFraction(), 0) || Mathf.Approximately(healthComponent.GetFraction(), 1))
            {
                rootCanvas.enabled = false;
                return;
            }

            rootCanvas.enabled = true;
            foreground.localScale = new Vector3(healthComponent.GetFraction(), foreground.localScale.y, foreground.localScale.z);
        }
    }

}