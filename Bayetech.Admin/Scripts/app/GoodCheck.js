﻿import Vue from '../vue.js'
import goodprocess from '../components/table-GoodProcess.vue'
import orderprocess from '../components/table-OrderProcess.vue'
import approve from '../components/Approve.vue'


//import comCompnent from '../common.js'(已在配置文件全局引用)
//Vue.prototype.com = comCompnent;单页面引用公共js的另外一种方式，先import后赋值到Vue全局对象上。

let pagetype = comCompnent.default.GetUrlParam($(".NFine_iframe").context.URL,"type");
    
let vmData = {
    //BaseUrl: GetBaseUrl()+"Good/GoodInfo.html?GoodNo=",
    PageType:pagetype,//待处理，已处理，24小时未处理等等单据类型。
    ItemType: "good",//单据类型
    SelectType: "good",//选择类型直接放到参数里面无法监听。
    flowId: "1",
    wfmid: "",
    currentcomponent:"",//当前组件
    GoodListUrl: comCompnent.default.MenuUrl[pagetype],
    CheckGoodUrl:"/api/CheckGood/CheckGoodInfo",
    CheckGoodNo:"",//模态框打开的GoodNo
    keyword: "",
    GoodInfoArray: [],
    Flows: [],//流程名称
    Status:[],//环节名称
    Logmonitors:[],//业务监控
    components: {
        goodprocess: 'goodprocess',
        orderprocess:'orderprocess',
        approve:'approve'
    },
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
        Param: {//查询条件的参数
            WFM_ID:"",
            GoodNo:"",
            OrderNo: "",
            Account:"",
            Status:pagetype,
            SelectType:"good",//form里选择的商品类型
            SelectNo: "",//form里面选择的编号
            CURSTATUS_ID:"-1",//当前环节ID.
            CURSTATUS_NAME:"",//当前环节名称
            Flow_ID:"-1",
            Flow_Name:""
        },
        Pagination: {//分页对象
            rows: 10,//每页行数，
            page: 1,//当前页码
            order: "GoodNo",//排序字段
            sord: "asc",//排序类型
            records: 10,//总记录数
            total: 10//总页数。
        }
    }
};

