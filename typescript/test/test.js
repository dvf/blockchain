'use strict';
const assert = require('assert');
const { fork } = require('child_process');
const { Writeln } = require('writeln');
const { Server: { JsonWebClient } } = require('quiq');

const webClient = new JsonWebClient('Blockchain Test Client');
const nodes = [];

function launchNode(port) {
	nodes.push(fork(process.cwd() + '/lib/index.js', ['' + port], { env: { DEBUG: 'blockchain' } }));
}

describe('Blockchain', function () {
	const logger = new Writeln('Blockchain');

	it('should launch 2 nodes', function (done) {
		launchNode(5000);
		launchNode(5001);

		setTimeout(done, 1500);
	});

	it('should register node 2 on node 1', function (done) {
		webClient
			.post('http://localhost:5000/nodes/register', { nodes: ['http://localhost:5001'] })
			.then(function ({ code }) {
				assert.equal(code, 201);
				done();
			});
	});

	it('should create a new transactions on node 2', function (done) {
		webClient
			.post('http://localhost:5001/transactions/new', {
				sender: "d4ee26eee15148ee92c6cd394edd974e",
				recipient: "someone-other-address",
				amount: 5
			})
			.then(function ({ code }) {
				assert.equal(code, 201);
				done();
			});
	});

	it('should mine 1 blocks on node 2', function (done) {
		webClient.get('http://localhost:5001/mine')
			.then(function ({ code }) {
				assert.equal(code, 200);
				done();
			})
	});

	it('should get consensus on node 1', function (done) {
		webClient.get('http://localhost:5000/nodes/resolve')
			.then(function ({ code, data }) {
				assert.equal(code, 200);
				assert.equal(Object.keys(data)[0], 'replaced');

				done();
			});
	});

	it('should decommission all nodes', function (done) {
		setTimeout(function () {
			nodes.forEach((node) => node.kill('SIGINT'));
			done()
		}, 1500);
	});
});