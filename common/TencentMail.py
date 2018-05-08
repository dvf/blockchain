#!/usr/bin/env python
#coding:UTF-8
#=================================================================
# @Author : xiaodeng, Tencent, ECC, Test, AutoTest
# @Desc : 腾讯内部消息发送用的一个接口包装，包括RTX,短信,邮件
# @FileName : TencentMail.py
# @version: 1.2
# @Date : 2013-06-23
# @Remark :
#   使用方法：
#        1. 将此文件拷贝到自己程序相同的目录下
#        2. 在自己的程序中头部增加如下语句：import TencentMail;
#        3. 联系zlightzhang为你程序运行所在的的机器开一下发邮件的权限(这个公司有限制,必须开权限才能用)
#        3. 需要发送邮件或RTX或短信时,按照如下语句调用
#               RTX: TencentMail.sendRTX("收RTX的人的英文名1;收RTX的人的英文名2", "发送RTX的人的英文名", "这里填RTX的标题"， "这里填RTX具体内容,中文必须是UTF8格式");
#               SMS: TencentMail.sendSMS("收信息的人的手机号码1或RTX英文名1; 收信息的人的手机号码2或RTX英文名2", "发信息的人的RTX英文名", "要发送的短信的内容,中文必须是UTF8格式");
#               邮件: TencentMail.sendMailFile("收件人的RTX英文名1;收件人的RTX英文名2", "抄送人的RTX英文名1;抄送人的RTX英文名2", "发邮件人的RTX英文名",
#                                              "邮件主题", "存放邮件内容的文件的全路径");
#               如果要发送附件: TencentMail.sendMailFile("收件人的RTX英文名1;收件人的RTX英文名2", "抄送人的RTX英文名1;抄送人的RTX英文名2", "发邮件人的RTX英文名",
#                                              "邮件主题", "存放邮件内容的文件的全路径", "附件文件全路径有多个附件则文件路径以分号分隔");
# @history
#         2013/06/26  V1.1 修复收件人串的最末尾有分号导致邮件发不出去的问题
#         2013/06/28  V1.2 修复TOF的封装中当附件过大时会出现socket Error 10054的问题，这里具体原因没太弄明白，但应该是服务器将连接重置导致，设置一个超时时间后问题解决
#==================================================================
import os;
import base64;
import socket;
import time;
import httplib;
import logging;

UTF8 = "utf-8";
LOGGER = logging.getLogger("Tencent.OA.Framework.Messages");
APPKEY = "f180211e2a71457580c5cb6a45e88431";
TOF_HOST_NAME = "ws.tof.oa.com";
TOF_HOST_IP = "10.14.12.7";

def sendMailContent(oReceiver, oCc, sSender, sSubject, sContent, oAttachList = [], sDomain="tencent.com"):
    ''' 直接发送内容
    oReceiver：收件人名字列表:["rachelzu","xiaodeng"], 也可以是字符串, 如果是字符串需要以分号分隔
    oCc:抄送人名字列表
    sSender：发件人名字：
    sSubject:邮件标题
    sContent：邮件内容
    oAttachList：附件列表
    sDomain:邮件域名
    '''
    oReceiverAddr = _getAddrListWithDomain(oReceiver, sDomain);
    oCcAddr = _getAddrListWithDomain(oCc, sDomain);
    oSenderAddr = _getAddrListWithDomain(sSender, sDomain);
    sSenderAddr = "".join(oSenderAddr);
    oTmpAttachList = _getAttachList(oAttachList);
    # tof 邮件服务
    sendByTof(sSenderAddr, ";".join(oReceiverAddr), ";".join(oCcAddr), sSubject, sContent, oTmpAttachList);

def sendMailFile(oReceiver, oCc, sSender, sSubject, sPath, oAttachList = [], sDomain="tencent.com"):
    """ 直接发送文件 """
    if not os.path.exists(sPath): raise Exception("the path '" + str(sPath) + "' is not exist!");
    if not os.path.isfile(sPath): raise Exception("the path '" + str(sPath) + "' is not a regular file!");
    sMode = "r";
    oFile = open(sPath, sMode);
    oLines = oFile.readlines();
    sContent = "".join(oLines);
    sendMailContent(oReceiver, oCc, sSender, sSubject, sContent, oAttachList, sDomain);

