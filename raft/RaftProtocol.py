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
	update = 3
	commit = 4
class NodeState(Enum):
	heartbeat = 1
	update = 2
	commit = 3
class Node:	 
	def __init__(self):
		self.role = Role.Follower
		self.address = ""
		self.client_address = ""
		self.nei_nodes = set()
		self.neighbor_num = 0
		self.leader = ""		  # restore the Leader`s address, "ip:port"
		self.message = ""
		self.is_client_on = False
		self.support_count = 0
		self.confirm_count = 0
		self.leader_on = False
		self.has_electing = False
		self.logger = None
		self.state = NodeState.heartbeat.value # only for leader
		pass
	def RegisterNodes(self, address):
		"""
		Function: Add a new node to the list of nodes
		Param 
			address: Address of node. Eg. 'http:#192.168.0.5:5000'
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
	def GetNeiNodes(self):
		'''
		Function: get the neighbor nodes
		'''
		return [item for item in leader.nei_nodes]
	def HbOperator(self):
		'''
		Function: heartbeat function
		'''
		post_data = json.dumps({
			'message_type': MessageType.heartbeat.value,
			'sender': self.address,
			'message': ""
		})	
		unreach_nodes = set()
		for node in self.nei_nodes:
			try:
				response = requests.post(f'http://{node}/heartbeat/receive', data=post_data)
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace);
				print(f'Error POST[http://{node}/heartbeat/receive]')
				self.logger.error(msg)
				unreach_nodes.add(node)
				continue
			values = response.json()
			required = ['message']
			if not all (k in values for k in required):
				print(f'Heartbeat Error: {node}')
				continue
			if values['message'] == 'RESET':
				self.role = Role.Follower
				break
		self.UpdateNeiNode(unreach_nodes)		
		pass
	def UpdateOperator(self):	
		'''
		Function: update the message among the nodes
		'''
		post_data = json.dumps({
			'message_type': MessageType.update.value,
			'sender': self.address,
			'message': self.message
		})
		self.confirm_count = 0		
		unreach_nodes = set()
		for node in self.nei_nodes:
			try:
				response = requests.post(f'http://{node}/heartbeat/receive', data=post_data)
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace)				
				print(f'Error POST[http://{node}/heartbeat/receive]')
				self.logger.error(msg)
				unreach_nodes.add(node)
				continue
			values = response.json()
			required = ['message']
			if not all (k in values for k in required):
				print(f'Heartbeat Error: {node}')
				continue
			if values['message'] == 'RESET':
				self.role = Role.Follower
				break
			elif values['message'] == 'OK':
				self.confirm_count = self.confirm_count + 1
		self.UpdateNeiNode(unreach_nodes)	
		if self.neighbor_num == 0 or self.confirm_count > self.neighbor_num/2:
			self.state = NodeState.commit.value
			return True		
		else:
			# continue to update
			return False
		pass
	def CommitOperator(self):
		'''
		Function: commit the update
		'''
		self.state = NodeState.heartbeat.value
		post_data = json.dumps({
			'message_type': MessageType.commit.value,
			'sender': self.address,
			'message': self.message
		})	
		unreach_nodes = set()
		for node in self.nei_nodes:
			try:
				response = requests.post(f'http://{node}/heartbeat/receive', data=post_data)
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace)
				print(f'Error POST[http://{node}/heartbeat/receive]')
				self.logger.error(msg)
				unreach_nodes.add(node)
				continue
			values = response.json()
			required = ['message']
			if not all (k in values for k in required):
				print(f'Heartbeat Error: {node}')
				continue
			if values['message'] == 'RESET':
				self.role = Role.Follower
				break
		self.UpdateNeiNode(unreach_nodes)	
	def Send2Client(self, message):
		'''
		Function: send the message to the client
		Param: 
			message: data sended to client
		'''
		post_data = json.dumps({
			'sender': self.address,
			'message': message
		})
		try:				
			response = requests.post(f'http://{self.client_address}/heartbeat/receive', data=post_data)
		except Exception as err:
			trace = traceback.format_exc()
			msg = "[Exception] %s\n%s" % (str(err), trace)
			print(f'Error POST[http://{node}/heartbeat/receive]')
			self.logger.error(msg)
			return False
		#values = response.json()
		return True
	def Heartbeat(self):
		"""
		Function: The Leader keeps the relationship with Followers
		"""
		#handle_type = MessageType.heartbeat.value
		#if self.is_client_on:
		#	   handle_type = 1
		if self.state == NodeState.heartbeat.value:
			self.HbOperator()
		elif self.state == NodeState.update.value:
			while not self.UpdateOperator():
				time.sleep(0.1) # sleep 100ms
				pass
		elif self.state == NodeState.commit.value:
			self.CommitOperator()
			self.UpdateLog(self.message)			
			# rely to client
			self.Send2Client("Commit successfully")			
		pass
	def HasElecting(self):
		'''
		Function: Judge the eletion state
		'''
		if self.has_electing:
			return True
		else:
			self.has_electing = True
			return False
		pass
	def UpdateNeiNode(self, remove_nodes):
		'''
		Function: remove the unlinked nodes 
		Param:
			remove_nodes: unlinked nodes
		'''
		for node in remove_nodes:
			self.nei_nodes.remove(node)
		self.neighbor_num = len(self.nei_nodes)
		pass
	def Election(self): 
		"""
		Function: Candidate apply to be leader
		Param:
			
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
				msg = "[Exception] %s\n%s" % (str(err), trace)
				print(f'Error POST[http://{node}/heartbeat/receive]')
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
			self.state = NodeState.heartbeat.value
			self.role = Role.Leader
			self.info(f'Elect Leader[{self.address}]')
			self.logger.info(f'Elect Leader[{self.address}]')
			return True
		elif self.support_count == self.neighbor_num/2 and not self.has_electing:
			self.has_electing = True			
			self.state = NodeState.heartbeat.value
			self.role = Role.Leader
			self.info(f'Elect Leader[{self.address}]')
			return True
		self.role = Role.Follower
		return False
		pass
	def Rely2Client(self, values):
		"""
		Function: Leader deals with the request from clients.
		Param:	
			values: data from clients
		"""
		if self.role != Role.Leader:			
			return {'message': "Not Leader"} 
		self.client_address = values['sender']		
		self.message = values['message']
		self.is_client_on = True
		self.state = NodeState.update.value
		return {'message': "OK"}
		
	def UpdateLog(self, message):
		'''
		Function: update the log as request from clients
		Param:
			message: updating data
		'''
		msg = f'Update Log[{message}]'
		print(msg)
		self.logger.info(msg)
		return True
		pass
	def HeartbeatReceive(self, values):
		'''
		Function: resolve the requests from the other nodes 
		Param: 
			values: data of requests
		'''
		if values['message_type']==MessageType.heartbeat.value: # Candidate
			self.leader_on = True
			self.leader = values['sender']
			if self.role == Role.Leader:
				response = {'message': 'RESET'}
			else:
				response = {'message': 'OK'}
		elif values['message_type'] == MessageType.election.value: # Leader
			if self.HasElecting():
				response = {'message': 'NO'}
			else:
				response = {'message': 'OK'}
		elif values['message_type'] == MessageType.update.value: # Candidate
			self.leader_on = True
			print('updating')
			response = {'message': 'OK'}
		elif values['message_type'] == MessageType.commit.value: # Candidate
			self.leader_on = True
			print('commit')
			if self.UpdateLog(values['message']):
				response = {'message': 'OK'}
			else:
				response = {'message': 'ERROR'}
		return response
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
		Function: Followers monitor the time delay, until election timeout.
		"""
		self.address = address
		self.logger = common.CommonHelper.GetLogger('log/%s_%s.log' % (time.strftime("%Y%m%d"),self.address[-4:]));
		while(1):
			try:
				print(f'{self.address} {self.role} Start!')
				if self.role == Role.Leader:
					time.sleep(2)
					rel = self.Heartbeat()
				elif self.role == Role.Follower:
					time.sleep(3)
					self.ConfirmLeader()
				elif self.role == Role.Candidate:
					self.Election()
			except Exception as err:
				trace = traceback.format_exc()
				msg = "[Exception] %s\n%s" % (str(err), trace)
				print(f'Running Error!')
				self.logger.error(msg)
		pass

# Instantiate the Node
app = Flask(__name__)

leader = Node()
@app.route('/register', methods=['POST'])
def RegisterNodes():
	values = request.get_json(force=True)
	nodes = values.get('nodes')
	if nodes is None:
		return "Error: Please supply a valid list of nodes", 400
	for node in nodes:
		leader.RegisterNodes(node)
	response = {
		'message': 'New nodes have been added',
		'total_nodes': leader.GetNeiNodes()
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
	response = leader.HeartbeatReceive(values)
	return jsonify(response), 201
@app.route('/nodes', methods=['GET'])
def full_nodes():
	nodes = leader.GetNeiNodes()
	response = {
		'nodes': nodes,
		'length': len(nodes),
	}
	return jsonify(response), 200
@app.route('/update', methods=['POST'])
def Update():	
	values = request.get_json(force=True)
	required = ['sender', 'message']
	if not all(k in values for k in required):
		return 'Missing values', 400
	response = leader.Rely2Client(values)
	
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



