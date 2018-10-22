import { getNodeIdentifier } from './utils';

describe('utils', () => {
  describe('getNodeIdentifier', () => {
    it('should return a random string of length 32', () => {
      const str1 = getNodeIdentifier();
      const str2 = getNodeIdentifier();

      expect(str1).not.toEqual(str2);
      expect(str1.length).toEqual(32);
      expect(str2.length).toEqual(32);
    });
  });
});
