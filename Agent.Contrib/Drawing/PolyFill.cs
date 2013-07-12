using System;
using Microsoft.SPOT;

namespace Agent.Contrib.Drawing
{
    /// <summary>
    /// Poly Fill types
    /// EMPTY - only border
    /// SOLID - flood fill
    /// DOTS - . . . . 
    /// GRID - ++++
    /// VERTICAL - |||
    /// HORIZONTAL - -----
    /// </summary>
    public enum PolyFill
    {
        POLYFILL_EMPTY,
        POLYFILL_SOLID,
        POLYFILL_DOTS,
        POLYFILL_GRID,
        POLYFILL_HORIZONTAL,
        POLYFILL_VERTICAL,
        POLYFILL_CROSS_LEFT,
        POLYFILL_CROSS_RIGHT,
    }
}
