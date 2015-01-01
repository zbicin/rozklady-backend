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
        public void UpdateLineWithVariants(Line line, List<Variant> variants, List<Variant> variantsToRemove)
        {
            using (BackendContext db = new BackendContext())
            {
                if (line.Id > 0)
                {
                    // entity exists
                    db.Entry(line).State = System.Data.Entity.EntityState.Modified;

                    foreach (var singleVariant in variants)
                    {
                        if (singleVariant.Id > 0)
                        {
                            db.Entry(line.Variants.Single(v => v.Id == singleVariant.Id)).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            singleVariant.Line = db.Lines.Single(l => l.Id == line.Id);
                            db.Entry(singleVariant).State = System.Data.Entity.EntityState.Added;
                        }
                    }
                }
                else
                {
                    // this creates the Variants as well
                    db.Entry(line).State = System.Data.Entity.EntityState.Added;
                }
                db.SaveChanges();

                if (variantsToRemove != null) { 
                    foreach (var singleVariant in variantsToRemove)
                    {
                        if (singleVariant.Id > 0)
                        {
                            db.Departures.RemoveRange(db.Departures.Where(d => d.Variant.Id == singleVariant.Id));
                            db.VariantStops.RemoveRange(db.VariantStops.Where(vs => vs.Variant.Id == singleVariant.Id));
                            db.Variants.Remove(db.Variants.Single(v => v.Id == singleVariant.Id));
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        public JsonCamelCaseResult GetLineWithVariants(int lineId)
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Lines.Include("Variants").Single(s => s.Id == lineId));
            }
        }

        public JsonCamelCaseResult ListLines()
        {
            using (BackendContext db = new BackendContext())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return new JsonCamelCaseResult(db.Lines.Include("Variants").Include("Variants.Departures").Include("Variants.VariantStops").ToList());
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