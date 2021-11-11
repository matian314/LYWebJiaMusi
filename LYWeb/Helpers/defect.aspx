<%@ Page Language="C#" AutoEventWireup="true" CodeFile="defect.aspx.cs" Inherits="defect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>单探头探伤报告</title>
    <style>
        .table {
            width: 100%;
            border-collapse:collapse;
        }
        .table {
            width: 100%;
            border-collapse:collapse;
        }
        .td1 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 15%;
            font-size: 7px;
            border:1px solid #000;
        }
        .td2 {
            text-align: left;
            color: #000;
            width: 35%;
            font-size: 7px;
            border:1px solid #000;
        }
        .td3 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 15%;
            font-size: 7px;
            border:1px solid #000;
        }
        .td4 {
            text-align: left;
            color: #000;
            width: 85%;
            font-size: 7px;
            border:1px solid #000;
        }
        .td5 {
            text-align: center;
            color: #000;
            width: 100%;
            font-size: 7px;
            border:1px solid #000;
        }
        .td6 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 5%;
            font-size: 7px;
            border:solid #000;
            border-width: 0 1px 0 1px;
        }
         .td7 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 10%;
            font-size: 7px;
            border:solid #000;
            border-width: 0 1px 0 1px;
        }
          .td8 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 40%;
            font-size: 7px;
            border:solid #000;
            border-width: 0 1px 0 1px;
        }
           .td9 {
            text-align: left;
            color: #000;
            background-color: rgba(180,180,180,1);
            width: 30%;
            font-size: 7px;
            border:solid #000;
            border-width: 0 1px 0 1px;
        }
           .td6_1 {
            text-align: left;
            color: #000;
            width: 5%;
            font-size: 7px;
            border:solid #000;
            border-width: 1px 1px 0 1px;
        }
         .td7_1 {
            text-align: left;
            color: #000;
            width: 10%;
            font-size: 7px;
            border:solid #000;
            border-width: 1px 1px 0 1px;
        }
          .td8_1 {
            text-align: left;
            color: #000;
            width: 40%;
            font-size: 7px;
            border:solid #000;
            border-width: 1px 1px 0 1px;
        }
           .td9_1 {
            text-align: left;
            color: #000;
            width: 30%;
            font-size: 7px;
            border:solid #000;
            border-width: 1px 1px 0 1px;
        }
           .td10 {
            text-align: center;
            color: #000;
            width: 50%;
            font-size: 7px;
            border:1px solid #000;
        }
           .td11 {
            width: 50%;
            height: 60%;
            border:1px solid #000;
        }
            .td12 {
            text-align: center;
            color: #000;
            width: 20%;
            height: 25%;
            font-size: 7px;
            border:1px solid #000;
        }
             .td13 {
            text-align: center;
            color: #000;
            width: 30%;
            height: 25%;
            font-size: 7px;
            border:1px solid #000;
        }
             .td14 {
            text-align: center;
            color: #000;
            width: 20%;
            height: 35%;
            font-size: 7px;
            border:1px solid #000;
        }
             .td15 {
            text-align: center;
            color: #000;
            width: 30%;
            height: 35%;
            font-size: 7px;
            border:1px solid #000;
        }
             .td16 {
            width: 100%;
            border:1px solid #000;
        }
             .div {
            /*text-align: left;*/
            color: #000;
            width: 100%;
            height: 70%;
            /*font-size: 7px;*/
            /*border:1px solid #000;*/
        }
    </style>
