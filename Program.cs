using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    #region
    abstract class Figure : IComparable
    {
        public abstract double Area();
        string _type;
        public string type
        {
            get { return this._type; }
            protected set { this._type = value; }
        }

        public override string ToString()
        {
            return this.type + " area = " + this.Area().ToString();
        }
        public int CompareTo(object o)
        {
            Figure f = (Figure)o;

            if (this.Area() < f.Area())
                return -1;
            else if (this.Area() == f.Area())
                return 0;
            else
                return 1;
        }
    }

    class Rectangle : Figure, IPrint
    {
        public Rectangle(double w, double h) { this.width = w; this.height = h; this.type = "Rectangle"; }

        protected double _width = 0;
        public double width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double _height = 0;
        public double height
        {
            get { return _height; }
            set { _height = value; }
        }
       
        public override double Area()
        {
            return this.width * this.height;
        }
   
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }

  class Square : Rectangle, IPrint
  {
      public Square(double a) : base(a, a) { this.type = "Square"; }

      public void Print()
      {
          Console.WriteLine(ToString());
      }
  }
    
    class Circle : Figure, IPrint
    {
        public Circle(double r) { this.radius = r; this.type = "Circle"; }

        private double _radius = 0;
        public double radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public override double Area()
        {
            return this.radius * this.radius * Math.PI;
        }
        
        public void Print()
        {
            Console.WriteLine(ToString());
        }
    }
    #region
    public interface ISparseMatrixCheckEmpty<T>
    {
        T getEmptyElement();
        bool checkEmptyElement(T element);
    }

    class FigureSparseMatrixCheckEmpty : ISparseMatrixCheckEmpty<Figure>
    {
        public Figure getEmptyElement()
        {
            return null;
        }
        public bool checkEmptyElement(Figure element)
        {
            bool result = false;
            if (element == null)
            {
                result = true;
            }
            return result;
        }
    }


    public class SparseMatrix<T>
    {
        Dictionary<string, T> _matrix = new Dictionary<string, T>();
        int maxX;

        int maxY;
       
        int maxZ;
       
        ISparseMatrixCheckEmpty<T> checkEmpty;

        public SparseMatrix(int x, int y, int z, ISparseMatrixCheckEmpty<T> checkEmptyParam)
        {
            this.maxX = x;
            this.maxY = y;
            this.maxZ = z;
            this.checkEmpty = checkEmptyParam;
        }

        void CheckBounds(int x, int y, int z)
        {
            if (x < 0 || x >= this.maxX)
            {
                throw new ArgumentOutOfRangeException("x", "x=" + x + " выход за границы");
            }
            if (y < 0 || y >= this.maxY)
            {
                throw new ArgumentOutOfRangeException("y", "y=" + y + " выход за границы");
            }
            if (z < 0 || z >= this.maxZ)
            {
                throw new ArgumentOutOfRangeException("z", "z=" + z + " выход за границы");
            }
        }
   
        string DictKey(int x, int y, int z)
        {
            return x.ToString() + "_" + y.ToString() + "_" + z.ToString();
        }
       
        public T this[int x, int y, int z]
        {
            set
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);
                this._matrix.Add(key, value);
            }
            get
            {
                CheckBounds(x, y, z);
                string key = DictKey(x, y, z);
                if (this._matrix.ContainsKey(key))
                {
                    return this._matrix[key];
                }
                else
                {
                    return this.checkEmpty.getEmptyElement();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            for (int k = 0; k < this.maxZ; k++)
            {
                b.Append("\nz = " + (k + 1).ToString() + ":\n");
                for (int j = 0; j < this.maxY; j++)
                {
                    b.Append("[");
                    for (int i = 0; i < this.maxX; i++)
                    {
                        if (i > 0)
                            b.Append("\t");
                        if (!this.checkEmpty.checkEmptyElement(this[i, j, k]))
                            b.Append(this[i, j, k].ToString());
                        else
                            b.Append(" * ");
                    }
                    b.Append("]\n");
                }
            }
            return b.ToString();
        }

    }
    #endregion
    interface IPrint
    {
        void Print();
    }

    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle obj = new Rectangle(1, 5);
            Rectangle obj3 = new Rectangle(4, 5);
            Rectangle obj4 = new Rectangle(1, 1);
            Circle obj1 = new Circle(2);
            Square obj2 = new Square(3);
            ArrayList collect = new ArrayList();
            collect.Add(obj);
            collect.Add(obj2);
            collect.Add(obj3);
            collect.Add(obj4);
            collect.Add(obj1);
            Rectangle[] test = new Rectangle[] { obj, obj3, obj4 };
            Console.WriteLine("\nArray list before sorting \n");
            foreach (var x in collect)
                Console.WriteLine("{0} ", x);
            Array.Sort(test);
            collect.Sort();
            Console.WriteLine("\nArray list after sorting \n");
            foreach (var x in collect)
                Console.WriteLine("{0} ", x);
            
            List<Figure> lf = new List<Figure>();
            
            
            lf.Add(obj);
            lf.Add(obj2);
            lf.Add(obj3);
            lf.Add(obj4);
            lf.Add(obj1);
            Console.WriteLine("\nlist<figure> before sorting \n");
            foreach (var x in lf)
                Console.WriteLine("{0} ", x);
            lf.Sort();
            Console.WriteLine("\nlist<figure> after sorting \n");
            foreach (var x in lf)
                Console.WriteLine("{0} ", x);
            Console.Read();
        }
    }
}

