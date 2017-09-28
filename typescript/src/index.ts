import { Server } from 'quiq';

const { factory: { app, server } } = Server;
const port = parseInt(process.argv[2] || '5000', 10);

server.create(app.create({
	controllers: [
		`${ __dirname }/controllers/**/*.js`
	]
}), port);
