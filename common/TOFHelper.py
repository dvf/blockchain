#!/usr/bin/env python
# encoding: utf8

##########################################
# Author: jasonyjiang
# Date:   2015-03-10
##########################################

import urllib2
import hashlib
import json
import sys
import binascii
import time
import random
import os;
from Crypto.Cipher import DES
reload(sys)
sys.setdefaultencoding('utf8')

#WECHAT_SENDER = 'tshp_music';
WECHAT_SENDER = 'tshp_data_monitor';
RTX_SENDER = 'jasonyjiang';

#TOF_APPKEY = 'f180211e2a71457580c5cb6a45e88431';
#TOF_SYSID = '22567';

TOF_APPKEY = '412f907fb0d348688c4e38173397a58b';
TOF_SYSID = '20005';
QUERY_TIMEOUT = 5;

TOF_MSG_URL = {
        'rtx' : 'http://oss.api.tof.oa.com/api/v1/message/SendRTX',
        'sms' : 'http://oss.api.tof.oa.com/api/v1/Message/SendSMS',
        'mail' : 'http://oss.api.tof.oa.com/api/v1/Message/SendMail',
        'wechat' : 'http://oss.api.tof.oa.com/api/v1/Message/SendWeiXin',
        'getinfo' : 'http://oss.api.tof.oa.com/api/v1/Staff/Info?engName='
};

def _des_signature(key, text):
    obj=DES.new(key, DES.MODE_CBC, key);
    pad = 8 - (len(text) % 8);
    new_text = text + pad * chr(pad);
    ciph=obj.encrypt(new_text);
    return binascii.hexlify(ciph).upper();

def make_tof_auth_header():
    auth_header = {}
    auth_header['appkey'] = TOF_APPKEY
    auth_header['random'] = str(random.randint(1,1000000))
    auth_header['timestamp'] = str(int(time.time()))
    key = TOF_SYSID.ljust(8,'-');
    data = "random%stimestamp%s" % (auth_header['random'], auth_header['timestamp'])
    auth_header['signature'] = _des_signature(key, data);
    return auth_header

def encode_multipart_formdata(fields):
    BOUNDARY = "%s-%d" % (hashlib.md5(str(time.time())).hexdigest(), random.randint(1,1000000))
    CRLF = '\r\n'
    L = []
    for (key, value) in fields.items():
        if key == 'attachment':
            if not isinstance(value, list): file_list = [value];
            else : file_list = value;

            for file in file_list:
                if not os.path.isfile(file):
                    print 'file not exit';
                    continue;
                fd = open(file, 'rb');
                fstr = fd.read();
                fd.close();
                L.append('--' + BOUNDARY)
                L.append('Content-Disposition: attachment; filename=%s' % value)
                L.append('')
                L.append(fstr);
            continue;

        L.append('--' + BOUNDARY)
        L.append('Content-Type: text/plain; charset=utf-8')
        L.append('Content-Disposition: form-data; name=%s' % key)
        L.append('')
        L.append(value)
    L.append('--' + BOUNDARY + '--')
    L.append('')
    body = CRLF.join(L)
    content_type = 'multipart/form-data; boundary="%s"' % BOUNDARY
    return content_type, body

