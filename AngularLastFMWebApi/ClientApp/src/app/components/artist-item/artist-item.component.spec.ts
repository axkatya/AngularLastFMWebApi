import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArtistItemComponent } from './artist-item.component';
import { NO_ERRORS_SCHEMA } from '@angular/core';

describe('ArtistItemComponent', () => {
  let component: ArtistItemComponent;
  let fixture: ComponentFixture<ArtistItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ArtistItemComponent],
      schemas: [NO_ERRORS_SCHEMA]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArtistItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
