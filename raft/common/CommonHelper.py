#!/usr/bin/env python
#coding=utf-8
import os;
import logging;
import logging.handlers;
import datetime,time;
import hashlib;
import struct;
import json
#import pexpect;
import re;

def Digest64(String):
    MD5Value = hashlib.md5(String).digest();
    A,B = struct.unpack('LL', MD5Value);
    return A;

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

def GetFileSuffix(path):
    Fields = path.split('.');
    if len(Fields) >= 2:
        return Fields[-1];
    return '';

def GetFilePrefix(path):
    Fields = os.path.splitext(path);
    return Fields[0];

def GetFileList(path, prefix=[], subfix=[]):
    try:
        files = os.listdir(path);
    except Exception as e:
        print (e);
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
            os.system("mkdir -p %s" % (log_path));

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
        print (e);
        return None


#执行命令
def ExecuteCommand(cmd, cwd=None, env=None, timeout=None):
    exitcode = -1
    output = "Execute failed"
    try:
        (output, exitcode) = pexpect.run(cmd, withexitstatus=1, timeout=timeout, cwd=cwd, env=env)
    except Exception as err:
        return -1, str(err)
    return exitcode, output

#获取Ip
def GetHostIp():
    Command = "netstat -tlnp|grep '36000'|awk '{print $4}'|awk -F: '{print $1}' 2>/dev/null";
    #(RetCode, RetResult) = ExecuteCommand(Command);
    #print RetCode,RetResult;
    RetResult = os.popen(Command).read().split()[0]
    return RetResult;

def DelSign(text):
    temp = text.decode("utf8");
    #ans = re.sub("[\s+\.\!\/_,$%^*(+\"\']+|[+——！，。？、~@#￥%……&*（）]+".decode("utf8"), "".decode("utf8"),temp).encode("utf-8");
    #ans = re.sub("[+\.\!\/_,$%^*(+\"\']+|[+——！，。？、~@#￥%……&*（）]+".decode("utf8"), "".decode("utf8"),temp).encode("utf-8");
    ans = re.sub("[+\.\!\/_,$%^*(+\"\']+|[+——！，。？：“”《》【】｛｝%?、~@#￥%……&*（）]+".decode("utf8"), "".decode("utf8"),temp).encode("utf-8");
    return ans;

def GetDatetimeFromString(str_time):
    if '.' == str_time[len(str_time)-1]:
        str_time += '0';
    t = datetime.datetime.strptime(str_time, '%Y-%m-%d %H:%M:%S.%f')
    return t
#File Operator Part
def ReadJsonFile(path):
	with open(path,'r') as load_f:
		load_dict = json.load(load_f)
	return load_dict
def WriteJsonFile(path, data):
	with open(path, 'w') as json_file:
		json_file.write(json.dumps(data))

if __name__ == '__main__':
    print(GetHostIp());
    print(Digest64("CA后来才发现"));
    print(ExecuteCommand("ls /data/"))
    print(DelSign("hi, nice to meet you!"));
