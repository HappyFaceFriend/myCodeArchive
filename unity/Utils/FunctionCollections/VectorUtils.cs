﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VectorUtils
{
    public static Vector2 Vec3toVec2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }
    public static Vector3 Vec2toVec3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0);
    }
    /// <summary>
    /// Returns current mouse position as world position. Uses SreenToWorldPoint
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetMouseAsWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    /// <summary>
    /// Compares magnitude of two vector2s not using square root and returns diffrence
    /// </summary>
    /// <returns>negative if left is smaller, 0 if same, 1 if left is bigger</returns>
    public static float CompareSize(Vector2 left, Vector2 right)
    {
        return left.x * left.x + left.y * left.y - right.x * right.x + right.y * right.y;
    }
    /// <summary>
    /// Compares magnitude of the vector with a float not using square root and returns diffrence
    /// </summary>
    /// <returns>negative if vector is smaller, 0 if same, 1 if vector is bigger</returns>
    public static float CompareSize(Vector2 vector, float size)
    {
        return vector.x * vector.x + vector.y + vector.y - size * size;
    }
}