def sendRTX(sReceiver, sSender, sTitle, sMsg):
    """ 发送RTX """
    oRTX = TencentRTX()
    oRTX.Receiver = sReceiver.strip().strip(";");
    oRTX.Sender = sSender;
    sTmpTitle = sTitle;
    if not isinstance(sTitle, unicode): sTmpTitle = sTitle.decode(UTF8);
    oRTX.Title = sTmpTitle;
    sTmpMsg = sMsg;
    if not isinstance(sMsg, unicode): sTmpMsg = sMsg.decode(UTF8);
    oRTX.MsgInfo = sTmpMsg;
    oMsgHelper = MessageHelper(APPKEY);
    oMsgHelper.send(oRTX)

def sendSMS(sReceiver, sSender, sMsg):
    """ 发送短信 """
    oSms = TencentSMS();
    oSms.Sender = sSender;
    oSms.Receiver = sReceiver.strip().strip(";");
    sTmpMsg = sMsg;
    if not isinstance(sMsg, unicode): sTmpMsg = sMsg.decode(UTF8);
    oSms.MsgInfo = sTmpMsg;
    oSms.Priority = oSms.Priority;
    oMsgHelper = MessageHelper(APPKEY);
    oMsgHelper.send(oSms);

def _getAddrListWithDomain(oAddr, sDomain = "@tencent.com"):
    ''' 将邮件地址用域名补充完整  '''
    oTmpAddr = oAddr;
    if isinstance(oAddr, str): oTmpAddr = oAddr.strip().strip(";").split(";");
    oAddrWithDomain = [];
    for sAddr in oTmpAddr:
        # 对于为空的情况直接过滤掉
        if not sAddr: continue;
        sTmpAddr = sAddr + "@" + sDomain;
        if (not sDomain) or ("@" in sAddr): sTmpAddr = sAddr;
        oAddrWithDomain.append(sTmpAddr);
    return oAddrWithDomain;

def _getAttachList(oAttach):
    oTmpAttach = oAttach;
    if isinstance(oAttach, str): oTmpAttach = oAttach.strip().strip(";").split(";");
    return oTmpAttach;

def sendByTof(sFrom, sTo, sCc, sSubject, sContent, oAttachList = []):
    oMail = TencentMail();
    oMail.From = sFrom;
    if not isinstance(sFrom, unicode): oMail.From = sFrom.decode(UTF8);
    oMail.To = sTo;
    if not isinstance(sTo, unicode): oMail.To = sTo.decode(UTF8);
    oMail.CC = sCc;
    if not isinstance(sCc, unicode): oMail.CC = sCc.decode(UTF8);
    oMail.Title = sSubject;
    if not isinstance(sSubject, unicode): oMail.Title = sSubject.decode(UTF8);
    oMail.Content = sContent;
    if not isinstance(sContent, unicode): oMail.Content = sContent.decode(UTF8);
    if (0 != len(oAttachList)):
        for sAttach in oAttachList: oMail.Attachments.append(TencentMailAttachment(sAttach));
    # 这里的指定的Key要申请才能获取到，具体怎么申请可以咨询zlightzhang, key决定了发送邮件的机器IP
    # KEY申请 @see: http://tof2.oa.com/application/views/system_list.php
    # 参照 @see: http://km.oa.com/group/599/articles/show/14372
    oMsgHelper = MessageHelper(APPKEY);
    oMsgHelper.send(oMail);

