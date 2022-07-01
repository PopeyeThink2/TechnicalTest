using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.Core.Services
{
    public class ShapeService : IShapeService
    {
        public Shape ProcessTriangle(Grid grid, GridValue gridValue)
        {
            // TODO: Calculate the coordinates.
            Coordinate TopLeftVertex, OuterVertex, BottomRightVertex;
            int col = gridValue.Column, row = gridValue.GetNumericRow(), size = grid.Size;
            // 2 types of triangles: right-angle on left or right
            if (col % 2 != 0)
            {
                // odd columns are left triangles
                // D5, row = 4, col = 5 : (20, 30), new(20, 40), new Coordinate(30, 40)
                TopLeftVertex = new Coordinate(col / 2 * size, (row - 1) * size);
                OuterVertex = new Coordinate(col / 2 * size, row * size);
                BottomRightVertex = new Coordinate((col / 2 + 1) * size, row * size);
            }
            else
            {
                // even columns are right triangles
                // D6, row = 4, col = 6: (20, 30), new(30, 30), new Coordinate(30, 40)
                TopLeftVertex = new((col - 1) / 2 * size, (row - 1) * size);
                OuterVertex = new(col / 2 * size, (row - 1) * size);
                BottomRightVertex = new(col / 2 * size, row * size);
            }
            return new Shape(new List<Coordinate>
            {
                TopLeftVertex,
                OuterVertex,
                BottomRightVertex
            });
        }

        public GridValue ProcessGridValueFromTriangularShape(Grid grid, Triangle triangle)
        {
            // TODO: Calculate the grid value.
            Coordinate OuterVertex = triangle.OuterVertex, BottomRightVertex = triangle.BottomRightVertex;
            // OuterVertex as the base point, compare BottomRightVertex decided whether left or right triangle
            // TopLeftVertex also fine, TopLeft.X = Outer.X -> Left, TopLeft.Y = Outer.Y -> right
            int row = OuterVertex.Y / grid.Size, col = OuterVertex.X / grid.Size * 2;
            // left triangle
            if (BottomRightVertex.Y == OuterVertex.Y)
            {
                col++;
            }
            // right triangle
            if (BottomRightVertex.X == OuterVertex.X)
            {
                row++;
            }
            return new GridValue(row, col);
        }
    }
}