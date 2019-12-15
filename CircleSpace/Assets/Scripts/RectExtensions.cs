using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class RectExtensions
{
    public static List<List<Rect>> Split(this Rect fullRect, List<List<float>> list)
    {
        fullRect.height /= list.Count;
        List<List<Rect>> r = new List<List<Rect>>();
        for (int i = 0; i < list.Count; i++)
        {
            r.Add(new List<Rect>());
            float total = 0;
            list[i].ForEach((j) => { total += j; });
            float x = 0;
            for (int u = 0; u < list[i].Count; u++)
            {
                float width = list[i][u] / total;
                r[r.Count - 1].Add(new Rect(fullRect.x + x, fullRect.y + fullRect.height * i, fullRect.width * width, fullRect.height));
                x += fullRect.width * width;
            }
        }
        return r;
    }

    public static Rect WithY(this Rect rect, float newY)
    {
        rect.y = newY;
        return rect;
    }

    public static Rect WithX(this Rect rect, float newX)
    {
        rect.x = newX;
        return rect;
    }

    public static Rect WithXMin(this Rect rect, float newXMin)
    {
        rect.xMin = newXMin;
        return rect;
    }

    public static Rect WithYMin(this Rect rect, float newYMin)
    {
        rect.yMin = newYMin;
        return rect;
    }

    public static Rect WithXMax(this Rect rect, float newXMax)
    {
        rect.xMax = newXMax;
        return rect;
    }

    public static Rect WithYMax(this Rect rect, float newYMax)
    {
        rect.yMax = newYMax;
        return rect;
    }

    public static Rect WithDimensions(this Rect rect, Vector2 dimensions)
    {
        rect.width = dimensions.x;
        rect.height = dimensions.y;
        return rect;
    }

    public static Rect WithHeight(this Rect rect, float newHeight)
    {
        rect.height = newHeight;
        return rect;
    }

    public static Rect WithWidth(this Rect rect, float newWidth)
    {
        rect.width = newWidth;
        return rect;
    }

    public static Rect WithCenter(this Rect rect, Vector2 center)
    {
        rect.center = center;
        return rect;
    }

    public static Rect WithCenter(this Rect rect, float x, float y)
    {
        rect.center = new Vector2(x, y);
        return rect;
    }

    public static Rect WithHorizontalCenter(this Rect rect, float x)
    {
        rect.center = new Vector2(x, rect.center.y);
        return rect;
    }

    public static Rect WithVerticalCenter(this Rect rect, float y)
    {
        rect.center = new Vector2(rect.center.x, y);
        return rect;
    }

    public static Rect WithHorizontalPadding(this Rect rect, float padding)
    {
        rect.x += padding;
        rect.xMax -= padding * 2;

        return rect;
    }

    public static Rect WithVerticalPadding(this Rect rect, float padding)
    {
        rect.y += padding;
        rect.yMax -= padding * 2;

        return rect;
    }

    public static Rect WithPadding(this Rect rect, float padding)
    {
        rect.x += padding;
        rect.xMax -= padding * 2;
        rect.y += padding;
        rect.yMax -= padding * 2;

        return rect;
    }

    public static Rect WithPosition(this Rect rect, Vector2 position)
    {
        rect.position = position;
        return rect;
    }

    public static Rect WithSize(this Rect rect, Vector2 size)
    {
        rect.size = size;
        return rect;
    }

    public static Rect WithOffset(this Rect rect, Vector2 offset)
    {
        rect.position += offset;
        return rect;
    }

    public static Rect WithXOffset(this Rect rect, float offset)
    {
        rect.position += new Vector2(offset, 0);
        return rect;
    }

    public static Rect WithYOffset(this Rect rect, float offset)
    {
        rect.position += new Vector2(0, offset);
        return rect;
    }

    public static Vector2 TopLeft(this Rect rect)
    {
        return new Vector2(rect.xMin, rect.yMin);
    }

    public static Vector2 TopRight(this Rect rect)
    {
        return new Vector2(rect.xMax, rect.yMin);
    }

    public static Rect ScaleSizeBy(this Rect rect, float scale)
    {
        return rect.ScaleSizeBy(scale, rect.center);
    }

    public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale;
        result.xMax *= scale;
        result.yMin *= scale;
        result.yMax *= scale;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }

    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
    {
        return rect.ScaleSizeBy(scale, rect.center);
    }

    public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
    {
        Rect result = rect;
        result.x -= pivotPoint.x;
        result.y -= pivotPoint.y;
        result.xMin *= scale.x;
        result.xMax *= scale.x;
        result.yMin *= scale.y;
        result.yMax *= scale.y;
        result.x += pivotPoint.x;
        result.y += pivotPoint.y;
        return result;
    }

    public static bool IsClicked(this Rect rect)
    {
        Event e = Event.current;
        return rect.Contains(e.mousePosition) && e.type == EventType.MouseDown && e.button == 0;
    }

    public static bool IsRightClicked(this Rect rect)
    {
        Event e = Event.current;
        return rect.Contains(e.mousePosition) && e.type == EventType.MouseDown && e.button == 1;
    }

    public static bool IsHovered(this Rect rect)
    {
        Event e = Event.current;
        return rect.Contains(e.mousePosition);
    }

}