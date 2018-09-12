/**
 * Simple blockchain example
 *
 */

import process from 'process';

import Blockchain from './lib/blockchain';
import server from './lib/server';


// Instantiate the Blockchain
export const blockchain = new Blockchain();

// Start the server with provided port or default port 3000
let port = process.argv[2];
port = Number(port) > 0 ? port : 3000;

server.init(port);
