using System;
using UnityEngine;

namespace _Game
{
    public class CustomMousePointer : MonoBehaviour
    {
        public float upScale;
        public float downScale;
        public float scaleSpeed;

        void Update()
        {
            
            Vector3 mousePos = Input.mousePosition;
            //mousePos.z = 0.5f;
            transform.position = mousePos;
            Vector3 targetScale = Input.GetMouseButton(0) ? Vector3.one * downScale : Vector3.one * upScale;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime*scaleSpeed);
            transform.parent.SetAsLastSibling();
        }
    }
}