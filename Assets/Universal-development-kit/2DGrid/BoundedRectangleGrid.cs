using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CellField2D
{
    public class BoundedRectangleGrid<TCell> : RectangleField<TCell> where TCell : I2DCell
    {
        public BoundedRectangleGrid(int width, int height) {
        
        }
    }

}
