﻿using Bayetech.Core;
using Bayetech.Core.Entity;
using Bayetech.DAL;
using Bayetech.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bayetech.Admin.Controllers
{
    public class SettingController : ApiController
    {

        BaseService<Settings> setting = new BaseService<Settings>();

        [HttpGet]
        public JObject GetSettings(string parentId)
        {
            using (BayetechEntities bay = new BayetechEntities())
            {
                var ret = new JObject();
                if (!string.IsNullOrEmpty(parentId))
                {
                    int _parent = Convert.ToInt32(parentId);
                   
                    List<Settings> settings = bay.Settings.Where(c => c.ParentId == _parent).ToList();
                    ret.Add("setting", JToken.FromObject(settings));
                }
                return ret;
            }
        }

        [HttpGet]
        public JObject GetSettingsList(string parentId)
        {
            using (var db = new RepositoryBase())
            {
                JObject ret = new JObject();
                PaginationResult<List<Settings>> ResultPage = new PaginationResult<List<Settings>>();
                if (!string.IsNullOrEmpty(parentId))
                {
                    var page = Pagination.GetDefaultPagination("Id");
                    int _parentId = Convert.ToInt32(parentId);
                    ResultPage.datas = db.FindList<Settings>(page, out page, c => c.ParentId== _parentId);
                    ResultPage.pagination = page;
                }
                ret.Add(ResultInfo.Result, true);
                ret.Add(ResultInfo.Content, JToken.FromObject(ResultPage));
                return ret;
            }
        }

        [HttpPost]
        public JObject AddSettings(JObject json)
        {
            try
            {
                using (var db = new RepositoryBase().BeginTrans())
                {
                    Settings set;
                    JObject ret = new JObject();
                    if (json["model"] != null)
                    {
                        set = JsonConvert.DeserializeObject<Settings>(json["model"].ToString());
                        db.Insert(set);
                        db.Commit();
                    }
                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        public JObject DelSettings(string id)
        {
            using (BayetechEntities bay = new BayetechEntities())
            {
                var ret = new JObject();
                if (!string.IsNullOrEmpty(id))
                {
                    int _id = Convert.ToInt32(id);

                    Settings settings = bay.Settings.Where(c => c.Id == _id).FirstOrDefault();
                    settings.IsDelete = true;
                    bay.SaveChanges();
                }
                return ret;
            }
        }
    }
}
