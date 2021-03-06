<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Echart.aspx.cs" Inherits="Echart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .tr {
            height: 5%;
        }
        .td1 {
            text-align: center;
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 30%;
            font-size: 14px;
        }
        .td2 {
            text-align: center;
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 69%;
            font-size: 14px;
        }
        .td3 {
            text-align: center;
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 33%;
            font-size: 14px;
        }
        .td4 {
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 33%;
            font-size: 14px;
        }
        .td5 {
            text-align: center;
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 20%;
            font-size: 14px;
        }
        .td6 {
            text-align: left;
            color: #fff;
            background-color: rgba(69,169,224,.5);
            width: 79%;
            font-size: 14px;
        }
        .button1 {
            border-left: 1px solid black;
            border-right: 1px solid black;
            border-bottom: 1px solid black;
        }
        .button2 {
            border-left: 1px solid black;
            border-right: 1px solid black;
            background-color: rgba(220,220,220,1);
        }
    </style>
</head>
<body style="width:100%; height:100%; margin:0;padding:0;position:fixed; background: rgba(33, 46, 74, 1);">
    <div style="width: 100%; height: 10%;color: rgba(208,138,65,1); font-size: 30px; text-align: center; pointer-events: none;">Railway carriage wheelset online intergrated system</div>
    <div style="width:50%;height: 35%; float:left;">
        <div id="bf" style="width:60%;height: 100%;float:left;">
            <div style="height: 10%;width:100%; color:#fff;text-align: center; margin-left: 2%;font-size: 18px;font-family:sans-serif;font-weight: normal;font-style: normal;"><strong id="lwt"></strong></div>
            <div id="echart1" style="width:100%; float:left; margin-left: 2%;"></div>
        </div>
        <div id="echart2" style="width:38%;height: 100%; float:right; background-color: rgba(33, 46, 74, 1);">
            <div id="table" style="height: 5%;color:#fff;"></div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="td1">检测时间</td>
                        <td style="background-color: #fff;width: 69%;">
                            <input type="text" placeholder="请选择时间" id="test1" readonly="readonly" />
                            <input type="hidden" id="time" />
                        </td>
                    </tr>
                    <tr>
                        <td class="td1">车辆号</td>
                        <td id="clh" class="td2"></td>
                    </tr>
                    <tr>
                        <td class="td1">轮径</td>
                        <td class="td2"></td>
                    </tr>
                </table>
            </div>
            <div style="height: 5%;color:#fff;" >车轮缺陷</div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="td1">自动探伤</td>
                        <td class="td2">无</td>
                    </tr>
                    <tr>
                        <td class="td1">实际缺陷</td>
                        <td class="td2">无</td>
                    </tr>
                </table>
            </div>
            <div id="glxx_div" style="height: 5%;color:#fff;" >过滤选项</div>
            <div">
                <table id="glxx" style="width: 100%;display:inline-block;">
                    <tr>
                        <td class="td3">探头类型</td>
                        <td class="td3">报警级别</td>
                        <td class="td3">缺陷报警</td>
                    </tr>
                    <tr>
                        <td class="td4"><input id="10" type="checkbox" onchange="Select_Data(1);" />直探头</td>
                        <td class="td3"><input id="21" type="checkbox" onchange="Select_Data(2);" />一级</td>
                        <td class="td3"><input type="checkbox" />一级</td>
                    </tr>
                    <tr>
                        <td class="td4"><input id="11" type="checkbox" onchange="Select_Data(1);" />斜探头</td>
                        <td class="td3"><input id="22" type="checkbox" onchange="Select_Data(2);" />二级</td>
                        <td class="td3"><input type="checkbox" />二级</td>
                    </tr>
                    <tr>
                        <td class="td4"><input id="12" type="checkbox" onchange="Select_Data(1);" />斜探头-轮缘</td>
                        <td class="td3"><input id="23" type="checkbox" onchange="Select_Data(2);" />三级</td>
                        <td class="td3"><input type="checkbox" />三级</td>
                    </tr>
                    <tr>
                        <td class="td4"><input type="checkbox" />当前探头</td>
                        <td class="td3"></td>
                        <td class="td3"></td>
                    </tr>
                </table>
                <table id="xq" style="width: 100%;display:none;">
                    <tr>
                        <td class="td5">位置</td>
                        <td class="td6 td61"></td>
                    </tr>
                    <tr>
                        <td class="td5">探头</td>
                        <td class="td6 td62"></td>
                    </tr>
                    <tr>
                        <td class="td5">描述</td>
                        <td class="td6 td63"></td>
                    </tr>
                    <tr>
                        <td class="td5">探头明细</td>
                        <td class="td6 td64"></td>
                    </tr>
                </table>
                <div>
                    <button style="color:#000;font-size: 14px;" onclick="Point('inspection');">打印车轮报告</button>&nbsp;&nbsp;&nbsp;<!--<input type="checkbox" style="color:#000;font-size: 14px;" />探头明细-->
                    <span id="dytt">
                        <!--<br />-->
                        <button style="color:#000;font-size: 14px;margin-top: 2%;" onclick="Point('defect');">打印探头报告</button>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <div id="echart3" style="width:50%; float:right;">
        <div id="echart4" style="width: 100%; float:left;"></div>
    </div>
   
        <div id="buv" style="position:fixed;bottom:0;left:0;width: 100%;height:10%;display: inline-block;overflow:auto;white-space: nowrap;"> </div>
   
    <script src="js/jquery.js"></script>
    <script type="text/javascript" src="js/echart/dist/ecStat.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/extension/dataTool.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/extension/bmap.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/echarts.js"></script>
    <script type="text/javascript" src="js/echart/dist/echarts-liquidfill.js"></script>
    <script src="js/laydate/laydate.js"></script>
    <script>
        var ttlx = "";
        var level = "";
        var arrs = [];
        var datas = [];
        var point_data = "";
        for (var i = 1; i <= 500; i++) {
            arrs.push(i);
            datas.push(0);
        }
        var status = 1;
        int();
        function int() {
            var divOne1 = document.getElementById('echart1');
            var divOne2 = document.getElementById('echart2');
            var divOne3 = document.getElementById('echart3');
            divOne1.style.height = divOne1.offsetWidth + 'px';
            divOne2.style.height = divOne1.offsetWidth + 'px';
            divOne3.style.height = divOne1.offsetWidth + 'px';
          

            SetData('data.ashx?type=int', true);
            laydate.render({
                elem: '#test1' //指定元素
                , type: 'datetime'
                , done: function (value, date, endDate) {
                    $("#time").val(value);
                    document.getElementById("glxx").style.display = "inline-block";
                    document.getElementById("xq").style.display = "none";
                    document.getElementById("dytt").style.display = "inline-block";
                    $("#glxx_div").empty();
                    $("#glxx_div").html("过滤选项");
                    SetData('data.ashx?type=qx&time=' + value + '&ttlx=' + ttlx + '&level=' + level, 0);
                }
            });
            Create_Button();
           
        }

        //获取数据
        function SetData(url, int) {
            if (status == 1) {
                status = 0;
            } else {
                status = 1;
            }
            //var time = $("#time").val();
            $.ajax({
                url: url,
                type: "get", //提交方式 
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    point_data = JSON.stringify(data);
                    $("#lwt").empty();
                    $("#table").empty();
                    $("#clh").empty();
                    $(".td61").empty();
                    $(".td62").empty();
                    $(".td63").empty();
                    $(".td64").empty();
                    $("#echart1").remove();
                    var str = '<div id="echart1" style="width:100%; float:left; margin-left: 2%;"></div>';
                    $("#bf").append(str);
                    var divOne1 = document.getElementById('echart1');
                    divOne1.style.height = divOne1.offsetWidth + 'px';
                    if (data.length > 0) {
                        $("#lwt").html(data[0].UNIT_NUMBER + " 轴号:" + data[0].AXLE_INDEX + " - " + data[0].WHEEL_POSITION + "轮位图");
                        $("#table").html(data[0].UNIT_NUMBER);
                        $("#clh").html(data[0].UNIT_NUMBER);
                        $(".td61").html("角度:" + data[0].PROBE_ANGLE + ";深度:" + data[0].DEFECT_DEPTH + "mm");
                        $(".td62").html(data[0].PROBE_TYPE);
                        $(".td63").html("这是一段是关于缺陷的描述，缺陷的定义......");
                        $(".td64").html("按实际进行描述介绍什么的......");
                        var result = JSON.parse(data[0].DATA).Waves;
                        show_bar(0, result, status, '检测数据曲线 ' + data[0].PROBE_TYPE + data[0].AXLE_INDEX + '(' + data[0].WHEEL_POSITION + ')');
                        pie(echarts.init(document.getElementById('echart1')), data[0].PROBE_ANGLE * 1, 9.5);
                        if(int == 1){
                            $("#test1").val(data[0].DETECTION_TIME1);
                            $("#time").val(data[0].DETECTION_TIME1);
                        }
                    }
                    else {
                        for (var i = 0; i < 500; i++) {
                            datas[i] = 0;
                        }
                        bar(echarts.init(document.getElementById('echart3')), datas, '检测数据曲线');
                        pie(echarts.init(document.getElementById('echart1')), 0, 9.5);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert( textStatus + errorThrown);
                }
            })
        }

        function show_bar(val, data, a, t) {
            if (a == status) {
                if (val > data.length - 1) {
                    val = val - data.length + 1;
                }
                datas = data[val].Wave;
                bar(echarts.init(document.getElementById('echart3')), datas, t);
                val++;
                setTimeout(function () { show_bar(val, data, a, t); }, 100);
            }
        }

        function Select_Data(a) {
            if(a == 1)
            {
                ttlx = "";
                for (var i = 0; i <= 2;i++){
                    var checkStatus = $('#1' + i).is(':checked');
                    if (checkStatus) {
                        ttlx = (ttlx == "") ? (ttlx + i) : (ttlx + "," + i)
                    }
                }
            }
            if (a == 2) {
                level = "";
                for (var i = 1; i <= 3; i++) {
                    var checkStatus = $('#2' + i).is(':checked');
                    if (checkStatus) {
                        level = (level == "") ? (level + i) : (level + "," + i)
                    }
                }
            }
            var time = $("#time").val();
            SetData('data.ashx?type=qx&time=' + time + '&ttlx=' + ttlx + '&level=' + level, 0);
        }

        function pie(myChart, a, d) {
           
            var barFgColor = "rgba(243,240,236,1)";
            var val = 80;
            option = {
               
                polar: {
                    radius: ['0%', '100%'],
                },
                angleAxis: {
                    min: 0,
                    max: 360,
                    interval: 90,
                    startAngle: 90 + a,
                    axisTick: {
                        show: false
                    },
                    axisLabel: {
                        show: false
                    },
                    axisLine: {
                        show: false
                    },
                    splitLine: {
                        show: true,
                        lineStyle: {
                            color: '#000',
                            width: 0.5
                        }
                    },
                    z: 15
                },
                radiusAxis: {
                    min: 0,
                    max: 10,
                   
                    axisTick: {
                        show: false
                    },
                    axisLabel: {
                        show: false
                    },
                    triggerEvent: false,
                    axisLine: {
                        show: true,
                        symbol: ["none", "arrow"],
                        triggerEvent: false,
                        symbolSize: [5, 20],
                        lineStyle: {
                            color: '#000',
                            width: 0.5
                        }
                    },
                    splitLine: {
                        show: false
                    },
                    z: 15
                },
                series: [
                    {
                        name: "bg0",
                        type: "scatter",
                        coordinateSystem: 'polar',
                        z: 20,
                        hoverAnimation: false,
                        data: [{
                            symbol: "rect",
                            value: [d, 180],
                        }],
                        tooltip: {
                            /*返回需要的信息*/
                            formatter: '4654126',
                            textStyle: {color: '#000'}
                        }
                    },
                    {
                        name: 'bg1',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['95%', '100%'],
                        borderColor: null,
                        borderWidth: 0,
                        z: 12,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            silent: true,
                            itemStyle: {
                                normal: {
                                    //color:'rgb(255,255,255)'
                                    color: {
                                        type: 'radial',
                                        x: 0.1,
                                        y: 0.5,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgb(255,255,255)' },
                                                //{offset: 0.8, color: 'rgba(208,138,65,1)'},
                                                { offset: 1, color: 'rgba(230,230,230,1)' }
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg2',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['85%', '95%'],
                        itemStyle: {
                            borderColor: null,
                            borderWidth: 0,
                            hadowColor: 'rgba(0, 0, 0, 1)',//设置折线阴影
                            shadowOffsetX: '-15',
                            shadowOffsetY: '5',
                            shadowBlur: 5,
                            z: 5
                        },

                        Z: 11,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                borderColor: null,
                                borderWidth: 0,
                                normal: {
                                    //color:'	rgb(192,192,192)'
                                    color: {
                                        type: 'radial',
                                        x: 0.1,
                                        y: 0.5,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgb(192,192,192)' },
                                                //{offset: 0.8, color: 'rgba(208,138,65,1)'},
                                                { offset: 1, color: 'rgba(170,170,170,.8)' }
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg3',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: false,
                        radius: ['83%', '85%'],
                        itemStyle: {
                            borderColor: null,
                            borderWidth: 0
                        },
                        z: 0,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                borderColor: null,
                                borderWidth: 0,
                                normal: {
                                    //color: 'rgba(208,138,65,1)',
                                    color: {
                                        type: 'radial',
                                        x: 0.1,
                                        y: 0.5,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.5, color: 'rgba(208,138,65,1)' },
                                                { offset: 1, color: 'rgba(0, 0, 0)' }
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg4',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['80%', '83%'],
                        itemStyle: {
                            borderColor: null,
                            borderWidth: 0
                        },
                        z: 0,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                borderColor: null,
                                borderWidth: 0,
                                normal: {
                                    //color:'rgba(208,138,65,1)'
                                    color: {
                                        type: 'radial',
                                        x: 0.2,
                                        y: 0.6,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: barFgColor },
                                                //{ offset: 0, color: 'rgba(243,240,236,0.5)' },
                                                //{ offset: 0, color: 'rgba(255,255,255,1)' },barFgColor
                                                { offset: 0.4, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.7, color: 'rgba(208,138,65,1)' },
                                                { offset: 1, color: 'rgba(208,138,65,1)' }
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg5',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['70%', '80%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 0,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    //color:'rgba(243,176,6,0.97)'
                                    color: {
                                        type: 'radial',
                                        x: 0.2,
                                        y: 0.6,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgba(103,62,19,1)' },
                                                { offset: 0.5, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.7, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.8, color: barFgColor },
                                                { offset: 1, color: 'rgba(208,138,65,1)' },
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg6',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['69%', '70%'],
                        itemStyle: {
                            borderColor: null
                        },
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    //color:'rgba(142,104,9,0.97)'
                                    //color:'rgba(236,171,6,0.9)'
                                    color: {
                                        type: 'radial',
                                        x: 0.2,
                                        y: 0.6,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgba(103,62,19,1)' },
                                                { offset: 0.5, color: 'rgba(208,138,65,1)' },
                                                { offset: 1, color: 'rgba(208,138,65,1)' },
                                                //{ offset: 0.8, color: 'rgba(243,240,236,0.5)' },
                                        ],
                                        global: false // 缺省为 false
                                    }
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg7',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: true,
                        silent: true,
                        radius: ['39%', '69%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 10,
                        data: [{
                            value: 100,
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    color: 'rgba(208,138,65,0.9)'
                                    /*color: {
                                        type: 'radial',
                                        x: 0.2,
                                        y: 0.6,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgba(208,138,65,0.9)' },
                                                { offset: 0.7, color: 'rgba(208,138,65,0.9)' },
                                                { offset: 0.8, color: 'rgba(243,240,236,0.5)' },
                                                { offset: 1, color: 'rgba(208,138,65,0.9)' },
                                        ],
                                        global: false // 缺省为 false
                                    }*/
                                }
                            }
                        }]
                    },
                    {
                        name: 'bg8',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['36%', '39%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 10,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    //color:'rgba(236,171,6,0.97)'
                                    //上一层
                                    color: {
                                        type: 'radial',
                                        x: 0.2,
                                        y: 0.6,
                                        r: 1,
                                        colorStops: [
                                                { offset: 0, color: 'rgba(103,62,19,1)' },
                                                { offset: 0.5, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.7, color: 'rgba(208,138,65,1)' },
                                                { offset: 0.8, color: barFgColor },
                                                { offset: 1, color: 'rgba(208,138,65,1)' },
                                        ],
                                        global: false // 缺省为 false
                                    },
                                    //shadowBlur: 15.5,
                                    //shadowOffsetX: -2.5
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg9',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['34%', '36%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 10,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    color: 'rgba(208,138,65,1)'
                                    /*color: {
                                        type: 'radial',
                                        x: 0.1,
                                        y: 0.5,
                                        r: 1,
                                        colorStops: [
                                                //{offset: 0, color: 'rgba(255,255,255,0.9)'},
                                                { offset: 0, color: 'rgba(50, 50, 50)' },
                                                { offset: 0.8, color: 'rgba(243,176,6,0.97)' },
                                                { offset: 1, color: 'rgba(243,176,6,0.97)' },

                                        ],
                                        global: false // 缺省为 false
                                    }*/
                                }
                            }
                        }],
                    },
                     {
                         name: 'bg10',
                         type: 'pie',
                         hoverAnimation: false,
                         animation: false,
                         silent: true,
                         radius: ['24%', '34%'],
                         itemStyle: {
                             borderColor: null,
                             /*shadowColor:' rgba(0, 0, 0, 0.5)',//设置折线阴影
                             shadowOffsetX: '-20',
                             shadowOffsetY: '5',
                             shadowBlur: 10,*/
                         },
                         z: 10,
                         data: [{
                             value: 100,
                             radius: [0, '30%'],
                             //name: 'v1',
                             itemStyle: {
                                 normal: {
                                     color: 'rgb(255,255,255)'
                                 }
                             }
                         }],
                     },
                    {
                        name: 'bg11',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['2.5%', '24%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 10,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    color: 'rgb(192,192,192)'
                                }
                            }
                        }],
                    },
                    {
                        name: 'bg12',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['0%', '2.5%'],
                        itemStyle: {
                            borderColor: null
                        },
                        z: 10,
                        data: [{
                            value: 100,
                            //radius: [0, '30%'],
                            //name: 'v1',
                            itemStyle: {
                                normal: {
                                    color: 'rgb(255,255,255)'
                                }
                            }
                        }],
                    },
                ]
            };
            myChart.setOption(option);
            myChart.on('click', function (params) {
                if (params.componentSubType == "scatter") {
                    document.getElementById("glxx").style.display= "none";
                    document.getElementById("xq").style.display = "inline-block";
                    document.getElementById("dytt").style.display = "none";
                    $("#glxx_div").empty();
                }
            });
        }

        function bar(myChart, data, title) {
            option = {
                title: {
                    text: title,
                    left: 'center',
                    textStyle: {
                        color: '#fff'
                    },
                },
                tooltip: {
                    trigger: 'axis',
                    axisPointer: {
                        animation: false
                    }
                },
                axisPointer: {
                    link: { xAxisIndex: 'all' }
                },
                grid: [{
                    left: 50,
                    right: 50,
                    height: '80%',
                    top: '10%',
                    show: true
                }],
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        axisLine: { onZero: true },
                        data: arrs,
                        axisLabel: {
                            color: 'rgb(255,255,255)',
                            margin: 10,
                            interval: function (index, value) {
                                arr = [1, 100, 200, 300, 400, 500];
                                if (arr.indexOf(value * 1) > -1) {
                                    return true;
                                }
                            },

                        },
                        axisTick: {
                            lineStyle: { color: 'rgb(255,255,255)' }    // x轴刻度的颜色
                        },
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        max: 300,
                        axisLabel: {
                            color: 'rgb(255,255,255)',
                        },
                        axisTick: {
                            lineStyle: { color: 'rgb(255,255,255)' }    // x轴刻度的颜色
                        },
                    }
                ],
                series: [
                    //{
                    //    //name: '流量',
                    //    type: 'line',
                    //    symbolSize: 8,
                    //    hoverAnimation: false,
                    //    symbol: "none",
                    //    data: [[20,60],[60,60]],
                    //    itemStyle: {
                    //        color: 'red'
                    //    },
                    //    lineStyle: {
                    //        width: 3
                    //    }
                    //},
                    {
                        //name: '流量',
                        type: 'line',
                        symbolSize: 8,
                        hoverAnimation: false,
                        symbol: "none",
                        data: data,
                        itemStyle: {
                            color: 'yellow'
                        },
                        lineStyle: {
                            width: 1
                        }
                    }
                ]
            };
            myChart.setOption(option);
        }

        function Create_Button() {
            var divOne1 = document.getElementById('buv');
            //divOne1.style.height = divOne1.offsetWidth + 'px';
            var h = divOne1.offsetHeight / 2;
            //h = h + h * 0.3;
            var str1 = '';
            var str2 = '';
            for (var i = 1; i <= 160; i++) {
                if(i % 4 == 1)
                {  
                    str1 = str1 + '<button id="but' + i + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">直' + i + '</button>';
                } else if (i % 4 == 2) {
                    str1 = str1 + '<button id="but' + i + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">轮' + i + '</button>';
                } else if (i % 4 == 2) {
                    str1 = str1 + '<button id="but' + i + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">直' + i + '</button>';
                } else {
                    str1 = str1 + '<button id="but' + i + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">斜' + i + '</button>';
                }
                /*var i1 = i + 1;
                var i2 = i + 2;
                var i3 = i + 3;
                var n = i + 160;
                var n1 = i + 161;
                var n2 = i + 162;
                var n3 = i + 163;
                str1 = str1 + '<button id="but' + i + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">直' + i + '</button>'
                            + '<button id="but' + i1 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">轮' + i1 + '</button>'
                            + '<button id="but' + i2 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">直' + i2 + '</button>'
                            + '<button id="but' + i3 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">斜' + i3 + '</button>';
                str2 = str2 + '<button id="buv' + n + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">斜' + n + '</button>'
                            + '<button id="buv' + n1 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">直' + n1 + '</button>'
                            + '<button id="buv' + n2 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">轮' + n2 + '</button>'
                            + '<button id="buv' + n3 + '" class="bun" style="width: ' + h + 'px;height: 100%;font-size:2px;">直' + n3 + '</button>';*/
            }
            for (var n = 161; n <= 320; n++) {
                if (n % 4 == 1) {
                    str2 = str2 + '<button id="but' + n + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">斜' + n + '</button>';
                } else if (n % 4 == 2) {
                    str2 = str2 + '<button id="but' + n + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">直' + n + '</button>';
                } else if (n % 4 == 2) {
                    str2 = str2 + '<button id="but' + n + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">轮' + n + '</button>';
                } else {
                    str2 = str2 + '<button id="but' + n + '" class="bun" style="width: ' + h + 'px;height: 50%;font-size:2px;">直' + n + '</button>';
                }
            }
            //$("#but").empty();
            $("#buv").empty();
            //$("#but").append(str1);
            $("#buv").append(str1 + "<br />" + str2);
        }

        function Point(a) {
            var time = $("#time").val();
            window.open(a + '.aspx?time=' + time + '&ttlx=' + ttlx + '&level=' + level);
        }

        //显示当前时间
        function getCurDate() {
            d = new Date();
            var years = d.getFullYear();
            var month = add_zero(d.getMonth() + 1);
            var days = add_zero(d.getDate());
            var hours = add_zero(d.getHours());
            var minutes = add_zero(d.getMinutes());
            var seconds = add_zero(d.getSeconds());
            var ndate = years + "-" + month + "-" + days + " " + hours + ":" + minutes + ":" + seconds;
            document.getElementById("time").innerHTML = ndate;
        }

        function add_zero(temp) {
            if (temp < 10) return "0" + temp;
            else return temp;
        }
    </script>
</body>
</html>
