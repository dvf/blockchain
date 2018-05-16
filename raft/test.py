import os
import common.CommonHelper
import json
from multiprocessing import Process
import requests
import time
'''
def ReadJsonFile(path):
	with open(path,'r') as load_f:
		load_dict = json.load(load_f)
	return load_dict
def WriteJsonFile(path, data):
	with open(path, 'w') as json_file:
		json_file.write(json.dumps(data))
'''
def NodeRun(port):
	return os.system(f'python RaftProtocol.py -p {port}')
	
def RegisterNodes(target, nodes):
	post_data = json.dumps({			
			"nodes": nodes
		}) 
	response = requests.post(f'http://{target}/register', data=post_data)
	pass

def InitNodes(nodes):
	for node in nodes:
		temp = list(nodes)
		temp.remove(node)
		RegisterNodes(node, temp)
	pass	
	
def UpdateOperator(message):
	pass
if __name__ == '__main__':
	cfg = common.CommonHelper.ReadJsonFile("conf/configure.json")	
	Parr = []
	for address in cfg["nodes"]:
		p = Process(target = NodeRun,args = (address[-4:],)) #必须加,号
		Parr.append(p)
	
	for p in Parr:
		p.start()
	time.sleep(3)
	InitNodes(cfg["nodes"])
	print("Main Process Nodes Initialize Successfully!")
	time.sleep(5)
	
	
	