#########################################华丽的分隔线################################################
""" Tencent.OA.Framework 统一消息结构的Python封装.
1. 发送RTX:
rtx = TencentRTX()
rtx.Sender = '900' #发送者
rtx.Receiver = 'somebody;anotherbody' #接收人
rtx.Title = 'test' #标题
rtx.MsgInfo = 'test msg' #正文
msghelper = MessageHelper("appkey") #在此处添加您申请的app key
msghelper.send(rtx)

2. 发送SMS:
sms = TencentSMS()
sms.Sender = '900' #发送者
sms.Receiver = 'somebody;anotherbody' #接收人手机
sms.MsgInfo = 'test msg' #短信内容
msghelper = MessageHelper("appkey") #在此处添加您申请的app key
msghelper.send(sms)

3. 发送Mail:
mail = TencentMail()
mail.From = 'somebody@tencent.com'  #发送方
mail.To = 'somebody@tencent.com'    #接收方
mail.Title = u"邮件标题"             #邮件标题
mail.Content = u"邮件正文"           #邮件正文
msghelper = MessageHelper("appkey") #在此处添加您申请的app key
msghelper.send(mail)

4. 发送带附件的Mail:
mail = TencentMail()
mail.From = 'somebody@tencent.com'  #发送方
mail.To = 'somebody@tencent.com'    #接收方
mail.Title = u"邮件标题"             #邮件标题
mail.Content = u"邮件正文"           #邮件正文
mail.Attachments.append(TencentMailAttachment(filepath1)) #上传第一个附件
mail.Attachments.append(TencentMailAttachment(filepath2)) #上传第二个附件
msghelper = MessageHelper("appkey") #在此处添加您申请的app key
msghelper.send(mail)

参照 @see: http://km.oa.com/group/599/articles/show/14372
APPKEY申请 @see: http://tof2.oa.com/application/views/system_list.php
"""

def escape(s):
    return s.replace('&','&amp;').replace('<','&lt;').replace('>','&gt;').replace("'",'&apos;').replace('"','&quot;')

def today():
    return time.strftime("%Y-%m-%d")

TOF_TEMPALTE = u"""
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:ten="http://schemas.datacontract.org/2004/07/Tencent.OA.Framework.Context" xmlns:tem="http://tempuri.org/" xmlns:ten1="http://schemas.datacontract.org/2004/07/Tencent.OA.Framework.Messages.DataContract">
    <soapenv:Header>
       <Application_Context>
          <ten:AppKey>%s</ten:AppKey>
       </Application_Context>
    </soapenv:Header>
    <soapenv:Body>%s</soapenv:Body>
</soapenv:Envelope>
"""
class MessageHelper:
    PORT = 80
    URL = "/MessageService.svc"
    METHOD = "POST"
    def __init__(self, appkey):
        """
        @param appkey: 申请的appkey
        @note: appkey一定要与 本机ip对应
        """
        self.appkey = appkey

    def send(self, message):
        LOGGER.info("Sending: %s"%message.__class__)
        try:
            data = TOF_TEMPALTE%(self.appkey, message)
        except Exception, e:
            LOGGER.info(str(e))
        #print data
        LOGGER.debug(data)
        # ========== soap request ==========

        headers = {'Host': TOF_HOST_NAME,
            'Content-Type': 'text/xml; charset="%s"'%UTF8,
            'SOAPAction': message.SOAPAction,
            }

        if isinstance(data, unicode): data = data.encode(UTF8)
        # 设置超时时间 单位为秒
        socket.setdefaulttimeout(30);
        conn = httplib.HTTPConnection(TOF_HOST_IP, self.PORT)
        conn.request(self.METHOD, self.URL, data, headers)
        conn.send(data)

        # ========== soap response ==========
        response = conn.getresponse()
        status = response.status
        reason = response.reason
        LOGGER.info("Response %d: %s"%(status, reason))
        LOGGER.debug(response)
        data = response.read()
        response.close()

        if status != 200:
            errmsg = "SOAP Response Error[%d]: %s\n %s"%(status, reason, data)
            LOGGER.error(errmsg)
            raise Exception, errmsg
        else:
            LOGGER.info("SOAP Response Success")

    def send_rtx(self, sender, receiver, title, message, priority=None):
        rtx = TencentRTX()
        rtx.Receiver = receiver or rtx.Receiver
        rtx.Sender = sender or rtx.Sender
        rtx.Title = title or rtx.Title
        rtx.MsgInfo = message or rtx.MsgInfo
        rtx.Priority = priority or rtx.Priority
        self.send(rtx)

    def send_sms(self, sender, receiver, message, priority=None):
        sms = TencentSMS()
        sms.Sender = sender or sms.Sender
        sms.Receiver = receiver or sms.Receiver
        sms.MsgInfo = message or sms.MsgInfo
        sms.Priority = priority or sms.Priority
        self.send(sms)

    def send_mail(self, sender, receiver, title, message, bcc=None, cc=None, type=None,
                    priority=None, format=None, attachments=None, organizer=None, status=None,
                    location=None, start_time=None, end_time=None):
        mail = TencentMail()
        mail.From = sender or mail.From
        mail.To = receiver or mail.To
        mail.Title = title or mail.Title
        mail.Content = message or mail.Content
        mail.Bcc = bcc or mail.Bcc
        mail.CC = cc or mail.Bcc
        mail.EmailType = type or mail.EmailType
        mail.Priority = priority or mail.Priority
        mail.BodyFormat = format or mail.BodyFormat
        mail.Organizer = organizer or mail.Organizer
        mail.MessageStatus = status or mail.MessageStatus
        mail.Location = location or mail.Location
        mail.StartTime = start_time or mail.StartTime
        mail.EndTime = end_time or mail.EndTime
        if attachments:
            if isinstance(attachments, (str, unicode)):
                attachments = [attachments]
            for attachment in attachments:
                mail.Attachments.append(TencentMailAttachment(attachment))
        self.send(mail)


