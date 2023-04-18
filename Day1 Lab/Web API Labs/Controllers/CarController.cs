using Microsoft.AspNetCore.Mvc;
using web_api_labs.middlewares;
using Web_API_Labs.Filters;

using Web_API_Labs.Models;

namespace Web_API_Labs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequestCount]    //  =====> filter to coun the number of requests
    public class CarController : Controller
    {
        // static list of csrs
        private static List<Car> Cars = new();
        private readonly ILogger<CarController> logger;
     

      //inject Ilogger service
        public CarController(ILogger<CarController> logger)
        {
            this.logger = logger;
          
        }

        [HttpGet]
        [Route("getrequestCount")]
        public ActionResult getcount()   // this method for getting the total number of requests
        {
            var res = new NumberResponse { number = RequestCountAttribute.getRequestCount() };
            return Json(res);
        }

        [HttpGet]
        public ActionResult GetAll()   
        {
            return Ok(Cars );
        }

        [HttpGet]
        [Route("{Id}")]
        public ActionResult GetById(int Id)
        {
            var car = Cars.FirstOrDefault(c=>c.Id==Id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }

        [HttpPost]
        [Route("V1")]
        public ActionResult Add_v1(Car car)
        {
            logger.LogCritical("Add route");
            car.Id = Cars.Count+ 1;
            car.Type = "Gas";
            Cars.Add(car);

            return CreatedAtAction(actionName: "GetById", routeValues: new { id = car.Id },
                new { message = "item added successfully" }
                );
        }

        [HttpPost]
        [Route("V2")]
        [CarTypeValidation]
        public ActionResult Add_v2(Car car)
        {
            logger.LogCritical("Add route");
            car.Id = Cars.Count + 1;
            Cars.Add(car);

            return CreatedAtAction(actionName: "GetById", routeValues: new { id = car.Id },
                new { message = "item added successfully" }
                );
        }



        [HttpPut]
        [Route("{Id}")]
        public ActionResult Update(Car car , int Id)
        {
            if (car.Id == null)
            {
              return  BadRequest();
            }
            Car? UpdatedCar = Cars.FirstOrDefault(car=>car.Id==Id);  

            if (UpdatedCar is null)
            {
                return NotFound();   
            }

            UpdatedCar.Name = car.Name;
            UpdatedCar.ProductionDate = car.ProductionDate;

           return NoContent();
        }



        [HttpDelete]
        [Route("{Id}")]
        public ActionResult Delete(int Id)
        {   
            var deletedCar = Cars.FirstOrDefault(c => c.Id == Id);

            if (deletedCar is null)
            {
                return NotFound();
            }

            Cars.Remove(deletedCar);
            return NoContent();  // ststus 204
 
        }



    }
}
