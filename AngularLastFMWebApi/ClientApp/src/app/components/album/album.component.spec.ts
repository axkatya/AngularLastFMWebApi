import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumComponent } from './album.component';
import Albumservice = require("../../services/album.service");
import AlbumService = Albumservice.AlbumService;
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('AlbumComponent', () => {
  let component: AlbumComponent;
  let fixture: ComponentFixture<AlbumComponent>;

  let albumServiceStub: Partial<AlbumService> = {};

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlbumComponent],
      providers: [{ provide: AlbumService, useValue: albumServiceStub }],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
