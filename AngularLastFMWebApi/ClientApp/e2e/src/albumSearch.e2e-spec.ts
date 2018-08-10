import { browser, by, element } from 'protractor';

describe('album search', () => {

  function navigateTo() {
    browser.waitForAngularEnabled(false);
    return browser.get('/albums');
  }

  it('should display album name', () => {
    navigateTo();
    var artistNameSearchInput = element(by.id("lblAlbumNameSearch"));
    artistNameSearchInput.clear().then(function () {
      artistNameSearchInput.sendKeys('love');
    });

    var artistNameSearchButton = element(by.id("btnAlbumNameSearch"));
    artistNameSearchButton.click();
    browser.sleep(8000);
    var cardItemName = element(by.className('card__itemname'));
    cardItemName.getText().then(function (text) {
      expect(text.toUpperCase()).toContain('LOVE');
    });
  });
});
