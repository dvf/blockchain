import { factory } from 'quiq';

export const app = factory.app.create({
	controllers: [
		`${ __dirname }/controllers/**/*.js`
	]
});