using Newtonsoft.Json;
using RozkladyBackend.Lib;
using RozkladyBackend.Models;
using RozkladyBackend.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RozkladyBackend.Controllers
{
    public partial class AjaxController : Controller
    {
        public void AddOrEditStop(Stop stop)
        {
            using (BackendContext db = new BackendContext())
            {
                Stop existingStop = db.Stops.SingleOrDefault(s => s.Id == stop.Id);

                if (existingStop == null)
                {
                    db.Stops.Add(stop);
                }
                else
                {
                    existingStop.Description = stop.Description;
                    existingStop.Name = stop.Name;
                    existingStop.Latitude = stop.Latitude;
                    existingStop.Longitude = stop.Longitude;
                }
                db.SaveChanges();
            }
        }

        public JsonCamelCaseResult GetStop(int stopId)
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Stops.Single(s => s.Id == stopId));
            }
        }

        public JsonCamelCaseResult ListStops()
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Stops.OrderBy(s => s.Name).ToList());
            }
        }

        public void RemoveStop(int stopId)
        {
            using (BackendContext db = new BackendContext())
            {
                Stop stop = db.Stops.Single(s => s.Id == stopId);
                db.Stops.Remove(stop);
                db.SaveChanges();
            }
        }

    }
}