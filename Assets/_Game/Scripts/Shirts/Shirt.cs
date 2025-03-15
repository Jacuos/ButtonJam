using UnityEngine;

namespace _Game
{
    public class Shirt : MonoBehaviour
    {
        public ColorConfig color;
        public int slotCount;
        public MeshRenderer mesh;
        
        public void Initalize(ColorConfig newColor, int count)
        {
            color = newColor;
            slotCount = count;
            mesh.material.color = newColor.color;
        }
    }
}