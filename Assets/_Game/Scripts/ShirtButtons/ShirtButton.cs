using UnityEngine;

namespace _Game
{
    public class ShirtButton : MonoBehaviour
    {
        public ColorConfig colorConfig;
        public MeshRenderer mesh;
        
        public void Initalize(ColorConfig newColor)
        {
            colorConfig = newColor;
            mesh.material.color = newColor.color;
        }
    }
}