﻿define(['v-search'], function (vsearch) {
    var html=`<div id="home-nav-box" class="com-header">
            <div class="head-search clearfix">
                <!--logo-->
                <div class="logo">
                    <a href="#">
                        <img src="/Content/Images/logo1.png" alt="Alternate Text" />
                    </a>
                </div>
                <!--search-->
                <v-search></v-search>
            </div>
        </div>`;
    
    var data = {
        object: [],
    };

    var components = {
        name: "v-header",
        template: html,
        components:{
            "v-search":vsearch,
        },
        data() {
            return data;
        },
    };
    return components;
});
