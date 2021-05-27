using System;

namespace Circle_Alg
{
    public class Point
    {
        float x, y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }

    }
    public class Circle
    {

        Point center;
        float radius;
        public Circle(Point c, float r)
        {
            this.center = c;
            this.radius = r;
        }
        public float getR()
        {
            return this.radius;
        }
        public Point getC()
        {
            return this.center;
        }
        public Circle getCircle()
        {
            return new Circle(this.center, this.radius);
        }
    };

    public class MinCircle
    {
        public static float Dist(Point a, Point b)
        {
            float x2 = (a.getX() - b.getX()) * (a.getX() - b.getX());
            float y2 = (a.getY() - b.getY()) * (a.getY() - b.getY());
            return (float)Math.Sqrt(x2 + y2);
        }

        public static Point MinCenter(float x_min, float x_max, float y_min, float y_max)
        {
            float x_center = (x_min + x_max) / 2;
            float y_center = (y_min + y_max) / 2;
            return new Point(x_center, y_center);
        }
        public static Circle from3Points(Point a, Point b, Point c)
        {
            // find the circumcenter of the triangle a,b,c

            Point mAB = new Point((a.getX() + b.getX()) / 2, (a.getY() + b.getY()) / 2); // mid point of line AB
            float slopAB = (b.getY() - a.getY()) / (b.getX() - a.getX()); // the slop of AB
            float pSlopAB = -1 / slopAB; // the perpendicular slop of AB
                                         // pSlop equation is:
                                         // y - mAB.y = pSlopAB * (x - mAB.x) ==> y = pSlopAB * (x - mAB.x) + mAB.y

            Point mBC = new Point((b.getX() + c.getX()) / 2, (b.getY() + c.getY()) / 2); // mid point of line BC
            float slopBC = (c.getY() - b.getY()) / (c.getX() - b.getX()); // the slop of BC
            float pSlopBC = -1 / slopBC; // the perpendicular slop of BC
                                         // pSlop equation is:
                                         // y - mBC.y = pSlopBC * (x - mBC.x) ==> y = pSlopBC * (x - mBC.x) + mBC.y

            float x = (-pSlopBC * mBC.getX() + mBC.getY() + pSlopAB * mAB.getX() - mAB.getY()) / (pSlopAB - pSlopBC);
            float y = pSlopAB * (x - mAB.getX()) + mAB.getY();
            Point center = new Point(x, y);
            float R = Dist(center, a);

            return new Circle(center, R);
        }
    }
}
