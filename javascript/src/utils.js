import uuidv4 from 'uuid/v4';

export const getNodeIdentifier = () => uuidv4().replace(/-/g, '');
