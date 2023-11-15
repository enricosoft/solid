// "Open for extension" means we must design our module/class so that the new functionality can be added only when new requirements are generated.
// "Closed for modification" means we have already developed a class, and it has gone through unit testing. We should then not alter it until we find bugs.


// WRONG CODE
using System;

public class Rectangle
{
    public double Height { get; set; }
    public double Wight { get; set; }
}

public class Circle
{
    public double Radius { get; set; }
}

public class AreaCalculator
{
    public double TotalArea(object[] arrObjects)
    {
        double area = 0;
        Rectangle objRectangle;
        Circle objCircle;
        foreach (var obj in arrObjects)
        {
            if (obj is Rectangle)
            {
                area += obj.Height * obj.Width;
            }
            else
            {
                objCircle = (Circle)obj;
                area += objCircle.Radius * objCircle.Radius * Math.PI;
            }
        }
        return area;
    }
}


//
// PROBLEM:
// Here we successfully introduced Circle into our app.
// We can add a Triangle and calculate its area by adding one more "if" block in the TotalArea method of AreaCalculator.
// But every time we introduce a new shape, we must alter the TotalArea method. So the AreaCalculator class is not closed for modification.
//



// CORRECT CODE
public abstract class Shape
{
    public abstract double Area();
}

public class Rectangle : Shape
{
    public double Height { get; set; }
    public double Width { get; set; }
    public override double Area()
    {
        return Height * Width;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area()
    {
        return Radius * Radus * Math.PI;
    }
}

public class AreaCalculator
{
    public double TotalArea(Shape[] arrShapes)
    {
        double area = 0;
        foreach (var objShape in arrShapes)
        {
            area += objShape.Area();
        }
        return area;
    }
}