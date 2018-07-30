import { TestBed, async } from '@angular/core/testing';
import { AppComponent } from './app.component';
import Routerlinkdirectivestub = require("../testing/router-link-directive-stub");
import RouterLinkDirectiveStub = Routerlinkdirectivestub.RouterLinkDirectiveStub;
import { Component } from '@angular/core';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        RouterLinkDirectiveStub
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  it('should create the app', async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app).toBeTruthy();
  }));
  it(`should have as title 'Last FM'`, async(() => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.debugElement.componentInstance;
    expect(app.title).toEqual('Last FM');
  }));
});
