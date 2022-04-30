using IGK.ICore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.PathFitter
{

    static class Numerical {
        public const float EPSILON = 1e-12f;

        public static bool isZero(float val)
        {
            return val >= -EPSILON && val <= EPSILON;
        }
    }
    struct CorePathPoint
    {
        internal float X { get; set; }
        internal float Y { get; set; }
        internal float Angle { get; set; }

        public static readonly CorePathPoint Zero;

        static CorePathPoint() {
            Zero = new CorePathPoint();
        }

        internal CorePathPoint subtract(CorePathPoint point)
        {
            return new CorePathPoint()
            {
                X = this.X - point.X,
                Y = this.Y - point.Y
            };
        }

        internal float  getDistance(CorePathPoint pt2)
        {
            return (float)Math.Sqrt(Math.Pow((this.X - pt2.X), 2) + Math.Pow(this.Y - pt2.Y,2));
        }
        internal float getLength()
        {
            return (float)Math.Sqrt(( X *X) + (Y*Y));
        }

        internal CorePathPoint normalize(float dist)
        {
            var length = this.getLength();
            var scale = dist/length;
            return new CorePathPoint()
            {
                X = this.X *scale ,
                Y = this.Y *scale
            };
        }

        internal CorePathPoint add(CorePathPoint point)
        {
            return new CorePathPoint()
            {
                X = this.X + point.X,
                Y = this.Y + point.Y
            };
        }

        internal CorePathPoint negate()
        {
            return new CorePathPoint()
            {
                X = -this.X ,
                Y = -this.Y 
            };
        }

        internal CorePathPoint multiply(float v)
        {
            return new CorePathPoint()
            {
                X = this.X * v,
                Y = this.Y * v
            };
        }

        internal float dot(CorePathPoint point)
        {
            return (this.X * point.X) + (this.Y * point.Y);
        }
    }
    //implementation from paper.js
    class CorePathSegment 
    {
        internal bool _closed;
        internal CorePathSegment[] _segments;
        internal CorePathPoint point;
        internal int _def;
        internal CorePathPoint handleOut;
        internal CorePathPoint handleIn;
        

        ///<summary>
        ///public .ctr
        ///</summary>
        public CorePathSegment()
        {
            _segments = new CorePathSegment[0];
            point = CorePathPoint.Zero;
            _closed = false;
        }

        internal void AddPoint(float x, float y)
        {
            var s = new CorePathSegment()
            {
                point = new CorePathPoint()
                {
                    X = x,
                    Y = y
                },
                _def=1
            };
            if (_segments.Length == 0)
                _segments = new CorePathSegment[] { s };
            else
            {
                List<CorePathSegment> g = new List<CorePathSegment>(_segments);
                g.Add(s);
                _segments = g.ToArray();
            }
        }

        internal void MoveTo(float x, float y)
        {
            var s = new CorePathSegment()
            {
                point = new CorePathPoint()
                {
                    X = x,
                    Y = y
                },
                _def =0
            };

            if (_segments.Length == 0)
                _segments = new CorePathSegment[] {s};
            else {
                List<CorePathSegment> g = new List<CorePathSegment>(_segments);
                g.Add(s);
                _segments= g.ToArray();
            }
        }
    }

    static    class CorePathExtension {
        public static void Add(this List<CorePathSegment> t, CorePathPoint p) {
            t.Add(new CorePathSegment() {
                point = p
            });
        }
        public static Array slice(this Array t, int start=1) {
            Array tt = null;// t.Clone() as Array;//  new Array();
            tt = Array.CreateInstance(t.GetValue(0).GetType(), t.Length - start);
            for (int i = 0; i < tt.Length; i++)
            {
                tt.SetValue(t.GetValue(start + i), i);
            }
            //t.CopyTo(tt, start,);
            return tt;
        }
        public static bool equals(this CorePathPoint o, CorePathPoint d) {
            return false;
        }

        public static CorePathPoint clone(this CorePathPoint o)
        {
            return new CorePathPoint() { X =o.X, Y=o.Y};
        }
        public static bool push(this IList p, CorePathPoint d) {
            p.Add(d);
            return true;
        }
        public static bool unshift(this IList p, CorePathPoint d)
        {
            p.Insert(0, d);
            return true;
        }
        public static bool shift(this IList p) {
            if (p.Count == 0)
                return false;
            p.RemoveAt(0);
            return true;
        }
        public static bool pop(this IList p)
        {
            if (p.Count == 0)
                return false;
            p.RemoveAt(p.Count-1);
            return true;
        }
    }

    class CorePathErrorData {
        internal float error;
        internal int index;
    }
    class CorePathFitter
    {
        private bool closed;
        private List<CorePathPoint> points;
        public CorePathFitter(CorePathSegment path)
        {
            var points = this.points = new List<CorePathPoint>();
            var segments = path._segments;
            var closed = path._closed;
            CorePathPoint prev = CorePathPoint.Zero;
            for (int i = 0, l = segments.Length; i < l; i++)
            {
                var point = segments[i].point;
                if (!prev.equals(point))
                {
                    points.push(prev = point.clone());
                }
            }
            if (closed)
            {
                points.unshift(points[points.Count - 1]);
                points.push(points[1]);
            }
            this.closed = closed;
        }

        public CorePathSegment[] fit(float error)
        {
            var points = this.points;
            var Length = points.Count;
            var segments = new List<CorePathSegment>();
            if (Length > 0)
            {
                segments.Add(points[0]); // = [new Segment(points[0])];
                if (Length > 1)
                {
                    this.fitCubic(segments, error, 0, Length - 1,
                            points[1].subtract(points[0]),
                            points[Length - 2].subtract(points[Length - 1]));
                    if (this.closed)
                    {
                        segments.shift(); //.shift();
                        segments.pop();
                    }
                }
            }
            return segments.ToArray();
        }

        public void fitCubic(List<CorePathSegment> segments, float error, int first, int last, CorePathPoint tan1,
            CorePathPoint tan2)
        {
            var points = this.points;
            if ((last - first) == 1)
            {
                var pt1 = points[first];
                var pt2 = points[last];
                var dist = pt1.getDistance(pt2) / 3;
                this.addCurve(segments, new CorePathPoint[]{pt1, pt1.add(tan1.normalize(dist)),
                        pt2.add(tan2.normalize(dist)), pt2 });
                return;
            }
            var uPrime = this.chordLengthParameterize(first, last);
            var maxError = Math.Max(error, error * error);
            var split = 0;
            var parametersInOrder = true;
            for (var i = 0; i <= 4; i++)
            {
                var curve = this.generateBezier(first, last, uPrime, tan1, tan2);
                var max = this.findMaxError(first, last, curve, uPrime);
                if (max.error < error && parametersInOrder)
                {
                    this.addCurve(segments, curve);
                    return;
                }
                split = max.index;
                if (max.error >= maxError)
                    break;
                parametersInOrder = this.reparameterize(first, last, uPrime, curve);
                maxError = max.error;
            }
            var tanCenter = points[split - 1].subtract(points[split + 1]);
            this.fitCubic(segments, error, first, split, tan1, tanCenter);
            this.fitCubic(segments, error, split, last, tanCenter.negate(), tan2);
        }



        private void addCurve(List<CorePathSegment> segments, CorePathPoint[] curve)
        {
            var prev = segments[segments.Count - 1];//.Length - 1];
                                                    //prev.setHandleOut(curve[1].subtract(curve[0]));

            //make ite compatible with logic
            prev.handleOut = curve[1].subtract(curve[0]);
            //segments.push(new Segment(curve[3], curve[2].subtract(curve[3])));
            segments.Add(new CorePathSegment()
            {
                point = curve[3],
                handleIn = curve[2].subtract(curve[3])
            });
        }
        public CorePathPoint[] generateBezier(int first, int last, float[] uPrime, CorePathPoint tan1, CorePathPoint tan2)
        //public void generateBezier(int first, int last, int uPrime, float tan1,float tan2)
        {
            var epsilon = 1e-12f;
            //abs = Math.abs,
            var points = this.points;
            var pt1 = points[first];
            var pt2 = points[last];
            var C = new float[][] {
                new float[]{ 0,0},
                new float[]{ 0,0}
            }; //[[0, 0], [0, 0]],
            var X =new float[] {0, 0};
            var l = last - first + 1;

            for (var i = 0 ; i < l; i++)
            {
                var u = uPrime[i];
                var    t = 1 - u;
                var b = 3 * u * t;
                var b0 = t * t * t;
                var b1 = b * t;
                var b2 = b * u;
                var b3 = u * u * u;
                var a1 = tan1.normalize(b1);
                var a2 = tan2.normalize(b2);
                    var tmp = points[first + i]
                        .subtract(pt1.multiply(b0 + b1))
                        .subtract(pt2.multiply(b2 + b3));
                C[0][0] += a1.dot(a1);
                C[0][1] += a1.dot(a2);
                C[1][0] = C[0][1];
                C[1][1] += a2.dot(a2);
                X[0] += a1.dot(tmp);
                X[1] += a2.dot(tmp);
            }

            var detC0C1 = C[0][0] * C[1][1] - C[1][0] * C[0][1];
            var alpha1 = 0.0f;
            var alpha2 = 0.0f;

            if (Math.Abs(detC0C1) > epsilon)
            {
                var detC0X = C[0][0] * X[1] - C[1][0] * X[0];
                var detXC1 = X[0] * C[1][1] - X[1] * C[0][1];
                alpha1 = detXC1 / detC0C1;
                alpha2 = detC0X / detC0C1;
            }
            else
            {
                var c0 = C[0][0] + C[0][1];
                var c1 = C[1][0] + C[1][1];
                alpha1 = alpha2 = Math.Abs(c0) > epsilon ? X[0] / c0
                                : Math.Abs(c1) > epsilon ? X[1] / c1
                                : 0;
            }

            var segLength = pt2.getDistance(pt1);
            var eps = epsilon * segLength;
            var handle1 =CorePathPoint.Zero ;
            var handle2 =CorePathPoint.Zero;
            if (alpha1 < eps || alpha2 < eps)
            {
                alpha1 = alpha2 = segLength / 3;
            }
            else
            {
                var line = pt2.subtract(pt1);
                handle1 = tan1.normalize(alpha1);
                handle2 = tan2.normalize(alpha2);
                if (handle1.dot(line) - handle2.dot(line) > segLength * segLength)
                {
                    alpha1 = alpha2 = segLength / 3;
                    handle1 = handle2 = CorePathPoint.Zero;
                }
            }

            return new CorePathPoint[]{pt1,
                    pt1.add(!handle1.Equals(CorePathPoint.Zero) ? handle1:  tan1.normalize(alpha1)),
                    pt2.add(!handle2.Equals(CorePathPoint.Zero) ? handle2 : tan2.normalize(alpha2)),
                    pt2 };


            //return [pt1,
            //        pt1.add(handle1 || tan1.normalize(alpha1)),
            //        pt2.add(handle2 || tan2.normalize(alpha2)),
            //        pt2];
        }

	public bool reparameterize(int first, int last, float[] u,CorePathPoint[] curve)
        {
            for (var i = first; i <= last; i++)
            {
                u[i - first] = this.findRoot(curve, this.points[i], u[i - first]);
            }
            var l = u.Length;
            for (var i = 1; i < l; i++)
            {
                if (u[i] <= u[i - 1])
                    return false;
            }
            return true;
        }

        public float findRoot(CorePathPoint[] curve, CorePathPoint point, float u)
        {
            var curve1 = new CorePathPoint[3];
            var curve2 = new CorePathPoint[3];
            for (var i = 0; i <= 2; i++)
            {
                curve1[i] = curve[i + 1].subtract(curve[i]).multiply(3);
            }
            for (var i = 0; i <= 1; i++)
            {
                curve2[i] = curve1[i + 1].subtract(curve1[i]).multiply(2);
            }
            var pt = this.evaluate(3, curve, u);
            var pt1 = this.evaluate(2, curve1, u);
            var pt2 = this.evaluate(1, curve2, u);
            var diff = pt.subtract(point);
            var df = pt1.dot(pt1) + diff.dot(pt2);
            return Numerical.isZero(df) ? u : u - diff.dot(pt1) / df;
        }

        public CorePathPoint evaluate(int degree, Array curve, float t)
        {
            var tmp = (CorePathPoint[])curve.slice(0);
            for (var i = 1; i <= degree; i++)
            {
                for (var j = 0; j <= degree - i; j++)
                {
                    tmp[j] = tmp[j].multiply(1 - t).add(tmp[j + 1].multiply(t));
                }
            }
            return tmp[0];
        }

	public float[] chordLengthParameterize(int first, int last)
        {
            var u = new float [last - first + 1];
            for (var i = first + 1; i <= last; i++)
            {
                u[i - first] = u[i - first - 1]
                        + this.points[i].getDistance(this.points[i - 1]);
            }
            for (int i = 1, m = last - first; i <= m; i++)
            {
                u[i] /= u[m];
            }
            return u;
        }

        public CorePathErrorData findMaxError(int first, int last, CorePathPoint[] curve, float[] u)
        {
            var index = (int) Math.Floor((last - first + 1) / 2.0f);
            float maxDist = 0;
            for (int i = first + 1; i < last; i++)
            {
                var P = this.evaluate(3, curve, u[i - first]);
                var v = P.subtract(this.points[i]);
                var dist = v.X * v.X + v.Y * v.Y;
                if (dist >= maxDist)
                {
                    maxDist = dist;
                    index = i;
                }
            }
            return new CorePathErrorData()
            {
                error = maxDist,
                index = index
            };

        }
    }

     
}
