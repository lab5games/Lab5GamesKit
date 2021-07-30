using UnityEngine;
using UnityEngine.UI;

namespace Lab5Games
{
    [AddComponentMenu("Lab5Games/UI/NoDrawingGraphic")]
    [RequireComponent(typeof(CanvasRenderer))]
    public class NoDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty()
        {
            
        }

        public override void SetVerticesDirty()
        {
            
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