</head>
<body style="width:100%; height:100%; margin:0;padding:0;position:fixed;">
    <div style="width: 50%; height: 15%;color: black; font-size: 15px; text-align: center; pointer-events: none;"><strong>哈尔滨动车段哈尔滨西动车班组运用所&nbsp;&nbsp;DLJ-I动车组车轴故障在<br />检测系统<br /><br />车轮探伤报告</strong></div>
    <div style="width: 50%; height: 15%;">
        <div style="width: 100%; height: 30%;color:#000;font-size: 15px;">1、车轮信息</div>
        <div style="width: 100%; height: 60%;">
            <table class="table">
                <tr style="width: 100%; font-size: 7px;">
                    <td class="td1">检测时间：</td>
                    <td id="td1" class="td2"></td>
                    <td class="td1">车组号：</td>
                    <td id="td2" class="td2"></td>
                </tr>
                <tr style="width: 100%; font-size: 7px;">
                    <td class="td1">车号：</td>
                    <td id="td3" class="td2"></td>
                    <td class="td1">车辆号：</td>
                    <td id="td4" class="td2"></td>
                </tr>
                <tr style="width: 100%; font-size: 7px;">
                    <td class="td1">轴号：</td>
                    <td id="td5" class="td2"></td>
                    <td class="td1">轮位：</td>
                    <td id="td6" class="td2"></td>
                </tr>
            </table>
        </div>
    </div>
    <div style="width: 50%; height: 70%;">
        <div style="width: 100%; height: 10%;color:#000;font-size: 15px;">2、探头探伤检测信息</div>
        <div style="width: 100%; height: 90%;">
            <table class="table" style="width: 100%; height: 90%;">
                <tr style="width: 100%; font-size: 7px;">
                    <td class="td1">探头类型：</td>
                    <td id="td7" class="td2"></td>
                    <td class="td1">探头编号：</td>
                    <td id="td8" class="td2"></td>
                </tr>
                <tr style="width: 100%; font-size: 7px;">
                    <td class="td5" colspan="2">缺陷波形图</td>
                    <td class="td5" colspan="2">缺陷部位示意图</td>
                </tr>
                <tr style="width: 100%; height: 50%;">
                    <td class="td5" colspan="2"><div id="echart2" style="width: 100%;height:100%; float:left;"></div></td>
                    <td class="td5" colspan="2"><div id="echart1" style="height:100%; float:left; "></div></td>
                </tr>
                <tr style="width: 100%; height: 40%;">
                    <td class="td16" colspan="2">
                        <div class="div">
                            <strong style="font-size: 6px;color:black;margin-top:2%;margin-left:2%;">探伤结果：</strong>
                            <div style="margin-left:2%;"><div id="td9" style="font-size: 6px;color:black;margin-top:2%;margin-left:24px;">探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果探伤结果</div></div>
                        </div>
                        <div style="width: 50%;height:20px;float:right;margin-bottom:2%;margin-right:1%;font-size:6px;">探伤工程师<input type="text" style="width:50%;border:none;border-bottom:1px solid #000;" disabled="disabled" /></div>
                    </td>
                    <td class="td16" colspan="2">                        
                        <div class="div">
                            <strong style="font-size: 6px;color:black;margin-top:2%;margin-left:2%;">处理意见：</strong>
                            <div style="margin-left:2%;"><div id="td10" style="font-size: 6px;color:black;margin-top:2%;margin-left:24px;">处理意见处理意见处理意见处理意见处理意见处理意见处理意见处理意见处理意见处理意见处理意见处理意见</div></div>
                        </div>
                        <div style="width: 60%;height:20px;float:right;margin-bottom:2%;margin-right:1%;font-size:6px;">专业工程师盖章<input type="text" style="width:50%;border:none;border-bottom:1px solid #000;" disabled="disabled" /></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <input type="hidden" id="data" runat="server" />
    <!--<div style="width: 50%; height: 10%;margin-top:1%;">
        <div style="width: 60%;height:100%;float:left;margin-left:1%;">探伤工程师（盖章）<input type="text" style="width:50%;border:none;border-bottom:1px solid #000;"/></div>
        <div style="width: 30%;height:100%;float:right;margin-right:1%;">审核<input type="text" style="width:60%;border:none;border-bottom:1px solid #000;"/></div>
    </div>-->
    <script src="js/jquery.js"></script>
    <script type="text/javascript" src="js/echart/dist/ecStat.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/extension/dataTool.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/extension/bmap.min.js"></script>
    <script type="text/javascript" src="js/echart/dist/echarts.js"></script>
    <script type="text/javascript" src="js/echart/dist/echarts-liquidfill.js"></script>
    <script src="js/laydate/laydate.js"></script>
    <script>
        var datas = [];
        var arrs = [];
        for (var i = 1; i <= 500; i++) {
            arrs.push(i);
            datas.push(0);
        }
        int();
        function int() {
            var divOne1 = document.getElementById('echart1');
            divOne1.style.width = divOne1.offsetHeight + 'px';
            var data = JSON.parse($("#data").val());
            if (data.length == 0) {
                pie(echarts.init(document.getElementById('echart1')), 0, 9.5);
                bar(echarts.init(document.getElementById('echart2')), datas)
            } else {
                $("#td1").html(data[0].DETECTION_TIME);//detection_time
                $("#td2").html(data[0].UNIT_NUMBER);
                $("#td3").html(data[0].NAME);
                $("#td4").html("无车辆号");
                $("#td5").html(data[0].AXLE_INDEX);
                $("#td6").html(data[0].WHEEL_POSITION);
                $("#td7").html(data[0].PROBE_ANGLE);
                //$("#td9").html("");
                //$("#td10").html("");
                var result = JSON.parse(data[0].DATA).Waves;
                bar(echarts.init(document.getElementById('echart2')), result[result.length - 1].Wave)
                pie(echarts.init(document.getElementById('echart1')), data[0].PROBE_ANGLE * 1, 9.5);
            }
        }

        function pie(myChart, a, d) {
            option = {
                //backgroundColor: '#e2eff3',
                backgroundColor: '#fff',
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
                        show: false,
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
                    //interval: 2,
                    axisTick: {
                        show: false
                    },
                    axisLabel: {
                        show: false
                    },
                    triggerEvent: false,
                    axisLine: {
                        show: false,
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
                            textStyle: { color: '#000' }
                        }
                    },
                    {
                        name: 'bg',
                        type: 'pie',
                        hoverAnimation: false,
                        animation: false,
                        silent: true,
                        radius: ['100%', '100%'],
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
                                    borderWidth: 1,//设置边框粗细
                                    borderColor: 'rgb(0,0,0)',//边框颜色
                                    //color:'rgb(255,255,255)'
                                }
                            }
                        }],
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
                            shadowOffsetX: '-10',
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
                                                { offset: 0, color: 'rgba(243,240,236,0.5)' },
                                                //{ offset: 0, color: 'rgba(255,255,255,1)' },
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
                                                { offset: 0.8, color: 'rgba(243,240,236,0.5)' },
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
                        z: 0,
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
                        z: 0,
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
                        z: 0,
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
                                                { offset: 0.8, color: 'rgba(243,240,236,0.5)' },
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
                        z: 0,
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
                         z: 0,
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
                        z: 0,
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
                        z: 0,
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
        }

        function bar(myChart, data) {
            option = {
                /*title: {
                    text: '检测数据曲线 直探头112(内侧)',
                    //subtext: '数据来自西安兰特水电测控技术有限公司',
                    left: 'center'
                },*/
                //grid: {  
                //    top: '-1%'
                //},
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
                            margin: 10,
                            interval: function (index, value) {
                                arr = [1, 100, 200, 300, 400, 500];
                                if (arr.indexOf(value * 1) > -1) {
                                    return true;
                                }
                            },

                        },
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        max: 300
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
                            color: 'green'
                        },
                        lineStyle: {
                            width: 1
                        }
                    }
                ]
            };
            myChart.setOption(option);
        }

    </script>
</body>
</html>