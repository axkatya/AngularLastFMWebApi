import { browser, by, element } from 'protractor';

describe('album search', () => {

  function navigateTo() {
	browser.waitForAngularEnabled(false);
	return browser.get('/albums');
  }

  it('should display album name', () => {
	navigateTo();
	const artistNameSearchInput = element(by.id('lblAlbumNameSearch'));
	artistNameSearchInput.clear().then(function () {
	  artistNameSearchInput.sendKeys('love');
	});

	const artistNameSearchButton = element(by.id('btnAlbumNameSearch'));
	artistNameSearchButton.click();
	browser.sleep(8000);
	const cardItemName = element(by.className('card__itemname'));
	cardItemName.getText().then(function (text) {
	  expect(text.toUpperCase()).toContain('LOVE');
	});
  });
});
