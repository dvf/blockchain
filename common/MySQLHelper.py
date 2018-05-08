#!/usr/local/bin/python
#coding=utf-8
import MySQLdb
OperationalError = MySQLdb.OperationalError
class MySQL:
    def __init__(self,host,user,password,port=3306,db='',charset="utf8"):
        self.host=host
        self.port=port
        self.user=user
        self.db = db;
        self.password=password
        self.charset=charset
        self.connect();
        if db == '':
            return
        try:
            self.conn.select_db(db)
        except MySQLdb.Error as e:
            print("Mysql Error %d: %s" % (e.args[0], e.args[1]))

    def __del__(self):
        self.close()

    def connect(self):
        try:
            self.conn=MySQLdb.connect(host=self.host,port=self.port,user=self.user,passwd=self.password)
            self.conn.autocommit(True);
            self.conn.set_character_set(self.charset)
            self.cur=self.conn.cursor()
            if self.db != '' :self.conn.select_db(self.db)
        except MySQLdb.Error as e:
            print("Mysql Error %d: %s" % (e.args[0], e.args[1]))


    def select_db(self,db):
        try:
            self.conn.select_db(db)
        except MySQLdb.Error as e:
            self.error = e.args[1];
            print("Mysql Error %d: %s" % (e.args[0], e.args[1]))


    def fetch_row(self):
        result = self.cur.fetchone()
        return result

    def fetch_all(self):
        result=self.cur.fetchall()
        desc = self.cur.description
        d = []
        for inv in result:
            _d = {}
            for i in range(0,len(inv)):
                _d[desc[i][0]] = str(inv[i])
            d.append(_d)
        return d

    def query(self,sql):
        self.error = 'ok--1';
        try:
            n=self.cur.execute(sql)
            self.sql = sql;
            return True;
        except MySQLdb.Error as e:
            self.error = e.args[1];
            print("Mysql Error:%s\nSQL:%s" %(e,sql))
            if self.error.find('server has gone away') != -1:
                self.connect();
            return False;

    def insert(self,table_name,data):
        self.error = 'ok';
        columns=data.keys()
        _prefix="".join(['INSERT INTO `',table_name,'` '])
        _fields=",".join(["".join(['`',column,'`']) for column in columns])
        _values=",".join(["%s" for i in range(len(columns))])
        _sql="".join([_prefix,"(",_fields,") VALUES (",_values,")"])
        _params=[data[key] for key in columns]
        try:
            self.sql = _sql % tuple(_params)
            self.cur.execute(_sql,tuple(_params))
            return True;
        except MySQLdb.Error as e:
            self.error = e.args[1];
            if self.error.find('server has gone away') != -1: self.connect();
            return False;

    def update(self,tbname,data,condition = ''):
        self.error = 'ok';
        _fields=[]
        _prefix="".join(['UPDATE `',tbname,'` ','SET '])
        for key in data:
            _fields.append(" `%s`='%s'" % (key,data[key]))
        if len(condition) == 0:_sql = _prefix + ",".join(_fields)
        else : _sql = _prefix + ",".join(_fields) + ' WHERE ' + condition
        self.sql = _sql;
        try:
            self.cur.execute(_sql)
            return True;
        except MySQLdb.Error as e:
            self.error = e.args[1];
            if self.error.find('server has gone away') != -1: self.connect();
            return False;


    def delete(self,tbname,condition):
        self.error = 'ok';
        _prefix="".join(['DELETE FROM  `',tbname,'`','WHERE'])
        _sql="".join([_prefix,condition])
        self.sql = _sql;
        try:
            self.cur.execute(_sql)
            return True;
        except MySQLdb.Error as e:
            self.error = e.args[1];
            if self.error.find('server has gone away') != -1: self.connect();
            return False;

    def get_last_id(self):
        return self.cur.lastrowid

    def get_last_error(self):
        return self.error;

    def get_last_sql(self):
        return self.sql;

    def rowcount(self):
        return self.cur.rowcount

    def commit(self):
        self.conn.commit()

    def rollback(self):
        self.conn.rollback()

    def close(self):
        self.cur.close()
        self.conn.close()

if __name__=='__main__':
    #db_handler=MySQL('172.24.31.121','root','root121',3306, 'music_search')
    db_handler=MySQL('10.155.128.12','root','123456',3306, 'aispeech')
    #db_handler=MySQL('localhost','root','123456',3306, 'aispeech')
    sql = "select * from asr_task_info limit 2"
    db_handler.query(sql)
    print db_handler.fetch_all()

