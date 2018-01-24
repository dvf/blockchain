import getApp from './app';
import Blockchain from './blockchain';

const port = process.argv[2] || 5000;
const app = getApp(new Blockchain());

app.listen(port, () => console.log(`Blockchain server listening on port ${port}!`)); // eslint-disable-line no-console
