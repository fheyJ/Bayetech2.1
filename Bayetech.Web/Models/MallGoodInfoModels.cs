﻿using Bayetech.Core.Entity;
using System.Collections.Generic;

/// <summary>
/// 商品信息基础vo类，所有的订单信息等信息继承此类
/// </summary>
namespace Bayetech.Web.Models
{
    public class MallGoodInfoModels : MallGoodInfo
    {
        public List<vw_MallGoodInfo> mallGoodInfo;
    }
}