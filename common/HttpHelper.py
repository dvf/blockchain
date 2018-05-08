#!/usr/bin/env python
import urllib
import urllib2
import time
import os
import sys

g_http_proxy = '';

def Post(url, dat):
    global g_http_proxy;
    req = urllib2.Request(url, data = dat);
    if len(g_http_proxy) != 0 :
        req.set_proxy(g_http_proxy, 'http');
    return Req(req);

def Get(url):
    global g_http_proxy;
    header = {"Proxy-Connection" : "keep-alive"}
    req = urllib2.Request(url, headers = header);
    if len(g_http_proxy) != 0 :
        req.set_proxy(g_http_proxy, 'http');
    return Req(req);

def Req(req):
    retry_times = 0
    error_code = 0
    while retry_times < 2:
        try:
            retry_times += 1
            handle = urllib2.urlopen(url = req ,timeout = 300)
            error_code = handle.getcode()
            break
        except urllib2.HTTPError, e:
            error_code = e.code
            if retry_times < 2 and error_code != 404:
                continue
            else:
                print 'HTTPError'
                return False,None
        except Exception, e:
            if retry_times < 2:
                continue
            else:
                print 'Exception'
                print e
                return False,None

    if not handle:
        print "pid = %d, urlopen:%s failed" % (os.getpid(), url)
        return False,None
    #content_type =  handle.headers['Content-Type']
    #content_type = content_type.lower()

    try:
        html_data = handle.read()
    except:
        handle.close()
        print 'read error'
        return False,None
    handle.close()
    return True,html_data


if __name__ == "__main__":
    g_http_proxy = '10.150.167.40:18080'
    (ret, html) = Get('http://news.16888.com/')
    print ret