#TOF API
class TOFHelper:
    def GetStaffInfo(self, name):
        api_url = TOF_MSG_URL.get('getinfo') + name;
        auth_header = make_tof_auth_header()
        request = urllib2.Request(api_url, None, auth_header)
        try:
            req = urllib2.urlopen(request, None, QUERY_TIMEOUT)
        except urllib2.URLError as e:
            print "error querying server: %s" % e
            return e.read()
        return req.read()

    def SendMsg(self, msg_type, sender, receiver, msg, title = ''):
        if msg_type == "wechat" and len(sender) == 0: sender = WECHAT_SENDER;
        elif len(sender) == 0: sender = RTX_SENDER;
        if not isinstance(receiver, list): receiver = [receiver];

        request_param = {};
        if msg_type == "rtx": request_param["Title"] = title

        request_param['MsgInfo'] = msg;
        request_param['Priority'] = '1';
        request_param['Receiver'] =  ';'.join(receiver);
        request_param['Sender'] = sender;
        msg_url = TOF_MSG_URL.get(msg_type)

        (content_type, request_data) = encode_multipart_formdata(request_param)
        auth_header = make_tof_auth_header()
        auth_header['Content-Type'] = content_type
        request = urllib2.Request(msg_url, request_data, auth_header)
        try:
            req = urllib2.urlopen(request, None, QUERY_TIMEOUT)
        except urllib2.URLError as e:
            print "error querying server: %s" % e
            return None
        return req.read()

    def SendMail(self, sender, receiver, title, body, param = {}):
        if not isinstance(receiver, list): receiver = [receiver];
        if len(sender) == 0: sender = RTX_SENDER + '@tencent.com';

        request_param = {};

        request_param['From'] =  sender;
        request_param['To'] =  ';'.join(receiver);
        request_param['Content'] = body;
        request_param['Title'] = title;

        request_param['EmailType'] = param.get('EmailType', '1'); #1 内部邮件 0：外部邮件
        request_param['CC'] = param.get('CC','');
        request_param['Bcc'] = param.get('Bcc','');
        request_param['Priority'] = param.get('Priority', '1');
        request_param['BodyFormat'] = param.get('BodyFormat','1'); #邮件格式，0 文本、1 Html
        request_param['Priority'] = param.get('Priority', '1');
        if param.has_key('attachment') : request_param['attachment'] = param['attachment'];

        msg_url = TOF_MSG_URL.get('mail');

        (content_type, request_data) = encode_multipart_formdata(request_param)
        auth_header = make_tof_auth_header()
        auth_header['Content-Type'] = content_type
        request = urllib2.Request(msg_url, request_data, auth_header)
        try:
            req = urllib2.urlopen(request, None, QUERY_TIMEOUT)
        except urllib2.URLError as e:
            print "error querying server: %s" % e
            return None
        return req.read()

    #发送微信
    def SendWeixin(self, sender, receiver, msg):
        return self.SendMsg('wechat', sender, receiver, msg);

    #发送RTX
    def SendRTX(self, sender, receiver, msg ,title):
        return self.SendMsg('rtx', sender, receiver, msg, title);

    #发送短信
    def SendSMS(self, sender, receiver, msg):
        return self.SendMsg('sms', sender, receiver, msg);

def getStaffInfo(name):
    tof = TOFHelper();
    return tof.GetStaffInfo(name);

def sendWeixin(sender, receiver, text):
    tof = TOFHelper();
    return tof.SendWeixin(sender, receiver, text);

def sendSMS(sender, receiver, text):
    tof = TOFHelper();
    return tof.SendSMS(sender, receiver, text);

def sendRTX(sender, receiver, text, title):
    tof = TOFHelper();
    return tof.SendRTX(sender, receiver, text, title);

def sendMail(sender, receiver, title, content, param = {}):
    tof = TOFHelper();
    return tof.SendMail(sender, receiver, title, content, param);

if __name__ == "__main__":

    msg = sys.argv[1];
    print getStaffInfo(msg);
    #print sendWeixin('',['jasonyjiang'], msg);
    #print sendSMS('', ['jasonyjiang'], msg);
    print sendRTX('', ['jasonyjiang'], msg, 'title');
    #param = {};
    #param['attachment'] = ['src_zero_rate_month.png', 'TencentMail.py'];
    #param['attachment'] = ['aa.txt','bb.txt'];
    #param['EmailType'] = '0';

    #print sendMail('', ['120610609@qq.com'], 'title', '<h2>jiangyong</h2><img src="cid:src_zero_rate_month.png">', param);
    #print sendMail('', ['jasonyjiang'], 'title', '<h2>jiangyong</h2><img src="cid:src_zero_rate_month.png">', param);
