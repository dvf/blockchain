#!/usr/bin/env python
#coding=utf-8
import os;
import logging;
import logging.handlers;
import datetime,time;
import hashlib;
import struct;

def Digest64(str):
    md5value = hashlib.md5(str).digest();
    a,b = struct.unpack('LL', md5value);
    return a;

#整数格式化w
def IntFormat(intData):
    intData = str(intData)
    if len(intData) <= 3:
        return intData

    ret = ""
    strData = str(intData)
    lens = len(strData)
    while lens > 0:
        lens -= 3
        if lens <= 0:
            ret = strData[0:(lens+3)]+ret
        else:
            ret = strData[lens:(lens+3)]+ret
            ret = ","+ret
    if len(ret) < 3:
        return ret
    if ret[0] == '-' and ret[1] == ',':
        ret = ret[0:1]+ret[2:]
    return ret

#整数格式化w
def IntFormatW(intData):
    floatData = float(intData) / 10000
    floatData = round(floatData, 0)
    floatData = str(floatData)
    lenDot = floatData.find(".")
    intData = floatData[0:lenDot]
    strData = intData
    lens = len(strData)
    ret = ""
    while lens > 0:
        lens -= 3
        if lens <= 0:
            ret = strData[0:(lens+3)]+ret
        else:
            ret = strData[lens:(lens+3)]+ret
            ret = ","+ret
    return ret

#整数格式化w
def IntFormatWV2(intData):
    floatData = float(intData) / 10000
    floatData = round(floatData, 2)
    floatData
    floatData = str(floatData)
    lenDot = floatData.find(".")
    intData = floatData[0:lenDot]
    floatPart = floatData[lenDot: len(floatData)]
    if len(intData) <= 3:
        return intData+floatPart
    strData = str(intData)
    lens = len(strData)
    ret = ""
    while lens > 0:
        lens -= 3
        if lens <= 0:
            ret = strData[0:(lens+3)]+ret
        else:
            ret = strData[lens:(lens+3)]+ret
            ret = ","+ret
    return ret+floatPart


#小数返回百分比
def PercentFormat(float_data):
    return str(round(float(float_data)*100.00,2))+'%'

#渲染模板
def Template(html, dicts):
    for key in dicts:
        #list
        if isinstance(dicts[key], list) == True:
            start_pos = html.find('<volist id=' + key + '>')
            end_pos = html.find('</volist>', start_pos)
            if end_pos == -1:
                continue
            end_pos += 9
            org_str = html[start_pos:end_pos]
            rep_str = html[start_pos+len('<volist id='+key+'>'):end_pos-len('</volist>')]
            new_str = ''
            for item in dicts[key]:
                tmp_str = rep_str
                for sub_key in item:
                    value = str(item[sub_key])
                    tmp_str = tmp_str.replace('<%=' + key+'.'+sub_key  + '%>', value)
                new_str += tmp_str
            html = html.replace(org_str, new_str)
        #dicts
        else:
            value = str(dicts[key])
            html = html.replace('<%=' + key  + '%>', value)
    return html

def GetFileList(path, prefix=[], subfix=[]):
    try:
        files = os.listdir(path);
    except Exception as e:
        print e;
        return [];
    if isinstance(prefix, str): prefix = [prefix];
    if isinstance(subfix, str): subfix = [subfix];

    result = [];
    for file in files:
        if len(prefix) != 0:
            isOk = False;
            for item in prefix:
                if file.startswith(item): isOk = True;
            if not isOk: continue;

        if len(subfix) != 0:
            isOk = False;
            for item in subfix:
                if file.endswith(item): isOk = True;
            if not isOk: continue;

        result.append(file);
    return result;

def GetLogger(log_file):
    try:
        log_path = os.path.dirname(log_file);
        if not os.path.exists(log_path):
            os.mkdir("%s" % (log_path));

        logger = logging.getLogger(log_file);
        logger.setLevel(logging.DEBUG)

        fh = logging.handlers.RotatingFileHandler(log_file, maxBytes=100*1024*1024, backupCount=10);
        fh.setLevel(logging.DEBUG);
        myformat = '[%(asctime)s][%(filename)s:%(lineno)d]:%(levelname)s: %(message)s';
        formatter = logging.Formatter(myformat);
        fh.setFormatter(formatter)
        logger.addHandler(fh)
        return logger
    except Exception as e:
        print e;
        return None

if __name__ == '__main__':

    print Digest64("CA后来才发现");
