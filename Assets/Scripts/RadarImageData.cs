using UnityEngine;
namespace yz
{
    public class RadarImageData : MonoBehaviour
    {
        [SerializeField]
        public RadarStrength[] Strength;
    }

    [System.Serializable]
    public sealed class RadarStrength
    {
        [Range(0f, 1f)]
        public float value = 1f;
    }
}