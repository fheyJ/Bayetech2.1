﻿using Bayetech.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayetech.Service.Interface
{
    public interface IGameRequirementsService
    {
        /// <summary>
        /// 获取出售需求列表
        /// </summary>
        /// <returns></returns>
        List<GameRequirements> GameRequirementsList();
    }
}
