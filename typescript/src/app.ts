import { Server } from 'quiq';

const { factory } = Server;

export const app = factory.app.create({
	controllers: [
		`${ __dirname }/controllers/**/*.js`
	]
});