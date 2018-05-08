import os
import _thread
from enum import Enum
import hashlib
import json
import time
from urllib.parse import urlparse
from uuid import uuid4
import traceback;
import requests
from flask import Flask, jsonify, request
import common.CommonHelper
#Configure file

ElectionTimeout = 300
HeartbeatTimeout = 150

class Role(Enum):
	Leader = 1
	Candidate = 2
	Follower = 3
class MessageType(Enum):
	end = 0	
	heartbeat = 1
	election = 2
	operation = 3
	
class Node:		
	def __init__(self, logger):
		self.role = Role.Follower
		self.address = ""
		self.nei_nodes = set()
		self.neighbor_num = 0
		self.time_count = 0
		self.leader = ""		  # restore the Leader`s address, "ip:port"
		self.message = ""
		self.is_client_on = False
		self.support_count = 0
		self.confirm_count = 0
		self.leader_on = False
		self.has_electing = False
		self.logger = logger
		pass
	def register_nodes(self, address):
		"""
		Add a new node to the list of nodes

		:param address: Address of node. Eg. 'http:#192.168.0.5:5000'
		"""
		parsed_url = urlparse(address)
		if parsed_url.netloc:
			self.nei_nodes.add(parsed_url.netloc)
		elif parsed_url.path:
			# Accepts an URL without scheme like '192.168.0.5:5000'.
			self.nei_nodes.add(parsed_url.path)
		else:
			raise ValueError('Invalid URL')
		self.neighbor_num = len(self.nei_nodes)
		pass
	def AskForSupporters(self):
		"""
		Function: Candidates ask for supporters to be Leader in Election process 
		"""
		
		pass
	def Response2Election(self, sender):
		"""
		Function: Send 'OK' or nothing to the Candidate
		Param:
		sender: The sender`s adddress
		"""
		pass
		
	def Heartbeat(self):
		"""
		Function: The Leader keeps the relationship with Followers
		"""
		#handle_type = MessageType.heartbeat.value
		#if self.is_client_on:
		#		handle_type = 1
		
		self.confirm_count = 0;
		post_data = json.dumps({
			'message_type': MessageType.heartbeat.value,
			'sender': self.address,
			'message': self.message
		})
		unreach_nodes = set()
		for node in self.nei_nodes:
			try:
				response = requests.post(f'http://{node}/heartbeat/receive', data=post_data);
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace);
				print(msg)
				self.logger.error(msg)
				unreach_nodes.add(node)
				continue
			values = response.json()
			required = ['message']
			if not all (k in values for k in required):
				print(f'Heartbeat Error: {node}')
				continue
			if values['message'] != 'OK':
				self.confirm_count = self.confirm_count + 1
		self.UpdateNeiNode(unreach_nodes)		
		if self.confirm_count > self.neighbor_num/2: 
			self.message = ""
			self.is_client_on = False
			return True
		return False
		pass
	def HasElecting(self):
		if self.has_electing:
			return True
		else:
			self.has_electing = True
			return False
		pass
	def UpdateNeiNode(self, remove_nodes):
		for node in remove_nodes:
			self.nei_nodes.remove(node)
		pass
	def Election(self):	
		"""
		Function: Candidate apply to be leader
		Param:
			message: 
		"""
		post_data = json.dumps({
			'message_type': MessageType.election.value,
			'sender': self.address,
			'message': self.message
		})
		
		self.support_count = 0
		unreach_nodes = set()
		for node in self.nei_nodes:
			try:
				response = requests.post(f'http://{node}/heartbeat/receive', data=post_data);
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace);
				print(msg)
				self.logger.error(msg)
				unreach_nodes.add(node)
				continue
			values = response.json()
			required = ['message']
			if not all (k in values for k in required):
				print(f'Heartbeat Error: {node}')
				continue
			if values['message'] == 'OK' or values['message'] == 'ok' :
				self.support_count = self.support_count + 1
		self.UpdateNeiNode(unreach_nodes)
		if self.support_count > self.neighbor_num/2:
			self.role = Role.Leader
			return True
		self.role = Role.Follower
		return False
		pass
		
	def RequestHandle(self, message):
		"""
		Function: Leader deals with the request from clients.
		Param:						
		"""
		self.message = message
		self.is_client_on = True
		pass
	def ConfirmLeader(self):
		"""
		Function: Followers make sure that the leader is on.
		"""
		self.has_electing = False
		if self.leader_on:
			print(f'leader is on {self.leader}')
			self.leader_on = False
		else:
			self.role = Role.Candidate
		pass 
	
	def Start(self, address):
		"""
		Function: Followers monitor the time_count, until election timeout.
		"""
		self.address = address
		while(1):
			print(f'{self.address} {self.role} Start!')
			if self.role == Role.Leader:			
				time.sleep(2)
				rel = self.Heartbeat()
			elif self.role == Role.Follower:
				time.sleep(3)
				self.ConfirmLeader()
			elif self.role == Role.Candidate:
				self.Election()
					
		pass

# Instantiate the Node
app = Flask(__name__)
logger = common.CommonHelper.GetLogger('log/%s.log' % time.strftime("%Y%m%d"));
leader = Node(logger)
@app.route('/register', methods=['POST'])
def register_nodes():
	values = request.get_json()
	nodes = values.get('nodes')
	if nodes is None:
		return "Error: Please supply a valid list of nodes", 400
	for node in nodes:
		leader.register_nodes(node)
	response = {
		'message': 'New nodes have been added',
		'total_nodes': list(leader.nei_nodes),
	}
	return jsonify(response), 201
@app.route('/heartbeat/receive', methods=['POST'])
def HeartbeatReceive():
	values = request.get_json(force=True)
	
	#print(values);
	# Check that the required fields are in the POST'ed data
	required = ['message_type','sender', 'message']
	if not all(k in values for k in required):
		return 'Missing values', 400
	if values['message_type']==MessageType.heartbeat.value:	# Candidate
		leader.leader_on = True
		leader.leader = values['sender']
		response = {'message': 'OK'}
		return jsonify(response), 201
		#leader.HeartbeatFeedback(valudes['sender'], valudes['message'])
	elif values['message_type'] == MessageType.election.value: # Leader
		if leader.HasElecting():
			response = {'message': 'NO'}
		else:
			response = {'message': 'OK'}
		return jsonify(response), 201
	elif values['message_type'] == MessageType.operation.value: # Candidate
		print('operation')
		response = {'message': 'OK'}
		return jsonify(response), 201
@app.route('/nodes', methods=['GET'])
def full_nodes():
    nodes = [item for item in leader.nei_nodes]
    response = {
        'nodes': nodes,
        'length': len(nodes),
    }
    return jsonify(response), 200  

# thread function 
def FlaskRun(host, port):
	global app
	app.run(host, port)
	pass

if __name__ == '__main__':
	from argparse import ArgumentParser
	parser = ArgumentParser()
	parser.add_argument('-p', '--port', default=5000, type=int, help='port to listen on')
	args = parser.parse_args()
	port = args.port
	try:
		_thread.start_new_thread(FlaskRun,('0.0.0.0', port, ))
	except:
		print ("Error: 无法启动Flask")
	leader.Start(f'127.0.0.1:{port}')



