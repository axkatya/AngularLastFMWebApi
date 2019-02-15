import { browser, by, element } from 'protractor';

describe('artist search', () => {

  function navigateTo() {
    browser.waitForAngularEnabled(false);
    return browser.get('/artists');
  }

  it('should display artist name', () => {
    navigateTo();
    const artistNameSearchInput = element(by.id('lblArtistNameSearch'));
    artistNameSearchInput.clear().then(function () {
      artistNameSearchInput.sendKeys('Cher');
    });

    const artistNameSearchButton = element(by.id('btnArtistNameSearch'));
    artistNameSearchButton.click();
    browser.sleep(8000);
    expect(element(by.className('card__itemname')).getText()).toEqual('CHER');
  });
});
