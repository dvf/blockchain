#!/usr/bin/env python
# -*- coding=utf-8 -*-
import urllib,urllib2
#from poster.encode import multipart_encode
#from poster.streaminghttp import register_openers

Sender = "sosodata";
Sender = "tshp_data_monitor";

#发送微信曲线
def SendChart(isChart, Rcptto=['jasonyjiang'], Sender='tshp_music'):
    encodeChart = {}
    for k in isChart:
        if type(isChart[k]) == list:
            encodeChart[k] = [urllib.quote(v.decode("gbk").encode("utf-8")) for v in isChart[k]]
        else:
            encodeChart[k] = urllib.quote(isChart[k].decode("gbk").encode("utf-8"))
    jsondata = str({"Sender":Sender, "Rcptto":Rcptto, "isChart":encodeChart}).replace("\'","\"")
    req = urllib2.Request(url='http://10.185.8.11/cgi-bin/sendmsg', data='data=%s'%jsondata)
    ret = eval(urllib2.urlopen(req).read())
    if  ret['errCode'] != '0': print "retcode="+ret['errCode']+",retmsg="+ret['errMsg']

#发送微信图片
def SendTable(isTable, Rcptto=['jasonyjiang'], Sender='tshp_music'):
    encodeTable = {}
    for k in isTable:
        if type(isTable[k]) == list:
            encodeTable[k] = [urllib.quote(v.decode("gbk").encode("utf-8")) for v in isTable[k]]
        else:
            encodeTable[k] = urllib.quote(isTable[k].decode("gbk").encode("utf-8"))
    jsondata = str({"Sender":Sender, "Rcptto":Rcptto, "isTable":encodeTable}).replace("\'","\"")
    #print jsondata;
    req = urllib2.Request(url='http://10.185.8.11/cgi-bin/sendmsg', data='data=%s'%jsondata)
    ret = eval(urllib2.urlopen(req).read())
    if  ret['errCode'] != '0': print "retcode="+ret['errCode']+",retmsg="+ret['errMsg']

#发送文字
def SendText(isText, Rcptto=['jasonyjiang'], Sender='tshp_music'):
    isText = isText.decode("gbk").encode("utf-8")
    jsondata = str({"Sender":Sender, "Rcptto":Rcptto, "isText":urllib.quote(isText)}).replace("\'","\"")
    req = urllib2.Request(url='http://10.185.8.11/cgi-bin/sendmsg', data='data=%s'%jsondata)
    ret =  eval(urllib2.urlopen(req).read())
    if  ret['errCode'] != '0': print "retcode="+ret['errCode']+",retmsg="+ret['errMsg']


#发送图片
#def SendImg(isImg, Rcptto=['jasonyjiang']):
    #register_openers()
    #isImgContent = open(isImg,"r").read()
    #datagen, headers = multipart_encode( {"isImg": isImgContent, "Sender": Sender, "Rcptto": Rcptto, })
    #request = urllib2.Request("http://10.185.8.11/cgi-bin/sendimg", datagen, headers)
    #ret =  eval(urllib2.urlopen(request).read())
    #if  ret['errCode'] != '0':
        #print "retcode="+ret['errCode']+",retmsg="+ret['errMsg']

if __name__ == "__main__":
    isTable = {"header":["时间","系统调用数","系统失败数","系统失败率","端口调用数","端口失败数","端口失败率"],
            "data1":["19:00","1234","3","0.3%","13023","10","0.01%"]
            ,"data2":["19:05","1230","3","0.3%","12423","20","0.01%"]
            ,"data3":["19:10","1134","30","0.3%","11423","10","0.01%"]
            ,"data4":["19:15","1034","33","0.3%","12423","30","0.01%"]
            ,"data5":["19:20","1004","322","0.3%","13323","60","0.01%"]
            ,"data6":["19:25","834","333","0.3%","9123","100","0.01%"]
            }

    isChart = {"title":"微信报警曲线图标题",
            "desc1":"图例描述1",
            "desc2":"图例描述2",
            "desc3":"图例描述3",
            "label":["19:38","19:39","19:40","19:41","19:42","19:43","19:44","19:45","19:46","19:47"],
            "data1":["3165","3123","3144","11003","11209","2309","3144","3165","3146","3193"],
            "data2":["3365","2123","2244","3033","1109","2203","1144","3265","2126","3143"],
            "data3":["3263","3113","3134","2003","1209","2109","1244","3163","3346","3193"]}

    #SendTable(isTable)
    SendText('蒋勇')
    #SendChart(isChart);
    #SendImg('aa.png')

