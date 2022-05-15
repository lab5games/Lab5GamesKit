using UnityEngine;
using UnityEngine.UI;

namespace Lab5Games
{
    [RequireComponent(typeof(CanvasRenderer))]
    public class NoDrawingGraphic : Graphic
    {
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