class BaseMessage(object):
    PRIORITY_LOW = "Low"
    PRIORITY_NORMAL = "Normal"
    PRIORITY_HIGHT = "Hight"
    PRIORITY_HEIGHT = PRIORITY_HIGHT  # deprecated, plase use PRIORITY_HIGHT
    def __init__(self):
        self.Priority = self.PRIORITY_NORMAL

    def __str__(self):
        raise ValueError, "Need Implemenet"


MAIL_TEMPLATE = """
<tem:SendMail>
   <tem:mail>
      <ten1:Attachments>
         %(Attachments)s
      </ten1:Attachments>
      <ten1:Bcc>%(Bcc)s</ten1:Bcc>
      <ten1:BodyFormat>%(BodyFormat)s</ten1:BodyFormat>
      <ten1:CC>%(CC)s</ten1:CC>
      <ten1:Content>%(Content)s</ten1:Content>
      <ten1:EmailType>%(EmailType)s</ten1:EmailType>
      <ten1:EndTime>%(EndTime)s</ten1:EndTime>
      <ten1:From>%(From)s</ten1:From>
      <ten1:Location>%(Location)s</ten1:Location>
      <ten1:MessageStatus>%(MessageStatus)s</ten1:MessageStatus>
      <ten1:Organizer>%(Organizer)s</ten1:Organizer>
      <ten1:Priority>%(Priority)s</ten1:Priority>
      <ten1:StartTime>%(StartTime)s</ten1:StartTime>
      <ten1:Title>%(Title)s</ten1:Title>
      <ten1:To>%(To)s</ten1:To>
   </tem:mail>
</tem:SendMail>
"""
class TencentMail(BaseMessage):
    SOAPAction = "http://tempuri.org/IMessageService/SendMail"

    FORMAT_HTML = "Html"
    FORMAT_PLAIN_TEXT = "Text"

    TYPE_TO_EXCHANGE = "SEND_TO_ENCHANGE"
    TYPE_TO_INTERNET = "SEND_TO_INTERNET"
    TYPE_TO_MEETING = "SEND_TO_MEETING"

    STATUS_PICKUP = "Pickup" #等待发送
    STATUS_POST = "Post" #已发送
    STATUS_QUEUE = "Queue" #发送失败后处于重发队列
    def __init__(self, **kwargs):
        """ 调用WEBSERVICE服务发送邮件的接口.
        @var Title: 邮件标题
        @var Content: 邮件内容
        @var From: 邮件发件人
        @var To: 邮件接受人, 多个接收人用;相隔
        @var CC: 邮件抄送人, 多个接收人用;相隔
        @var Bcc: 密件抄送人 , 多个接收人用;相隔
        @var MailType: 邮件类型
        @var Priority: 优先级
        @var BodyFormat: 邮件格式, 设置为FORMAT_HTML,FORMAT_PLAIN_TEXT
        @var Attachments: 附件
        @var Organizer: 会议组织者
        @var Location: 会议地点
        @var MessageStatus: 信息状态, 设置为STATUS_PICKUP,STATUS_POST,STATUS_QUEUE
        @var StartTime: 会议开始时间, 暂时只知道支持'yyyy-mm-dd'格式
        @var EndTime: 会议结束时间, 暂时只知道支持'yyyy-mm-dd'格式
        """
        BaseMessage.__init__(self)
        self.BodyFormat = self.FORMAT_HTML
        self.EmailType = self.TYPE_TO_EXCHANGE
        self.From = ''
        self.To = ''
        self.Bcc = ''
        self.CC = ''
        self.Title = ''
        self.Content = ''
        self.Attachments = []
        self.Organizer = ''
        self.Location = ''
        self.MessageStatus = self.STATUS_QUEUE
        self.StartTime = today()
        self.EndTime = today()

    def __str__(self):
        args = vars(self).copy()
        args['Content'] = escape(args['Content'])
        args['Title'] = escape(args['Title'])
        #args['Attachments'] = u"".join(self.Attachments)
        args['Attachments'] = u"".join([unicode(att) for att in self.Attachments])
        return MAIL_TEMPLATE%args

