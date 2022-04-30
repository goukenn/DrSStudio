

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RegionUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:RegionUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Collections ;
namespace IGK.DrSStudio.Drawing2D
{
    //static class Extension
    //{
    //    public static ScanLine[] GetScanLine(this System.Drawing.Rectangle rect)
    //    {
    //        ScanLine[] tab = new ScanLine[rect.Height];
    //        for (int i = 0; i < tab.Length; i++)
    //        {
    //            tab[i] = new ScanLine(rect.X, rect.Y + i, rect.Width);
    //        }
    //        return tab;
    //    }
    //    public static Rectangle[] ToRectangle(this RectangleF[] rect)
    //    {
    //        Rectangle[] c = new Rectangle[rect.Length];
    //        for (int i = 0; i < c.Length; i++)
    //        {
    //            c[i] = Rectangle.Round(rect[i]);
    //        }
    //        return c;
    //    }
    //}
    //public class RegionUtils
    //{
    //    public static CoreGraphicsPath GetPath(Region region)
    //    {
    //        CoreGraphicsPath v_out = null;
    //        Graphics g = Graphics.FromHwnd(IntPtr.Zero);
    //        Rectangle bounds = Rectangle.Round(region.GetBounds(g));
    //        Rectangle[] scans = region.GetRegionScans(new Matrix()).ToRectangle();
    //        BitVector2D stencil = new BitVector2D(bounds.Width, bounds.Height);
    //        for (int i = 0; i < scans.Length; ++i)
    //        {
    //            Rectangle rect = scans[i];
    //            rect.X -= bounds.X;
    //            rect.Y -= bounds.Y;
    //            stencil.SetUnchecked(rect, true);
    //        }
    //        v_out = PathFromStencil(stencil, new Rectangle(0, 0, stencil.Width, stencil.Height));
    //        using (Matrix matrix = new Matrix())
    //        {
    //            matrix.Reset();
    //            matrix.Translate(bounds.X, bounds.Y);
    //            v_out.Transform(matrix);
    //        }
    //        return v_out;
    //    }
    //    private static CoreGraphicsPath PathFromStencil(BitVector2D stencil, Rectangle bounds)
    //    {
    //        if (stencil.IsEmpty)
    //        {
    //            return null;
    //        }
    //        CoreGraphicsPath cp = new CoreGraphicsPath();
    //        Point start = bounds.Location;
    //        Vector<Point> pts = new Vector<Point>();
    //        int count = 0;
    //        // find all islands
    //        while (true)
    //        {
    //            bool startFound = false;
    //            while (true)
    //            {
    //                if (stencil[start])
    //                {
    //                    startFound = true;
    //                    break;
    //                }
    //                ++start.X;
    //                if (start.X >= bounds.Right)
    //                {
    //                    ++start.Y;
    //                    start.X = bounds.Left;
    //                    if (start.Y >= bounds.Bottom)
    //                    {
    //                        break;
    //                    }
    //                }
    //            }
    //            if (!startFound)
    //            {
    //                break;
    //            }
    //            pts.Clear();
    //            Point last = new Point(start.X, start.Y + 1);
    //            Point curr = new Point(start.X, start.Y);
    //            Point next = curr;
    //            Point left = Point.Empty;
    //            Point right = Point.Empty;
    //            // trace island outline
    //            while (true)
    //            {
    //                left.X = ((curr.X - last.X) + (curr.Y - last.Y) + 2) / 2 + curr.X - 1;
    //                left.Y = ((curr.Y - last.Y) - (curr.X - last.X) + 2) / 2 + curr.Y - 1;
    //                right.X = ((curr.X - last.X) - (curr.Y - last.Y) + 2) / 2 + curr.X - 1;
    //                right.Y = ((curr.Y - last.Y) + (curr.X - last.X) + 2) / 2 + curr.Y - 1;
    //                if (bounds.Contains(left) && stencil[left])
    //                {
    //                    // go left
    //                    next.X += curr.Y - last.Y;
    //                    next.Y -= curr.X - last.X;
    //                }
    //                else if (bounds.Contains(right) && stencil[right])
    //                {
    //                    // go straight
    //                    next.X += curr.X - last.X;
    //                    next.Y += curr.Y - last.Y;
    //                }
    //                else
    //                {
    //                    // turn right
    //                    next.X -= curr.Y - last.Y;
    //                    next.Y += curr.X - last.X;
    //                }
    //                if (Math.Sign(next.X - curr.X) != Math.Sign(curr.X - last.X) ||
    //                    Math.Sign(next.Y - curr.Y) != Math.Sign(curr.Y - last.Y))
    //                {
    //                    pts.Add(curr);
    //                    ++count;
    //                }
    //                last = curr;
    //                curr = next;
    //                if (next.X == start.X && next.Y == start.Y)
    //                {
    //                    break;
    //                }
    //            }
    //            Point[] points = pts.ToArray();
    //            ScanLine[] scans = GetScans(points, 0, points.Length);
    //            foreach (ScanLine scan in scans)
    //            {
    //                stencil.Invert(scan);
    //            }
    //            cp.AddLines(points);
    //            cp.CloseFigure();
    //        }
    //        return cp;
    //    }
    //    public static ScanLine[] GetScans(Point[] vertices, int startIndex, int length)
    //    {
    //        if (length > vertices.Length - startIndex)
    //        {
    //            throw new ArgumentException("out of bounds: length > vertices.Length - startIndex");
    //        }
    //        int ymax = 0;
    //        // Build edge table
    //        Edge[] edgeTable = new Edge[length];
    //        int edgeCount = 0;
    //        for (int i = startIndex; i < startIndex + length; ++i)
    //        {
    //            Point top = vertices[i];
    //            Point bottom = vertices[(((i + 1) - startIndex) % length) + startIndex];
    //            int dy;
    //            if (top.Y > bottom.Y)
    //            {
    //                Point temp = top;
    //                top = bottom;
    //                bottom = temp;
    //            }
    //            dy = bottom.Y - top.Y;
    //            if (dy != 0)
    //            {
    //                edgeTable[edgeCount] = new Edge(top.Y, bottom.Y, top.X << 8, (((bottom.X - top.X) << 8) / dy));
    //                ymax = Math.Max(ymax, bottom.Y);
    //                ++edgeCount;
    //            }
    //        }
    //        // Sort edge table by miny
    //        for (int i = 0; i < edgeCount - 1; ++i)
    //        {
    //            int min = i;
    //            for (int j = i + 1; j < edgeCount; ++j)
    //            {
    //                if (edgeTable[j].miny < edgeTable[min].miny)
    //                {
    //                    min = j;
    //                }
    //            }
    //            if (min != i)
    //            {
    //                Edge temp = edgeTable[min];
    //                edgeTable[min] = edgeTable[i];
    //                edgeTable[i] = temp;
    //            }
    //        }
    //        // Compute how many ScanLines we will be emitting
    //        int scanCount = 0;
    //        int activeLow = 0;
    //        int activeHigh = 0;
    //        int yscan1 = edgeTable[0].miny;
    //        // we assume that edgeTable[0].miny == yscan
    //        while (activeHigh < edgeCount - 1 &&
    //               edgeTable[activeHigh + 1].miny == yscan1)
    //        {
    //            ++activeHigh;
    //        }
    //        while (yscan1 <= ymax)
    //        {
    //            // Find new edges where yscan == miny
    //            while (activeHigh < edgeCount - 1 &&
    //                   edgeTable[activeHigh + 1].miny == yscan1)
    //            {
    //                ++activeHigh;
    //            }
    //            int count = 0;
    //            for (int i = activeLow; i <= activeHigh; ++i)
    //            {
    //                if (edgeTable[i].maxy > yscan1)
    //                {
    //                    ++count;
    //                }
    //            }
    //            scanCount += count / 2;
    //            ++yscan1;
    //            // Remove edges where yscan == maxy
    //            while (activeLow < edgeCount - 1 &&
    //                   edgeTable[activeLow].maxy <= yscan1)
    //            {
    //                ++activeLow;
    //            }
    //            if (activeLow > activeHigh)
    //            {
    //                activeHigh = activeLow;
    //            }
    //        }
    //        // Allocate ScanLines that we'll return
    //        ScanLine[] scans = new ScanLine[scanCount];
    //        // Active Edge Table (AET): it is indices into the Edge Table (ET)
    //        int[] active = new int[edgeCount];
    //        int activeCount = 0;
    //        int yscan2 = edgeTable[0].miny;
    //        int scansIndex = 0;
    //        // Repeat until both the ET and AET are empty
    //        while (yscan2 <= ymax)
    //        {
    //            // Move any edges from the ET to the AET where yscan == miny
    //            for (int i = 0; i < edgeCount; ++i)
    //            {
    //                if (edgeTable[i].miny == yscan2)
    //                {
    //                    active[activeCount] = i;
    //                    ++activeCount;
    //                }
    //            }
    //            // Sort the AET on x
    //            for (int i = 0; i < activeCount - 1; ++i)
    //            {
    //                int min = i;
    //                for (int j = i + 1; j < activeCount; ++j)
    //                {
    //                    if (edgeTable[active[j]].x < edgeTable[active[min]].x)
    //                    {
    //                        min = j;
    //                    }
    //                }
    //                if (min != i)
    //                {
    //                    int temp = active[min];
    //                    active[min] = active[i];
    //                    active[i] = temp;
    //                }
    //            }
    //            // For each pair of entries in the AET, fill in pixels between their info
    //            for (int i = 0; i < activeCount; i += 2)
    //            {
    //                Edge el = edgeTable[active[i]];
    //                Edge er = edgeTable[active[i + 1]];
    //                int startx = (el.x + 0xff) >> 8; // ceil(x)
    //                int endx = er.x >> 8;      // floor(x)
    //                scans[scansIndex] = new ScanLine(startx, yscan2, endx - startx);
    //                ++scansIndex;
    //            }
    //            ++yscan2;
    //            // Remove from the AET any edge where yscan == maxy
    //            int k = 0;
    //            while (k < activeCount && activeCount > 0)
    //            {
    //                if (edgeTable[active[k]].maxy == yscan2)
    //                {
    //                    // remove by shifting everything down one
    //                    for (int j = k + 1; j < activeCount; ++j)
    //                    {
    //                        active[j - 1] = active[j];
    //                    }
    //                    --activeCount;
    //                }
    //                else
    //                {
    //                    ++k;
    //                }
    //            }
    //            // Update x for each entry in AET
    //            for (int i = 0; i < activeCount; ++i)
    //            {
    //                edgeTable[active[i]].x += edgeTable[active[i]].dxdy;
    //            }
    //        }
    //        return scans;
    //    }
    //    private struct Edge
    //    {
    //        public int miny;   // int
    //        public int maxy;   // int
    //        public int x;      // fixed point: 24.8
    //        public int dxdy;   // fixed point: 24.8
    //        public Edge(int miny, int maxy, int x, int dxdy)
    //        {
    //            this.miny = miny;
    //            this.maxy = maxy;
    //            this.x = x;
    //            this.dxdy = dxdy;
    //        }
    //    }
    //}
    //public sealed class BitVector2D :
    //    ICloneable
    //{
    //    private BitArray bitArray;
    //    private int width;
    //    private int height;
    //    public int Width
    //    {
    //        get
    //        {
    //            return width;
    //        }
    //    }
    //    public int Height
    //    {
    //        get
    //        {
    //            return height;
    //        }
    //    }
    //    public bool IsEmpty
    //    {
    //        get
    //        {
    //            return (width == 0) || (height == 0);
    //        }
    //    }
    //    public bool this[int x, int y]
    //    {
    //        get
    //        {
    //            CheckBounds(x, y);
    //            return bitArray[x + (y * width)];
    //        }
    //        set
    //        {
    //            CheckBounds(x, y);
    //            bitArray[x + (y * width)] = value;
    //        }
    //    }
    //    public bool this[System.Drawing.Point pt]
    //    {
    //        get
    //        {
    //            CheckBounds(pt.X, pt.Y);
    //            return bitArray[pt.X + (pt.Y * width)];
    //        }
    //        set
    //        {
    //            CheckBounds(pt.X, pt.Y);
    //            bitArray[pt.X + (pt.Y * width)] = value;
    //        }
    //    }
    //    public BitVector2D(int width, int height)
    //    {
    //        this.width = width;
    //        this.height = height;
    //        this.bitArray = new BitArray(width * height, false);
    //    }
    //    private void CheckBounds(int x, int y)
    //    {
    //        if (x >= width || y >= height || x < 0 || y < 0)
    //        {
    //            throw new ArgumentOutOfRangeException();
    //        }
    //    }
    //    public void Clear(bool newValue)
    //    {
    //        bitArray.SetAll(newValue);
    //    }
    //    public bool Get(int x, int y)
    //    {
    //        return this[x, y];
    //    }
    //    public bool GetUnchecked(int x, int y)
    //    {
    //        return bitArray[x + (y * width)];
    //    }
    //    public void Set(int x, int y, bool newValue)
    //    {
    //        this[x, y] = newValue;
    //    }
    //    public void Set(Point pt, bool newValue)
    //    {
    //        Set(pt.X, pt.Y, newValue);
    //    }
    //    public void Set(Rectangle rect, bool newValue)
    //    {
    //        for (int y = rect.Top; y < rect.Bottom; ++y)
    //        {
    //            for (int x = rect.Left; x < rect.Right; ++x)
    //            {
    //                Set(x, y, newValue);
    //            }
    //        }
    //    }
    //    public void SetUnchecked(Rectangle rect, bool newValue)
    //    {
    //        for (int y = rect.Top; y < rect.Bottom; ++y)
    //        {
    //            for (int x = rect.Left; x < rect.Right; ++x)
    //            {
    //                SetUnchecked(x, y, newValue);
    //            }
    //        }
    //    }
    //    public void Set(ScanLine scan, bool newValue)
    //    {
    //        int x = scan.X;
    //        while (x < scan.X + scan.Length)
    //        {
    //            Set(x, scan.Y, newValue);
    //            ++x;
    //        }
    //    }
    //    public void SetUnchecked(ScanLine scan, bool newValue)
    //    {
    //        int x = scan.X;
    //        while (x < scan.X + scan.Length)
    //        {
    //            SetUnchecked(x, scan.Y, newValue);
    //            ++x;
    //        }
    //    }
    //    public void SetUnchecked(int x, int y, bool newValue)
    //    {
    //        bitArray[x + (y * width)] = newValue;
    //    }
    //    public void Invert(int x, int y)
    //    {
    //        Set(x, y, !Get(x, y));
    //    }
    //    public void Invert(Point pt)
    //    {
    //        Invert(pt.X, pt.Y);
    //    }
    //    public void Invert(Rectangle rect)
    //    {
    //        for (int y = rect.Top; y < rect.Bottom; ++y)
    //        {
    //            for (int x = rect.Left; x < rect.Right; ++x)
    //            {
    //                Invert(x, y);
    //            }
    //        }
    //    }
    //    public void Invert(ScanLine scan)
    //    {
    //        int x = scan.X;
    //        while (x < scan.X + scan.Length)
    //        {
    //            Invert(x, scan.Y);
    //            ++x;
    //        }
    //    }
    //    #region ICloneable Members
    //    public object Clone()
    //    {
    //        return MemberwiseClone();
    //    }
    //    #endregion
    //}
    //public struct ScanLine
    //{
    //    private int m_X;
    //    private int m_Y;
    //    private int m_Width;
    //    public int Width
    //    {
    //        get { return m_Width; }
    //    }
    //    public int Y
    //    {
    //        get { return m_Y; }
    //    }
    //    public int Length
    //    {
    //        get { return m_Width; }
    //    }
    //    public int X
    //    {
    //        get { return m_X; }
    //    }
    //    public ScanLine(int x, int y, int w)
    //    {
    //        this.m_X = x;
    //        this.m_Y = y;
    //        this.m_Width = w;
    //    }
    //    public override string ToString()
    //    {
    //        return string.Format("{0}x{1};{2}", X, Y, Width);
    //    }
    //}
    //public sealed class Vector<T>
    //{
    //    private int count = 0;
    //    private T[] array;
    //    public Vector()
    //        : this(10)
    //    {
    //    }
    //    public Vector(int capacity)
    //    {
    //        this.array = new T[capacity];
    //    }
    //    public Vector(IEnumerable<T> copyMe)
    //    {
    //        foreach (T t in copyMe)
    //        {
    //            Add(t);
    //        }
    //    }
    //    public void Add(T pt)
    //    {
    //        if (this.count >= this.array.Length)
    //        {
    //            Grow(this.count + 1);
    //        }
    //        this.array[this.count] = pt;
    //        ++this.count;
    //    }
    //    public void Insert(int index, T item)
    //    {
    //        if (this.count >= this.array.Length)
    //        {
    //            Grow(this.count + 1);
    //        }
    //        ++this.count;
    //        for (int i = this.count - 1; i >= index + 1; --i)
    //        {
    //            this.array[i] = this.array[i - 1];
    //        }
    //        this.array[index] = item;
    //    }
    //    public void Clear()
    //    {
    //        this.count = 0;
    //    }
    //    public T this[int index]
    //    {
    //        get
    //        {
    //            return Get(index);
    //        }
    //        set
    //        {
    //            Set(index, value);
    //        }
    //    }
    //    public T Get(int index)
    //    {
    //        if (index < 0 || index >= this.count)
    //        {
    //            throw new ArgumentOutOfRangeException("index", index, "0 <= index < count");
    //        }
    //        return this.array[index];
    //    }
    //    //public unsafe T GetUnchecked(int index)
    //    //{
    //    //    return this.array[index];
    //    //}
    //    public void Set(int index, T pt)
    //    {
    //        if (index < 0)
    //        {
    //            throw new ArgumentOutOfRangeException("index", index, "0 <= index");
    //        }
    //        if (index >= this.array.Length)
    //        {
    //            Grow(index + 1);
    //        }
    //        this.array[index] = pt;
    //    }
    //    public int Count
    //    {
    //        get
    //        {
    //            return this.count;
    //        }
    //    }
    //    private void Grow(int min)
    //    {
    //        int newSize = this.array.Length;
    //        if (newSize <= 0)
    //        {
    //            newSize = 1;
    //        }
    //        while (newSize < min)
    //        {
    //            newSize = 1 + ((newSize * 10) / 8);
    //        }
    //        T[] replacement = new T[newSize];
    //        for (int i = 0; i < this.count; i++)
    //        {
    //            replacement[i] = this.array[i];
    //        }
    //        this.array = replacement;
    //    }
    //    [Obsolete("Use ToArray() instead", true)]
    //    public T[] GetArray()
    //    {
    //        return ToArray();
    //    }
    //    public T[] ToArray()
    //    {
    //        T[] ret = new T[this.count];
    //        for (int i = 0; i < this.count; i++)
    //        {
    //            ret[i] = this.array[i];
    //        }
    //        return ret;
    //    }
    //    //public unsafe T[] UnsafeArray
    //    //{
    //    //    get
    //    //    {
    //    //        return this.array;
    //    //    }
    //    //}
    //    /// <summary>
    //    /// Gets direct access to the array held by the Vector.
    //    /// The caller must not modify the array.
    //    /// </summary>
    //    /// <param name="array">The array.</param>
    //    /// <param name="length">The actual number of items stored in the array. This number will be less than or equal to array.Length.</param>
    //    /// <remarks>This method is supplied strictly for performance-critical purposes.</remarks>
    //    //public unsafe void GetArrayReadOnly(out T[] arrayResult, out int lengthResult)
    //    //{
    //    //    arrayResult = this.array;
    //    //    lengthResult = this.count;
    //    //}
    //}
}

