using UnityEngine;

namespace _Game
{
    public class Shirt : MonoBehaviour
    {
        public ColorConfig color;
        public int slotCount;
        public MeshRenderer mesh;
        public GameObject[] buttonsSlots;
        
        public void Initalize(ColorConfig newColor, int count)
        {
            color = newColor;
            slotCount = count;
            mesh.material.color = newColor.color;
            for(int i=0;i<buttonsSlots.Length;i++)
                buttonsSlots[i].SetActive( i<count );
        }
    }
}