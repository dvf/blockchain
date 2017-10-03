import { factory } from 'quiq';

const port = parseInt(process.argv[2] || '5000', 10);

factory.server.create(factory.app.create({
	controllers: [
		`${ __dirname }/controllers/**/*.js`
	]
}), port);