new Vue({
    el: '#CommForm',
    data: vmData,
    created() {
        this.currentcomponent = goodprocess;
        this.GetFlows();//获取流程绑定列表
        this.GetStatus(1);//默认金币流程环节
        this.findList();
    },
    watch: {
        SelectType(val, oldval) {//类型切换
            var self = this;
            self.SearchParam.Param.SelectType = val;
            if (val == "good") {
                self.currentcomponent = self.components.goodprocess;
            } else if (val == "order") {
                self.currentcomponent = self.components.orderprocess;
                self.GoodInfoArray = [];
            }
        },
        SelectFlow(val, oldval) {//根据选中的流程获取环节
            var self = this;
            self.GetStatus(val);
        },
        deep: true,
        immediate: true
    },
    methods: {
        CreatNewOrder(){//模拟创建一笔订单测试时候使用，正式注释掉。
            var self = this;
            var param = {//新建流程实例
                    EngineInfo: {
                        Flow_Id : "1",
                        Sender_Id : localStorage.getItem("User_Id"),
                        Sender_Code: localStorage.getItem("User_Code")
                    },
                    PageInfo:  {
                        txtPageConditionRule99:";KefuOperate;"
                    }
                }
            var dataEngine = self.$refs.approve.Create_NewFlowExample(param,1); //获得返回参数

            //新建订单并且关联
            //var url = comCompnent.default.WebUrl+"Order/CreatOrder"
            var url = comCompnent.default.LocalWeb+"Order/CreatOrder";
            var orderparam = {
                GoodNo: "201801111655565777",//默认的商品编号
                OrderPrice:"",
                GoodPrice:0,
                GoodTypeId: 3,
                GoodTypeName: "账号",
                BuyNum: "1",
                InternalTypeId:"1",//内部交易类型：（拍卖交易，邮寄交易等等）
                GameName: "",
                GameAccount: "",
                GameAccountAgain: "",
                GroupName: "",
                InternalTypeId:"",
                ServerName: "",
                BuyerPhone: "18717708731",
                BuyerQQ: "619963501",
                Signal:"",
                PromoNum: "1111",
                WFM_ID:dataEngine.result
            };
            comCompnent.default.postWebJson(url,JSON.stringify(orderparam),function (data) {
                if (data) {
                    alert("模拟新建一笔订单成功,流程ID为:"+ dataEngine.result+"请在查询框重新查询!");
                }
            });
        },
        GetFlows() {//获取所有的流程信息
            var self = this;
            comCompnent.default.getWebJson("/api/Flow/GetFlows", null, function (data) {
                if (data) {
                    self.Flows = data.content;
                }
            });
        },
        GetStatus(flowId) {//根据流程获取环节信息
            var self = this;
            var param = {
                flowId: flowId
            };
            comCompnent.default.getWebJson("/api/Flow/GetStatus", param, function (data) {
                if (data) {
                    self.Status = data.content;
                }
            });
        },
        findList() {//获取商品的简要列表
            $("#QueryList").Btns("loading");
            var self = this;
            //改直接绑定
            //self.SearchParam.Param.SelectType =="good"? (self.SearchParam.Param.GoodNo = self.SearchParam.Param.SelectNo):
            //(self.SearchParam.Param.OrderNo = self.SearchParam.Param.SelectNo,self.SearchParam.Param.GoodNo ="");//如果是订单把商品编号置空。
            comCompnent.default.postWebJson(self.GoodListUrl, self.SearchParam, function (data) {
                $("#QueryList").Btns("reset");
                if (data.result) {
                    self.ItemType = self.SearchParam.Param.SelectType;//根据单据类型选择加载的标题等等内容
                    //good和order返回的数据类型不相同
                    self.GoodInfoArray = self.ItemType == "good" ? data.content.datas : data.content;
                    self.SearchParam.Pagination = self.ItemType == "good" ? data.content.pagination : data.outpage;
                    comCompnent.default.SetPagination($('#paginator-test'), self.SearchParam, self.findList);
                }else {
                    self.GoodInfoArray = [];
                }
            },function(){
                $("#QueryList").Btns("reset");
            }); 
        },
        StartCheck(item) {//开始审批 
            var self = this;
            self.CheckGoodNo = item.GoodNo;
            self.$refs.approve.Init(item.WFM_ID);//调用审批组件直接调用改为监听
            $("#checkModal").modal("show");
            //self.wfmid = item.WFM_ID; 监听方式，暂时有漏洞，关闭
        },
        TurnToPage(page){
            var self = this;
            self.SearchParam.Pagination.rows = page;
            self.findList();
        },
        CheckGoods(flag){
            var self = this;
            if (self.SearchParam.Param.SelectType == "good") {//商品审核
                self.SearchParam.Param.GoodNo = self.CheckGoodNo;
                self.SearchParam.Param.Status = (flag == 'Y' ? 'PutOnsale' : 'PutDownsale');
                if (confirm(flag == 'Y' ? "确定审批通过？" : "确认审批不通过？")) {
                    $("#CheckConfirm").Btns("loading");
                    comCompnent.default.postWebJson(self.CheckGoodUrl, self.SearchParam, function (data) {
                        if (data.result) {
                            alert("审批成功!");
                        }
                        $("#checkModal").modal("hide");
                        $("#CheckConfirm").Btns("reset");
                    }, function () {
                        $("#CheckConfirm").Btns("reset");
                    });
                }
            } else {//订单审核
                self.$refs.approve.Execute_OnNextStep();//提交送下一步
            }
        }
    },
    components:{
        goodprocess,
        orderprocess,
        approve
    }
});

//剩余功能缺乏鉴定前后端加载的是订单还是商品的逻辑。后台。

