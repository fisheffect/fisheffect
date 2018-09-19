export default {

  getIndexOfStart()
  {
    return 0;
  },

  getArraySize()
  {
    return this.getSizeOfDna() + this.sizeOfBirthBlockHeight() + this.sizeOfQuantityOfFeeds() + this.sizeOfFedWithFishBlockHeight();
  },

  getIndexOfRandomDnaInfluence()
  {
    return this.getIndexOfStart();
  },

  sizeOfRandomDnaInfluence()
  {
    return 4;
  },

  getIndexOfReefDnaInfluence()
  {
    return this.getIndexOfRandomDnaInfluence() + this.sizeOfRandomDnaInfluence();
  },

  sizeOfReefDnaInfluence()
  {
    return 16;
  },

  getIndexOfDna()
  {
    return this.getIndexOfStart();
  },

  getSizeOfDna()
  {
    return this.sizeOfRandomDnaInfluence() + this.sizeOfReefDnaInfluence();
  },

  getIndexOfBirthBlockHeight()
  {
    return this.getIndexOfDna() + this.getSizeOfDna();
  },

  sizeOfBirthBlockHeight()
  {
    return 4;
  },

  sizeOfQuantityOfFeeds()
  {
    return 1;
  },

  getIndexOfQuantityOfFeeds()
  {
    return this.getIndexOfBirthBlockHeight() + this.sizeOfBirthBlockHeight();
  },

  getIndexOfFedWithFishBlockHeight()
  {
    return this.getIndexOfQuantityOfFeeds() + this.sizeOfQuantityOfFeeds();
  },

  sizeOfFedWithFishBlockHeight()
  {
    return 4;
  }
}