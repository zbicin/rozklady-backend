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
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Lines.Single(s => s.Id == lineId), JsonRequestBehavior.AllowGet, PreserveReferencesHandling.None);
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

    }
}