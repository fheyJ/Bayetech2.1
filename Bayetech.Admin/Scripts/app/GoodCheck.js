﻿import Vue from '../vue.js'
import comCompnent from '../common.js'
import componentTable from '../components/table.js'

let vmData = {
    //BaseUrl: GetBaseUrl()+"Good/GoodInfo.html?GoodNo=",
    BaseTarget: "_blank",
    tools:{
        _comCompnent:comCompnent,
        _componentTable:componentTable
    },
    GoodListUrl:"/api/CheckGood/GetList",
    keyword: "",
    GoodTitleArray:["商品编号","游戏名称","交易类型","关键词","商品标题","审核商品"],
    GoodInfoArray:[],
    ListObj: [
        {
            GoodNo: "",
            GoodFirstPicture: "",
            aurl: "",
            GoodTitle: "",
            GroupName: "",
            ServerName: "",
            GoodPrice: ""
        }
    ],
    SearchParam: {
        Param: {
            GoodNo:""
        },
        Pagination: {//分页对象
            rows: 10,//每页行数，
            page: 1,//当前页码
            order: "GoodNo",//排序字段
            sord: "asc",//排序类型
            records: 10,//总记录数
            total: 10//总页数。
        }
    },
};

new Vue({
    el: '#CommForm',
    data: vmData,
    created(){
        this.findList();
    },
    methods: {
            findList() {//获取商品的简要列表
            var self=this;
            self.tools._comCompnent.comCompnent.postWebJson(self.GoodListUrl, self.SearchParam, function (data) {
                if (data.result) {
                    self.GoodInfoArray=data.content.datas;
                }
            });
        },
        StartCheck() {//开始检查
            $("#checkModal").modal("show");
        }
    },
    components:{
        comtable:componentTable
    }
});


