import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AlbumListComponent } from './album-list.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('AlbumListComponent', () => {
  let component: AlbumListComponent;
  let fixture: ComponentFixture<AlbumListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AlbumListComponent],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AlbumListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
