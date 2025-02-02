﻿using Microsoft.AspNetCore.Mvc;
using TechnicalTest.API.DTOs;
using TechnicalTest.Core;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Core.Models;

namespace TechnicalTest.API.Controllers
{
    /// <summary>
    /// Shape Controller which is responsible for calculating coordinates and grid value.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ShapeController : ControllerBase
    {
        private readonly IShapeFactory _shapeFactory;

        /// <summary>
        /// Constructor of the Shape Controller.
        /// </summary>
        /// <param name="shapeFactory"></param>
        public ShapeController(IShapeFactory shapeFactory)
        {
            _shapeFactory = shapeFactory;
        }

        /// <summary>
        /// Calculates the Coordinates of a shape given the Grid Value.
        /// </summary>
        /// <param name="calculateCoordinatesRequest"></param>   
        /// <returns>A Coordinates response with a list of coordinates.</returns>
        /// <response code="200">Returns the Coordinates response model.</response>
        /// <response code="400">If an error occurred while calculating the Coordinates.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Shape))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateCoordinates")]
        [HttpPost]
        public IActionResult CalculateCoordinates([FromBody]CalculateCoordinatesDTO calculateCoordinatesRequest)
        {
            
            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shapeEnum = (ShapeEnum)calculateCoordinatesRequest.ShapeType;
            if (!shapeEnum.Equals(ShapeEnum.Triangle))
            {
                return BadRequest("ERROR: Not a triangle!");
            }

            // TODO: Call the Calculate function in the shape factory.
            Shape? shape = _shapeFactory.CalculateCoordinates(shapeEnum, new Grid(calculateCoordinatesRequest.Grid.Size), new GridValue(calculateCoordinatesRequest.GridValue));

            // TODO: Return BadRequest with error message if the calculate result is null
            if (shape == null)
            {
                return BadRequest("ERROR: Shape is NULL!");
            }

            // TODO: Create ResponseModel with Coordinates and return as OK with responseModel.
            return Ok(new CalculateCoordinatesResponseDTO(shape.Coordinates));
        }

        /// <summary>
        /// Calculates the Grid Value of a shape given the Coordinates.
        /// </summary>
        /// <remarks>
        /// A Triangle Shape must have 3 vertices, in this order: Top Left Vertex, Outer Vertex, Bottom Right Vertex.
        /// </remarks>
        /// <param name="gridValueRequest"></param>   
        /// <returns>A Grid Value response with a Row and a Column.</returns>
        /// <response code="200">Returns the Grid Value response model.</response>
        /// <response code="400">If an error occurred while calculating the Grid Value.</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GridValue))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("CalculateGridValue")]
        [HttpPost]
        public IActionResult CalculateGridValue([FromBody]CalculateGridValueDTO gridValueRequest)
        {
            // TODO: Get the ShapeEnum and if it is default (ShapeEnum.None) or not triangle, return BadRequest as only Triangle is implemented yet.
            ShapeEnum shapeEnum = (ShapeEnum)gridValueRequest.ShapeType;
            if (!shapeEnum.Equals(ShapeEnum.Triangle))
            {
                return BadRequest("ERROR: Not a triangle!");
            }

            // TODO: Create new Shape with coordinates based on the parameters from the DTO.
            List<Coordinate> coordinates = new();
            foreach (Vertex v in gridValueRequest.Vertices)
            {
                coordinates.Add(new(v.x, v.y));
            }
            Shape shape = new(coordinates);

            // TODO: Call the function in the shape factory to calculate grid value.
            GridValue? gridValue = _shapeFactory.CalculateGridValue(shapeEnum, new Grid(gridValueRequest.Grid.Size), shape);

            // TODO: If the GridValue result is null then return BadRequest with an error message.
            if (gridValue == null)
            {
                return BadRequest("ERROR: GridValue is NULL!");
            }

            // TODO: Generate a ResponseModel based on the result and return it in Ok();
            return Ok(new CalculateGridValueResponseDTO(gridValue.Row, gridValue.Column));
        }
    }
}
