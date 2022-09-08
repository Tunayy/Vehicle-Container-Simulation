using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paycore_patika_HW3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Vehicle_Controller : ControllerBase
    {
        

        private readonly ISession session;
        private ITransaction transaction;
        public Vehicle_Controller(ISession session)
        {
            this.session = session;
        }


        [HttpGet ("All Data")]
        public List<Vehicle> Get()
        {
            var response = session.Query<Vehicle>().ToList();
            return response;
        }

        [HttpGet("Data For İd {id}")]
        public Vehicle GetbyId(long id)
        {
            var response = session.Query<Vehicle>().Where(x => x.Id == id).FirstOrDefault();
            return response;
        }

        [HttpPost ("Post For Vehicle")]
        public void Post([FromBody] Vehicle request)
        {

            using (var transaction = session.BeginTransaction())
            {
                var Vehicle = new Vehicle();
                Vehicle.Id = request.Id;
                Vehicle.VehicleName = request.VehicleName;
                Vehicle.VehiclePlate = request.VehiclePlate;


                session.Save(Vehicle);
                transaction.Commit();
            }

        }

        [HttpPut ("Put For Vehicle")]
        public ActionResult<Vehicle> Put([FromBody] Vehicle request)
        {
            using (var transaction = session.BeginTransaction())
            {
                Vehicle vehicle = session.Query<Vehicle>().Where(x => x.Id == request.Id).FirstOrDefault();
                if (vehicle == null)
                {
                    return NotFound();
                }
                vehicle.VehicleName = request.VehicleName;
                vehicle.VehiclePlate = request.VehiclePlate;
                session.Update(vehicle);
                transaction.Commit();
                return Ok();
            }


        }


        [HttpDelete("Delete For Vehicle")]
        public ActionResult Delete(long id)
        {
            var vehicle = session.Query<Vehicle>().FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
                return BadRequest();

            var containers = session.Query<Container>().Where(x => x.VehicleId == id).ToList();

            try
            {
                session.BeginTransaction();
                for (int i = 0; i < containers.Count; i++)
                {
                    session.Delete(containers[i]);
                }
                session.Delete(vehicle);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                transaction.Dispose();
            }


            return Ok();
        }

    }
}