MAIL_ACCACHMENT_TEMPLATE = """
<ten1:TencentMailAttachment>
    <ten1:FileContent>%(FileContent)s</ten1:FileContent>
    <ten1:FileName>%(FileName)s</ten1:FileName>
 </ten1:TencentMailAttachment>
"""
class TencentMailAttachment(object):
    def __init__(self, fpath, filename=None):
        """
        @param fpath: 附件文件的路径
        @param filename: 附件的文件名, 默认为原文件名
        """
        self.fpath = fpath
        if not os.path.isfile(fpath):
            raise IOError, u"该文件不存在"
        self.FileName = filename or os.path.split(fpath)[1]
        self.FileContent = base64.b64encode(open(fpath, "rb").read())
        self.FileContent = escape(self.FileContent)

    def __str__(self):
        return MAIL_ACCACHMENT_TEMPLATE%vars(self)


RTX_TEMPLATE = """
<tem:SendRTX>
   <!--Optional:-->
   <tem:message>
      <ten1:MsgInfo>%(MsgInfo)s</ten1:MsgInfo>
      <ten1:Priority>%(Priority)s</ten1:Priority>
      <ten1:Receiver>%(Receiver)s</ten1:Receiver>
      <ten1:Sender>%(Sender)s</ten1:Sender>
      <ten1:Title>%(Title)s</ten1:Title>
   </tem:message>
</tem:SendRTX>
"""
class TencentRTX(BaseMessage):
    SOAPAction = "http://tempuri.org/IMessageService/SendRTX"
    def __init__(self):
        """ 调用WEBSERVICE服务发送RTX消息的接口.
        @var Sender:发送人,需要是有效的rtx用户,系统发送为"900",也可以是英文昵称如"mavisluo".
        @var Receiver:接收者.需要是有效的rtx用户,发送手机短信可以是用分号";"分割的多个用户,也可以是分号分割的多个手机号如"13422885961;13728695069".
        @var Title:标题
        @var MsgInfo:是发送的具体信息内容
        @var Priority:优先级
        """
        BaseMessage.__init__(self)
        self.Sender = '900'
        self.Receiver = ''
        self.Title = ''
        self.MsgInfo = ''

    def __str__(self):
        args = vars(self).copy()
        args['MsgInfo'] = escape(args['MsgInfo'])
        args['Title'] = escape(args['Title'])
        return RTX_TEMPLATE%args


SMS_TEMPLATE = """
<tem:SendSMS>
    <!--Optional:-->
    <tem:message>
       <ten1:MsgInfo>%(MsgInfo)s</ten1:MsgInfo>
       <ten1:Priority>%(Priority)s</ten1:Priority>
       <ten1:Receiver>%(Receiver)s</ten1:Receiver>
       <ten1:Sender>%(Sender)s</ten1:Sender>
    </tem:message>
</tem:SendSMS>
"""
class TencentSMS(BaseMessage):
    SOAPAction = "http://tempuri.org/IMessageService/SendSMS"
    def __init__(self):
        """ 调用WEBSERVICE服务发送短信的接口
        @var Sender: 短信发件人,系统发送为900
        @var Receiver: 短信接受人,多个接收人用;相隔
        @var MsgInfo: 短信内容
        @var Priority: 优先级
        """
        BaseMessage.__init__(self)
        self.Sender = '900'
        self.Receiver = ''
        self.MsgInfo = ''

    def __str__(self):
        args = vars(self).copy()
        args['MsgInfo'] = escape(args['MsgInfo'])
        return SMS_TEMPLATE%args

