using TechnicalTest.Core.Models;

namespace TechnicalTest.API.DTOs
{
    public class CalculateCoordinatesResponseDTO
    {
        public List<Coordinate> Coordinates { get; set; }

        //public class Coordinate
        //{
        //    public int X { get; set; }
        //    public int Y { get; set; }

        //    public Coordinate(int x, int y)
        //    {
        //        X = x;
        //        Y = y;
        //    }
        //}

        // constructor with parameters
        public CalculateCoordinatesResponseDTO(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
        }

        public CalculateCoordinatesResponseDTO()
        {
            Coordinates = new List<Coordinate>();
        }

    }
}
