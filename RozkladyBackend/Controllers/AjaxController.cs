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
        public void AddOrEditLine(Line line)
        {
            using (BackendContext db = new BackendContext())
            {
                Line existsingLine = db.Lines.SingleOrDefault(l => l.Id == line.Id);

                if (existsingLine == null)
                {
                    db.Lines.Add(line);
                }
                else
                {
                    existsingLine.Name = line.Name;
                    existsingLine.Variants = line.Variants;
                }
                db.SaveChanges();
            }
        }
        public JsonCamelCaseResult GetLine(int lineId)
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Lines.Single(s => s.Id == lineId), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonCamelCaseResult ListLines()
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Lines.Include("Variants").ToList(), JsonRequestBehavior.AllowGet, PreserveReferencesHandling.None);
            }
        }

        public void RemoveLine(int lineId)
        {
            using (BackendContext db = new BackendContext())
            {
                Line line = db.Lines.Include("Variants").Include("Variants.Departures").Include("Variants.VariantStops").Single(l => l.Id == lineId);
                foreach (var variant in line.Variants)
                {
                    db.VariantStops.RemoveRange(variant.VariantStops);
                    db.Departures.RemoveRange(variant.Departures);
                }

                db.Variants.RemoveRange(line.Variants);
                db.Lines.Remove(line);
                db.SaveChanges();
            }
        }

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
                return new JsonCamelCaseResult(db.Stops.Single(s => s.Id == stopId), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonCamelCaseResult ListStops()
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Stops.ToList(), JsonRequestBehavior.AllowGet);
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

        public JsonCamelCaseResult GetDeparturesByVariantId(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                return new JsonCamelCaseResult(db.Departures.Where(d => d.Variant.Id == variantId).ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonCamelCaseResult GetVariant(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Variants.Single(v => v.Id == variantId), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonCamelCaseResult GetVariantWithDepartures(int variantId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Variants.Include("Departures").Single(v => v.Id == variantId), JsonRequestBehavior.AllowGet);
            }
        }
    }
}