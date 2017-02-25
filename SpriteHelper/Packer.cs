using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpriteHelper
{
    public static class Packer
    {
        /// <summary>
        /// Packs rectangles in a max width.
        /// </summary>
        /// <param name="sizes">Input values.</param>
        /// <param name="maxWidth">Max width.</param>
        /// <returns>Result positions, array indices correspond to input</returns>
        public static List<Tuple<Point, Size>> Pack(IEnumerable<Size> input, int maxWidth)
        {
            var sizes = input.ToList();
            var result = new List<Tuple<Point, Size>>();

            maxWidth = Math.Max(maxWidth, sizes.Max(s => s.Width));

            var y = 0;

            while (sizes.Count > 0)
            {
                // Find the widest remaining object
                var widest = sizes.First(s => s.Width == sizes.Max(s2 => s2.Width));
                var widthOfWidest = widest.Width;
                var heightOfWidest = widest.Height;
                sizes.Remove(widest);

                // Put it on the left-most place.
                result.Add(Tuple.Create(new Point(0, y), widest));

                // Now look for stuff to fill the space on the right.
                var startingX = widthOfWidest;
                var startingY = y;
                var widthRemaining = maxWidth - widthOfWidest;
                var heightRemaining = heightOfWidest;
                while (true)
                {
                    Size widestFitting;
                    try
                    {
                        widestFitting = sizes.First(
                            s => s.Width == sizes.Where(s2 => s2.Width <= widthRemaining).Max(s2 => s2.Width) &&
                                 s.Height <= heightRemaining);
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }
                    
                    var heightOfWidestFitting = widestFitting.Height;
                    sizes.Remove(widestFitting);

                    // Put it on the right.
                    result.Add(Tuple.Create(new Point(startingX, startingY), widestFitting));

                    // Update values.
                    startingY += heightOfWidestFitting;
                    heightRemaining -= heightOfWidestFitting;

                    if (heightRemaining <= 0)
                    {
                        break;
                    }
                }

                // Update values.
                y += heightOfWidest;
            }

            return result;
        }
    }
}
